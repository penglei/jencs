var util = require("util");
var events = require("events");
var ast = require("../parse/ast");
var ClearSilverParser = require("../parse/clearsilver").Parser;
var Walker = require("./walker").Walker;

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
function Executer(engine){
    events.EventEmitter.call(this);

    this.debugr = null;
    this.context = null;
    this.cmdHead = null;
    this._engine = engine;

    this._active = true;
    this._breakpoints = [];
    this._breakpiontIdUpper = 1;
    this._watchExpressions = {};
    this._watchObjectIdUpper = 1;

    /** @type {!Object.<Integer, Object>} */
    this._callFramesScope = {};
}

util.inherits(Executer, events.EventEmitter);

Executer.prototype.clean = function(){
    this.context = null;
    this.debugr = null;
    this.cmdHead = null;
    ast.AST_Node.proto("context", null);
    ast.AST_Node.proto("executer", null);
};

Executer.prototype.getCallFrames = function(){
    var scopeStack = this.context.scopeStack;
    var callFrames = [],
        callStack = [scopeStack[0]];//0是Program

    //先把with, each, loop形成的scope stack 去掉
    for(var i = 1; i < scopeStack.length; i++) {
        if (scopeStack[i].astNode instanceof ast.AST_MacroDef) callStack.push(scopeStack[i]);
    }

    var callDeep = callStack.length;
    if (callDeep == 1) {
        var scope = callStack[0];
        //顶层program
        var posInfo = this.getExecutePos();
        var callFrameId = 1;
        var frameInfo = {
            "callFrameId": callFrameId,
            "functionName": "(Program)",
            "location":{
                "columnNumber":posInfo.column,
                "lineNumber":posInfo.line - 1,
                "scriptId":posInfo.scriptId
            },
            "scopechain":[/*{
                className,
                objectId: "scope:0:0",
            }*/]
        };
        callFrames.push(frameInfo);
        this._callFramesScope[callFrameId] = scope;
        return callFrames;
    }

    var id, pos, scope;
    var functionName, columnNumber, scriptId;
    for(id = 0; id < callDeep; id++){
        scope = callStack[id];
        if (id == 0){//Top Level Program
            pos = callStack[1].caller.pos;//在这里callStack[1]肯定是存在的
            functionName = "(Program)";
            columnNumber = pos.first_column;
            lineNumber = pos.first_line;
            scriptId = pos.fileid;
        } else {
            functionName =  scope.astNode.id;//macroDef name
            if (id == callDeep - 1){
                var posInfo = this.getExecutePos();
                columnNumber = posInfo.column;
                lineNumber = posInfo.line;
                scriptId = posInfo.scriptId;
            } else {
                var exePos = callStack[id + 1].caller.pos;
                columnNumber = exePos.first_column;
                lineNumber = exePos.first_line;
                scriptId = exePos.fileid;
            }
        }

        var callFrameId = id + 1;
        var frameInfo = {
            "callFrameId": callFrameId,
            "functionName": functionName,
            "location": {
                "columnNumber":columnNumber,
                "lineNumber":lineNumber - 1,//解析器返回的line是从1开始，debugger client需要从0开始，所以需要减1
                "scriptId": scriptId
            },
            "scopechain":[/*{
                className,
                objectId: "scope:0:0",
            }*/]
        };
        this._callFramesScope[callFrameId] = scope;
        callFrames.unshift(frameInfo);
    }

    return callFrames;
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
        this._active = false;
        var curcmd = this.cmdHead;
        while (curcmd) {
            this.cmdHead = curcmd.next;
            curcmd.go();
            curcmd = this.cmdHead;
        }
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
            //
                if (this._canBreak(this.cmdHead.node)) break;
                //if (this.cmdHead.node instanceof ast.AST_CSDebugger && this._active) break;

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
Executer.prototype.resume = function(flag){
    var curcmd = this.cmdHead;
    while (curcmd) {
        this.cmdHead = curcmd.next;
        curcmd.go();

        curcmd = this.cmdHead;

        if (curcmd && curcmd.node && this._canBreak(curcmd.node)) break;
        //这里保证debug cmd还没有执行
        //if (curcmd && curcmd.node instanceof ast.AST_CSDebugger && this._active) break;

        if (flag == 2){//运行到宏后面的语句
            //TODO 需要运行到当前哨兵位置
            if (curcmd && curcmd.type != "MacroReturn"){
                this.forward(true);
                break;
            }
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
        "line": cmd.pos.line || cmd.pos.first_line,
        "scriptId": cmd.pos.fileid
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
        //macrodef结束时添加一个返回命令，它的位置相当于<?cs /def?>的位置
        cmd.pos = util._extend({}, astNode.pos);
        cmd.pos.line = cmd.pos.last_line;
        cmd.pos.column = cmd.pos.last_column;
        cmd.type = "MacroReturn";
    }
    cmd.next = parentCmd.next;
    parentCmd.next = cmd;
    return cmd;
};

Executer.prototype._canBreak = function(node) {
    if (!this._active) return false;
    if (this.cmdHead && this.cmdHead.type == "MacroReturn" || this.cmdHead.dependent) return false;
    if (node instanceof ast.AST_CSDebugger) return true;
    for(var i = 0; i < this._breakpoints.length; i++){
        if (this._breakpoints[i].astNode == node){
            return true;
        }
    }
    return false;
};

Executer.prototype.setBreakpointsActive = function(active){
    //console.log("setBreakpointsActive:" + active);
    this._active = !!active;
};

Executer.prototype._setBreakpoint = function(node, line){
    var i, breakobj;
    for(var i = 0; i < this._breakpoints.length; i++){
        breakobj = this._breakpoints[i];
        if (breakobj.astNode == node){
            return breakobj;
        }
    }
    var id = this._breakpiontIdUpper++;//id不会特别多，这种是保持唯一性的最简单方式
    breakobj = {
        "astNode": node,
        "line": line,
        "active": true,
        "id": id
    }
    this._breakpoints.push(breakobj);
    return breakobj;
};

Executer.prototype.requestBreakpoint = function (rawPosition) {
    var fileid = rawPosition.scriptId;
    var sourceObj = this._engine.getSourceObjectById(fileid);

    if (!sourceObj || !sourceObj.astTree) return;

    var line = rawPosition.lineNumber + 1;
    //console.log("To find line:" + line);
    var pos, brkpos, brkNode;
    sourceObj.astTree.walk(new Walker(function(node, descend){
        if (!node.pos) return;//AST_Program需要继续访问body部分
        if (node instanceof ast.AST_Block){
            pos = node.pos;
            if (line >= pos.first_line && line <= pos.last_line){
                /*
                if (line in node.startPos) {
                    点击的是block开始标签, finded = true
                } else if (line in node.endPos) {
                    if (node instanceof ast.AST_MacroDef) {
                        //点击的是def的结束标签
                    } else {
                    }
                    点击的是block结束标签 line = node.endPos.last_line + 1
                } else {
                    点击的是block的body内的
                }
                */
                //console.log("BlockLineRange:" + node.pos.first_line + "->" + node.pos.last_line);
                //断点暂时属于该block
                brkpos = pos;
                brkNode = node;
                return false;//找更精确的位置, 需要继续遍历body
            }
        } /*else if (node instanceof ast.AST_Content) {
            pos = node.pos;
            if (line >= pos.first_line && line <= pos.last_line && pos.last_line - pos.first_line > 1){
                console.log("ContentLineRange:" + node.pos.first_line + "->" + node.pos.last_line);
                brkpos = pos;
                brkNode = node;
            }
        } */else if (node instanceof ast.AST_Statement){
            pos = node.pos;
            if (line >= pos.first_line && line <= pos.last_line){
                //console.log("StmtLineRange:" + node.pos.first_line + "->" + node.pos.last_line);
                brkpos = pos;
                brkNode = node;
            }
        }
        return true;//不需要遍历表达式节点
    }));
    if (brkNode && brkNode != sourceObj.astTree && brkpos) {
        var breakobj = this._setBreakpoint(brkNode, line);
        return {
            "id": breakobj.id,
            "pos": brkpos,
            "type": brkNode.type
        };
    }
};

Executer.prototype.removeBreakpoint = function(breakpointId){
    for (var i = 0; i < this._breakpoints.length; i++){
        if (this._breakpoints[i].id == breakpointId) {
            this._breakpoints.splice(i, 1);
            return true;
        }
    }
};

Executer.prototype.evaluateExpr = function (expression, callFrameId) {
    var tempCsParser = new ClearSilverParser();
    try{
        var astProgram;
        if (!this._watchExpressions[expression]){
            astProgram = tempCsParser.parse("<?cs var:" + expression + " ?>");
        } else {
            astProgram = this._watchExpressions[expression];
        }

        //var objectId = this._watchObjectIdUpper++;

        var exprStmt, expr;
        if (astProgram.body && (exprStmt = astProgram.body[0])){
            expr = exprStmt.argument;
            //先把scope切换到指定的状态，以获取正确的变量值
            var scope = this._callFramesScope[callFrameId];
            if (!scope){
                //TODO throw Error
            } else {
                this.context.setStartfromScope(scope);

                /*
                if (expr instanceof ast.AST_VariableAccess){
                    //对于 VariabelAccess可以不用calc，而是获得它的节点
                } else {
                }
                */

                var result = expr.calc();
                this.context.resetStartfromScope();
            }
            return result.value;
        }
    } catch (e){
        return new Error("parse and evalue error");
    }
}

exports.Executer = Executer;
exports.def_execute = def_execute;

