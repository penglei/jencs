var ast = require("../parse/ast");
var util = require("util");
var events = require("events");

function Empty(){}

function def_execute(nodeType, executeFun){
    nodeType.proto("execute", function(){
        var args = Array.prototype.slice.call(arguments);
        if (!(this instanceof ast.AST_MacroDef)) executeFun.apply(this, args);//这个条件不能去掉
    });
}

function Command(act, astNode, that, args){
    this.action = act;
    this.args = args;
    this.that = that;
    this.node = astNode;
    this.next = null;
}

Command.prototype.go = function(){
    if (this.action){
        this.action.apply(this.that, this.args);
    }
};

function Executer(){
    events.EventEmitter.call(this);

    this.context = null;

    this.cmdHead = null;
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

Executer.prototype.forward = function(){
    var cmd = this.cmdHead;
    if (cmd){
        this.cmdHead = cmd.next;
        //执行command的动作，可以在这里卡住
        cmd.go();
    }
};


Executer.prototype.genList = function(bodyList, endcb, that){

    var st, commandLocalStart, currentCommand;

    //i = 0已经在上面处理
    for (var i = 0; i < bodyList.length; i++){
        st = bodyList[i];

        if (st instanceof ast.AST_MacroDef){
        } else {
            st.execute(this.context);

            var command = new Command(st.execute, st, st, [this.context]);
            if (!commandLocalStart) {
                currentCommand = commandLocalStart = command;
            } else {
                currentCommand = currentCommand.next = command;
            }
        }
    }
    if (endcb) {
        endcb.call(that);

        //currentCommand有可能是没有的，比如一个*只有*宏定义的文件
        if (currentCommand) currentCommand = currentCommand.next = new Command(endcb, null, that);
    }

    if (this.cmdHead){
        //!!每一次取一个command时都会从链接头把其取下来，并且修改this.cmdHead使用指向下一个command，这有点向指令指针
        if (currentCommand){
            currentCommand.next = this.cmdHead;
            this.cmdHead = commandLocalStart;
        }
    } else {
        this.cmdHead = commandLocalStart;
    }

    /*
    while(this.cmdHead) {
        this.forward();//前进执行
    }
    */
};

Executer.prototype.genLoop = function(stepFun, cb, that){
    stepFun.call(that);
    if (cb) cb.call(that);
};

Executer.prototype.step = function(step, next, that){
    step.call(that);
    if (next) next.call(that);
};

def_execute(ast.AST_Node, function(){});

exports.Executer = Executer;
exports.def_execute = def_execute;

