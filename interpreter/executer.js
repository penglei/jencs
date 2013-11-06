var ast = require("../parse/ast");
var Util = require("./util");
var CSDebugger = require("./debugger/app");

//startup -> waitConnect -> connect-> isbreakfirst -n-> hasBreakpoints -y-> runToFirstBreakpoint -> waitDebuggerCommand

var debugMode = true;
function def_execute(nodeType, executeFun){

    nodeType.proto("execute", function(){
        var args = Util.argsToArr(arguments);
        if (debugMode){
            debugger
            waitDebugger(function(){
                executeFun.apply(this, args);
            }, this);
        } else {
            executeFun.apply(this, args);
        }
    });
}

var nextExeEntity;

function waitDebugger(cb, that){
    nextExeEntity = function(){
        cb.call(that);
        return that.pos;
    };
}

CSDebugger.onNext(function(){
    debugger
    return nextExeEntity();//执行这一条语句
});

//---------------exports-------------------//
def_execute(ast.AST_Node, function(){});

exports.def_execute = def_execute;

exports.run = function(astInstance, context){
    CSDebugger.bootstrap(function ready(){
        //方便表达式里使用(主要是symbol接口)，execute传递context，没有什么差别(从语法树最上面传到最后一个节点..)
        ast.AST_Node.proto("context", context);
        astInstance.execute(context);
        ast.AST_Node.proto("context", null);
    });
};
