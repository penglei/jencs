var ast = require("../parse/ast");
var util = require("util");
var events = require("events");

//var CSDebugger = require("./debugger/app");
//startup -> waitConnect -> connect-> isbreakfirst -n-> hasBreakpoints -y-> runToFirstBreakpoint -> waitDebuggerCommand

var nextExeEntity;

function waitDebugger(cb, that){
    nextExeEntity = function(){
        cb.call(that);
        return that.pos;
    };
}

/*
CSDebugger.onNext(function(){
    debugger
    return nextExeEntity();//执行这一条语句
});
*/

function def_execute(nodeType, executeFun){
    nodeType.proto("execute", function(){
        var args = Array.prototype.slice.call(arguments);
        executeFun.apply(this, args);
    });
}

def_execute(ast.AST_Node, function(){});

exports.def_execute = def_execute;

function Executer(){
    this.context = null;
}

util.inherits(Executer, events.EventEmitter);

Executer.prototype.run = function(astInstance, context){
    this.context = context;
    ast.AST_Node.proto("executer", this);
    ast.AST_Node.proto("context", context);

    astInstance.execute(context);

    ast.AST_Node.proto("context", null);
    ast.AST_Node.proto("executer", null);
};

Executer.prototype.genList = function(bodyList, endcb, that){
    if (!bodyList) endcb.call(that);
    for(var i = 0; i < bodyList.length; i++){
        bodyList[i].execute(this.context);
    }
    endcb.call(that);
};

Executer.prototype.genLoop = function(stepEntity, cb){
    stepEntity();
    cb();
};

exports.Executer = Executer;
