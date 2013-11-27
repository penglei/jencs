var util = require("util");
var events = require("events");
var ast = require("../parse/ast");

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
    this.type = astNode.type;
    this.action = act;
    this.args = args || [];
    this.pos = astNode.pos;
    this.node = astNode;
    this.next = null;
    this.dependent = false;

    this.endTarget = null;
}

Command.prototype.go = function(){
    this.action.apply(this.node, this.args);
};

//-----
function Executer(){
    events.EventEmitter.call(this);

    this.debugr = null;
    this.context = null;
    this.cmdHead = null;

    //用事件的方式可以统一debug模式和普通模式
    this.on("end", this.clean.bind(this))
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
        var tomatchEndCmd = cmd;
        //执行掉当前的指令,执行前要把cmdHead指向下一个cmd，这样才可以保证内部对cmdHead的个改不会出问题
        this.cmdHead = cmd.next;
        cmd.go();

        //当前执行的是include或macrocall
        //如果不是单步执行，在遇到哨兵或者debug指令时要停止
        //区分是否要全部执行完就是看当前cmd有没有endTarget，这相当于一个哨兵(在genList的时候放置的)
        if (!stepin && tomatchEndCmd.endTarget){

            while (this.cmdHead && this.cmdHead != tomatchEndCmd.endTarget.target){
            //@2 如果下一条指令是debug，就*直接(无论是单步还是resume)*返回它为下一次要执行的指令
                if (this.cmdHead.node instanceof ast.AST_CSDebugger) break;
                cmd = this.cmdHead;
                this.cmdHead = cmd.next;
                cmd.go();
            }
            //这个时候不是debug break出来的，而是哨兵cmd被发现，执行它
            if (this.cmdHead && this.cmdHead == tomatchEndCmd.endTarget.target) {
                cmd = this.cmdHead;
                this.cmdHead = this.cmdHead.next;
                cmd.go();
            }
        }

        //1.如果一条指令是内容输出，执行它 TODO 如果是内容，也执行它
        //2.如果下一条指令只是cmd的副作用(suffix)，也执行它
        while(this.cmdHead && (this.cmdHead.node instanceof ast.AST_Content || this.cmdHead.dependent)){
            var continueCmd = this.cmdHead;
            this.cmdHead = continueCmd.next;
            continueCmd.go();
        }
    }
};

//恢复执行:从当前断点处继续执行，直接遇到下一个断点
//标准的作法是不断调用forward直到遇到debug，但那样效率比较低，在这用一个循环代替
Executer.prototype.resume = function(){
    var curcmd = this.cmdHead;
    while (curcmd) {
        this.cmdHead = curcmd.next;
        curcmd.go();

        curcmd = this.cmdHead;
        if (curcmd && curcmd.node instanceof ast.AST_CSDebugger){//这里保证debug cmd还没有执行
            break;
        }
    }
};

Executer.prototype.isRunning = function(){
    return this.cmdHead ? true : false;
};

Executer.prototype.getExecutePos = function(){
    if (!this.cmdHead) return;
    var cmd = this.cmdHead;
    var executePos = {
        "stype": cmd.node.type,
        "column": cmd.pos.column ||cmd.pos.first_column,
        "line": cmd.pos.line || cmd.pos.first_line,//first_line在某些时候不是最好的
        "sourceId": cmd.pos.fileid
    };
    return executePos;
};

Executer.prototype.allowPaused = function(){
    if (this.cmdHead && this.cmdHead.node instanceof ast.AST_CSDebugger){
        return true;
    }
    return false;
};


/**
 * 生成bodyCommands
 */
Executer.prototype.genList = function(bodyList){
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
                //添加一个include结束的标志(哨兵)，这样单步执行时才知道到什么时候结束
                var endFlagCmd = new Command(Empty, st);
                endFlagCmd.type = st.type + ".Sentinel";
                //哨兵也只是一个副作用，它不能单独存在
                endFlagCmd.dependent = true;
                command.endTarget = {
                    name: st.targetName,//TODO targetName不是都有的
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
    if (dependent) {
        cmd.dependent = true;
    } else if (astNode instanceof ast.AST_MacroDef){
        //只有单独存在的cmd才需要特殊处理
        cmd.pos = util._extend({}, astNode.pos);
        cmd.pos.line = cmd.pos.last_line;
        cmd.pos.column = cmd.pos.last_column;
    }
    cmd.next = parentCmd.next;
    parentCmd.next = cmd;
};

exports.Executer = Executer;
exports.def_execute = def_execute;

