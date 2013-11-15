var util = require("util");
var events = require("events");
var ast = require("../parse/ast");
var DebuggerServer = require("./debugger").DebuggerServer;

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

    this.endTarget = null;
}

Command.prototype.go = function(){
    this.action.apply(this.that, this.args);
};

//-----
function Executer(){
    events.EventEmitter.call(this);

    this.debugr = null;
    this.context = null;
    this.cmdHead = null;
}

util.inherits(Executer, events.EventEmitter);

Executer.prototype.clean = function(){
    this.context = null;
    this.debugr = null;
    this.cmdHead = null;
    ast.AST_Node.proto("context", null);
    ast.AST_Node.proto("executer", null);
};

Executer.prototype.run = function(astInstance, context, debugr){
    this.debugr = debugr;
    this.context = context;

    ast.AST_Node.proto("executer", this);
    ast.AST_Node.proto("context", context);

    astInstance.execute(context);//AST_Program start

    //用事件的方式可以统一debug模式和普通模式
    this.on("end", this.clean.bind(this))
};

//支持debug方式运行
Executer.prototype.start = function(){
    if (!this.debugr){//没有debugr就直接运行，否则运行的过程由debugr控制
        while(this.cmdHead) this.forward();
    }
};

Executer.prototype.forward = function(stepin){
    var cmd = this.cmdHead;
    if (cmd){
        this.cmdHead = cmd.next;
        //执行command的动作，可以在这里卡住
        cmd.go();

        if (cmd.node instanceof ast.AST_CSDebugger){
            return this.cmdHead && this.cmdHead.node;
        }
        while (this.cmdHead && this.cmdHead.dependent){
            var cmdContinue = this.cmdHead;
            this.cmdHead = this.cmdHead.next;
            cmdContinue.go();
        }

        if (cmd.endTarget && !stepin) {
            while (cmd.endTarget && this.cmdHead != cmd.endTarget.target){
                var cmdContinue = this.cmdHead;
                this.cmdHead = this.cmdHead.next;
                cmdContinue.go();
                if (cmdContinue.node instanceof ast.AST_CSDebugger) return this.cmdHead && this.cmdHead.node;
            }
            this.forward();
        }

        return this.cmdHead && this.cmdHead.node;
    }
};

Executer.prototype.resume = function(){
    var cmd = this.cmdHead;
    while(cmd){
        this.cmdHead = cmd.next;
        cmd.go();
        cmd = this.cmdHead;
        if (cmd && cmd.node instanceof ast.AST_CSDebugger){
            return cmd.node;
        }
    }
};


Executer.prototype.genList = function(bodyList, that){
    var st, commandLocalStart, currentCommand;

    //i = 0已经在上面处理
    for (var i = 0; i < bodyList.length; i++){
        st = bodyList[i];

        if (st instanceof ast.AST_MacroDef){
            //AST_MacroDef不要生成command，因为它不能自已执行，需要被调用后才执行
        } else {
            ////st.execute(this.context);

            var command = new Command(st.execute, st, [this.context]);
            if (!commandLocalStart) {
                currentCommand = commandLocalStart = command;
            } else {
                currentCommand = currentCommand.next = command;
            }

            if (st instanceof ast.AST_Include || st instanceof ast.AST_MacroCall){
                //添加一个include结束的标志
                var endFlagCmd = new Command(Empty, st);
                command.endTarget = {
                    name: st.targetName,
                    target: endFlagCmd
                };
                currentCommand = currentCommand.next = endFlagCmd;
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

