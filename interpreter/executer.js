var util = require("util");
var events = require("events");
var ast = require("../parse/ast");
var CSDebugger = require("./debugger/app");

function Empty(){}

function def_execute(nodeType, executeFun){
    nodeType.proto("execute", function(){
        var args = Array.prototype.slice.call(arguments);
        if (!(this instanceof ast.AST_MacroDef)) executeFun.apply(this, args);//这个条件不能去掉
    });
}
def_execute(ast.AST_Node, function(){});

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

    this._debugMode = false;
}

util.inherits(Executer, events.EventEmitter);

Executer.prototype.enableDebugger = function(){
    this._debugMode = true;
};

Executer.prototype.clean = function(){
    ast.AST_Node.proto("context", null);
    ast.AST_Node.proto("executer", null);
};

Executer.prototype.run = function(astInstance, context){
    this.context = context;
    ast.AST_Node.proto("executer", this);
    ast.AST_Node.proto("context", context);

    astInstance.execute(context);//AST_Program start

    if (!this._debugMode){
        this.clean();
    } else {
        this.on("end", this.clean)
    }
};

Executer.prototype.start = function(){
    if (this._debugMode){
        var self = this;
        CSDebugger.bootstrap(this, function(){
            var astnode = self.forward();
            if (astnode) return astnode.pos;
        });
        //恢复执行到下一个断点
        CSDebugger.onResume(function(){
        });
        //单步执行
        CSDebugger.onStepOver(function(){
            var astnode = self.forward();
            if (astnode) return astnode.pos;
        });
        //单步执行，遇到macro进入
        CSDebugger.onStepInto(function(){
        });
        //跳出当前宏
        CSDebugger.onStepOut(function(){
        });
    } else {
        while(this.cmdHead) this.forward();
    }
};

Executer.prototype.forward = function(){
    var cmd = this.cmdHead;
    if (cmd){
        this.cmdHead = cmd.next;
        //执行command的动作，可以在这里卡住
        cmd.go();
        return cmd.node;
    }
};


Executer.prototype.genList = function(bodyList, endcb, that){

    var st, commandLocalStart, currentCommand;

    //i = 0已经在上面处理
    for (var i = 0; i < bodyList.length; i++){
        st = bodyList[i];

        if (st instanceof ast.AST_MacroDef){
        } else {
            ////st.execute(this.context);

            var command = new Command(st.execute, st, st, [this.context]);
            if (!commandLocalStart) {
                currentCommand = commandLocalStart = command;
            } else {
                currentCommand = currentCommand.next = command;
            }
        }
    }
    if (endcb) {
        ////endcb.call(that);

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

};

Executer.prototype.step = function(step, next, that){
    ////step.call(that);
    ////if (next) next.call(that);

    var cmd = new Command(step, that, that);
    if (next) {
        cmd.next = new Command(next, that, that);
        cmd.next.next = this.cmdHead;
    } else {
        cmd.next = this.cmdHead;
    }
    this.cmdHead = cmd;
};


exports.Executer = Executer;
exports.def_execute = def_execute;

