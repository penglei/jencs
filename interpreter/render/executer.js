var ast = require("../../parse/ast");


var debugTestPauseTime = 3000;
function waitDebugger(cb, that){
    cb.call(that);
}

function def_execute(nodeType, executeFun){

    nodeType.proto("execute", function(){
        if (this instanceof ast.AST_Escape){
            var args = Array.prototype.slice.call(arguments, 0);
            waitDebugger(function(){
                executeFun.apply(this, args);
            }, this);
        } else {
            executeFun.apply(this, Array.prototype.slice.call(arguments, 0));
        }
    });
}

//---------------exports-------------------
def_execute(ast.AST_Node, function(){});

exports.def_execute = def_execute;

exports.run = function(astInstance, context){
    function print(str){
        context.output(str);
    }
    ast.AST_Node.proto("context", context);
    astInstance.execute(print);
    ast.AST_Node.unproto("context", context);
};
