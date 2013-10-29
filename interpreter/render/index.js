var Walker = require("./walker").Walker;
var ast = require("../../parse/ast");
var Util = require("./util");
var Scope = require("./scope");
var Executer = require("./executer");

var Context = Scope.Context;

require("./external");

//放在这里而不是executer里面，是因为executer里的def_execute会被它们用到，这就会形成循环依赖
require("./block");
require("./statement");
require("./expr");

function initScopeLayer(astTree){
    var scope = astTree.parent_scope = null,
        macros = {};
    var tree_scope_walker = new Walker(function(node, descend){
        if (node instanceof ast.AST_Scope){
            node.init_scope();
            var saved_scope = scope;
            if (!(node instanceof ast.AST_MacroDef)){
                //macrodef的作用域是运行时决定的。所以不需要给其加上parent_scope，等到call的时候再指定
                node.parent_scope = scope;
            } else {
                macros[node.id] = node;
                if (node.parameters.length > 1){
                    //TODO 检查去重，不允许同名形参
                }
            }
            scope = node;
            //保存上一级scope，后续与当前scope平级的定义才能找到这个scope
            descend();//手动递归下降访问，进入当前（新的）scope
            scope = saved_scope;//还原当前scope
            return true;
        }

        //XXX 如果要支持先使用，后定义，可以修改这个逻辑（先遍历一次，找出所有macro_def）
        if (node instanceof ast.AST_MacroCall){
            var macro = macros[node.id];
            if (macro){
                if (macro === scope){
                    var errMsg = "不允许宏进行递归调用,在宏:\"" + node.id + "\"中";
                    throw new Error(errMsg);
                }
                if (node.args.length != macro.parameters.length){
                    var i = node.args.length > macro.parameters.length;
                    var errMsg = 'call macro:"' + node.id + '" but arguments is too ' + (i? "more" : "less") + "!";
                    throw new Error(errMsg);
                }
                node.refMacro = macro;
            } else {
                var errMsg = 'call macro:"' + node.id + '" that was not been defined';
                throw new Error(errMsg);
            }
        }
    });
    astTree.walk(tree_scope_walker);
}


//处理作用域->为不同的语法节点安装不同的执行函数->从根节点开始执行
exports.render = function(astInstance, hdfdata, renderListener){
    initScopeLayer(astInstance);//虽然会修改ast，但它是没有什么副作用的.

    var context = new Context();
    context.initData(hdfdata); //初始化root hdf节点的包装器
    context.setRenderListener(renderListener);
    Executer.run(astInstance, context);

};
