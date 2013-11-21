var ast = require("../parse/ast"),
    Types = require("./types"),
    def_execute = require("./executer").def_execute;

var CSValue = Types.CSValue;
var HNode = Types.HNode;

ast.AST_Block.proto("gen_body", function(context) {
    //对AST_MacroDef的排除放在genList函数体里
    this.executer.genList(this.body, this);
});


def_execute(ast.AST_If, function(context){
    var testExpr = this.test.calc();
    if (testExpr.isTrue()){
        this.gen_body(context);
    } else if (this.alternate) {
        this.executer.command(this.alternate.execute, this.alternate);
        //this.alternate.execute(context);
    }
});

def_execute(ast.AST_Alt, function(context){
    var echoValue = this.expression.calc();
    if (echoValue.isTrue()){
        context.output(echoValue.getString(), this);
    } else {
        this.gen_body(context);
    }
});

def_execute(ast.AST_With, function(context) {
    //expression只能是VariableAccess，这在语法分析阶段已经完成了
    var resultVal = this.expression.getSymbolValueNode();
    //如果表达式对应的节点不存在。(或者没有值，这与标准和cs解析引擎行为会有差异）
    if (resultVal && this.expression instanceof ast.AST_VariableAccess){
        var scope = context.enterScope(this); //必须先进入scope，才可以使用symbolAlias

        scope.symbolAlias[this.alias.name] = resultVal;

        //所有body后面需要执行其它语句的逻辑都需要封装成函数
        var lastCommand = this.executer.genList(this.body, this);
        this.executer.insertCommand(lastCommand,  function(){
            context.leaveScope();
        }, this, true);
    } else {
        //TODO tips this.executer.step....
    }
});


def_execute(ast.AST_Each, function(context){
    var resultVal = this.expression.getSymbolValueNode();
    if (resultVal && resultVal instanceof HNode){
        var scope = context.enterScope(this),
            executer = this.executer,
            itemName = this.variable.name;

        scope.loopVarName = itemName;

        var firsted = false;
        scope.isLoopFirst = true;
        scope.isLoopLast = false;

        var childNode = resultVal.child;//this is firstchild
        function eachStep(){
            scope.symbolAlias[itemName] = childNode;
            if (!firsted) firsted = true;
            else scope.isLoopFirst = false;
            //TODO gen_body里面可能会改变children，所以这样判断isLoopFirst可能是不对的，需要在last函数里动态判断
            if (!childNode.next) scope.isLoopLast = true;

            var lastCommand = executer.genList(this.body, this);
            executer.insertCommand(lastCommand, eachStepSuffixInEach, this);
        }

        function eachStepSuffixInEach(){
            childNode = childNode.next;
            if (childNode){
                executer.command(eachStep, this, true);//下一次执行eachStep时，它是被当前command依赖的
            } else {
                //在循环完后要leaveScope，比较直接的方式是放在整个循环后
                //放在这个位置也是可以的，因为执行完下面这一句后就到整个循环后了
                //也就不再需要一个command来封装执行了
                context.leaveScope();
            }
        }

        eachStep.call(this);
    } else {
        //TODO 警告在一个值上面进行遍历
    }
});

def_execute(ast.AST_Loop, function(context){
    var start = this.initVarSymbol.initValue.calc().getNumber();
    var end = this.endexpr.calc().getNumber();
    var step = this.step.calc().getNumber();

    var name = this.initVarSymbol.name;
    var aliasSymbolValue = new CSValue(CSValue.Number, start);

    var scope = context.enterScope(this);

    scope.symbolAlias[name] = aliasSymbolValue;//setSymbolInCurrentScope
    scope.loopVarName = name;

    var checkFun;
    //循环次数是固定的
    if (step > 0){
        //start <= end
        checkFun = function(){
            return start <= end;
        }
    } else if (step < 0) {
        //start >= end
        checkFun = function(){
            return start >= end;
        }
    } else {
        //TODO notice 不要执行
    }
    var firsted = false;
    scope.isLoopFirst = true;
    scope.isLoopLast = false;
    if (step == 0) return;

    var executer = this.executer;

    function eachStepSuffixInLoop(){
        start += step;
        //同时要更新aliasSymbolValue的值
        //循环变量是不支持写，否则这是一个有歧义的语法，这样做与官方解析引擎是不同的.详见loop测试3
        var symbolValue = context.querySymbol(name);
        if (symbolValue instanceof CSValue){
            symbolValue.value = start;
        } else if (symbolValue instanceof HNode){
            symbolValue.setValue(start);
        } else {
            throw new Error("运行时内部错误。循环变量: " + name + " 意外为空");
        }
        executer.command(eachStep, this, true);
    }

    function eachStep(){

        if (checkFun()) {
            //为第一次循环打一个标记，留给first函数使用
            if (!firsted) firsted = true;
            else scope.isLoopFirst = false;//以后每次循环要置为false

            var next = start + step;
            if (step > 0){
                if (next > end) {
                    scope.isLoopLast = true;
                }
            } else {
                if (next < end){
                    scope.isLoopLast = true;
                }
            }

            var lastCommand = executer.genList(this.body, this);
            executer.insertCommand(lastCommand, eachStepSuffixInLoop, this);
        } else {
            context.leaveScope();//类似于each
        }
    }

    eachStep.call(this);
});


ast.AST_MacroDef.proto("execJump", function(context, _symbolAlias) {
    var scope = context.enterScope(this);

    //初始化实参
    for (var name in _symbolAlias){
        if (_symbolAlias.hasOwnProperty(name)){
            if (_symbolAlias[name] instanceof ast.AST_Node){
                scope.symbolAlias[name] = null;
                scope.nonExistParams[name] = _symbolAlias[name];
            } else {
                scope.symbolAlias[name] = _symbolAlias[name];
            }
        }
    }
    var lastCommand = this.executer.genList(this.body, this);
    this.executer.insertCommand(lastCommand, function(){
        context.leaveScope();
    }, this, false);//宏执行末尾添加一个独立的command，方便调试的时候更直观
});

def_execute(ast.AST_Escape, function(context){
    var escapeType = this.escapeType;// "html" || "js" || "url"

    context.pushEscapeFilter(escapeType);
    var lastCommand = this.executer.genList(this.body, this);
    this.executer.insertCommand(lastCommand, function(){
        context.popEscapeFilter();
    }, this, true);
});

def_execute(ast.AST_Program, function(context){
    var executer = this.executer;

    if (!this.body.length) {
        executer.emit("end");
        return;
    }

    context.enterScope(this);

    //所有body后面需要执行其它语句的逻辑都需要封装成函数
    var lastCommand = executer.genList(this.body, this);

    executer.insertCommand(lastCommand, function(){
        context.leaveScope();
        executer.emit("end");
    }, this, true);

    executer.start();
});
