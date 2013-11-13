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
//默认的动作函数，什么也不做，对AST_MacroDef非常有用
def_execute(ast.AST_Node, Empty);
def_execute(ast.AST_Block, function(context){
    this.gen_body(context);
});

function Command(act, astNode, args){
    this.action = act;
    this.args = args || [];
    this.that = astNode;
    this.node = astNode;
    this.next = null;
    this.dependent = false;
}

Command.prototype.go = function(){
    this.action.apply(this.that, this.args);
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
        this.on("end", this.clean.bind(this))
    }
};

//支持debug方式运行
Executer.prototype.start = function(){
    if (this._debugMode){
        var self = this;
        CSDebugger.bootstrap(this, function(){
            /*
            var astnode = self.forward();
            if (astnode) return astnode.pos;
            */
        });
        //恢复执行到下一个断点
        CSDebugger.onResume(function(){
        });
        //单步执行
        CSDebugger.onStepOver(function(){
            var astnode = self.forward();
            var info = {
                "type":astnode.type,
                "first_line":astnode.pos.first_line
            };
            return info;
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
        if (this.cmdHead && this.cmdHead.dependent){
            this.forward();
        }
        return cmd.node;
    }
};


Executer.prototype.genList = function(bodyList, that){
    var st, commandLocalStart, currentCommand;

    //i = 0已经在上面处理
    for (var i = 0; i < bodyList.length; i++){
        st = bodyList[i];

        if (st instanceof ast.AST_MacroDef){
            //macro_def什么也不要做
        } else {
            ////st.execute(this.context);

            var command = new Command(st.execute, st, [this.context]);
            if (!commandLocalStart) {
                currentCommand = commandLocalStart = command;
            } else {
                currentCommand = currentCommand.next = command;
            }
        }
    }

    if (this.cmdHead){
        //!!每一次取一个command时都会从链接头把其取下来，并且修改this.cmdHead使用指向下一个command，这有点向指令指针
        if (currentCommand){//currentCommand可能是没有的，比如只有宏定义的文件
            currentCommand.next = this.cmdHead;
            this.cmdHead = commandLocalStart;
        }
    } else {
        this.cmdHead = commandLocalStart;
    }
    return currentCommand;
};

Executer.prototype.command = function(fun, astNode, dependent){
    var cmd = new Command(fun, astNode);
    if (dependent) cmd.dependent = true;
    cmd.next = this.cmdHead;
    this.cmdHead = cmd;
};

Executer.prototype.insertCommand = function(parentCmd, fun, astNode, dependent){
    var cmd = new Command(fun, astNode);
    if (dependent) cmd.dependent = true;
    cmd.next = parentCmd.next;
    parentCmd.next = cmd;
};

exports.Executer = Executer;
exports.def_execute = def_execute;

