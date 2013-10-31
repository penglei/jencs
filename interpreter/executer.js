var ast = require("../parse/ast");


var debugTestPauseTime = 3000;
function waitDebugger(cb, that){
    cb.call(that);
}

function def_execute(nodeType, executeFun){

    nodeType.proto("execute", function(){
        if (this instanceof ast.AST_Escape && false){
            var args = Array.prototype.slice.call(arguments, 0);
            waitDebugger(function(){
                executeFun.apply(this, args);
            }, this);
        } else {
            executeFun.apply(this, Array.prototype.slice.call(arguments, 0));
        }
    });
}

//---------------exports-------------------//
def_execute(ast.AST_Node, function(){});

exports.def_execute = def_execute;

exports.run = function(astInstance, context){
    //方便表达式里使用(主要是symbol接口)，execute传递context，没有什么差别(从语法树最上面传到最后一个节点..)
    ast.AST_Node.proto("context", context);
    astInstance.execute(context);
    ast.AST_Node.proto("context", null);
};
