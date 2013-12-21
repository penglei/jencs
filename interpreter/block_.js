var ast = require("../parse/ast"),
    Types = require("./types"),
    def_execute = require("./executer").def_execute;

var CSValue = Types.CSValue;
var HNode = Types.HNode;

ast.AST_Block.proto("gen_body", function(context) {
    for(var i = 0; i < this.body.length; i++){
        this.body[i].execute(context);
    }
});

def_execute(ast.AST_If, function(context){
    var testExpr = this.test.calc();
    if (testExpr.isTrue()){
        this.gen_body(context);
    } else if (this.alternate) {//alternate is ast.AST_Block (or ast.AST_If)
        this.alternate.execute(context);
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

        this.gen_body(context);

        context.leaveScope();
    } else {
        //TODO tips
    }
});

def_execute(ast.AST_Each, function(context){
    var resultVal = this.expression.getSymbolValueNode();
    if (resultVal && resultVal instanceof HNode){
        var scope = context.enterScope(this), itemName = this.variable.name;

        scope.loopVarName = itemName;

        var firsted = false;
        scope.isLoopFirst = true;
        scope.isLoopLast = false;

        var childNode = resultVal.child;
        while(childNode){
            scope.symbolAlias[itemName] = childNode;
            if (!firsted) firsted = true;
            else scope.isLoopFirst = false;
            if (!childNode.next) scope.isLoopLast = true;//TODO gen_body里面可能会改变children，所以这样是不对的

            this.gen_body(context);
            childNode = childNode.next;
        }

        context.leaveScope();
    } else {
        //TODO 警告在一个值上面进行遍历
    }
});

def_execute(ast.AST_Loop, function(context){
    //this.initvar is AST_Symbol;

    //循环在遍历之前已经把start end step全部确定
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
    if (step != 0) while (checkFun()) {
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

        this.gen_body(context);

        start += step;
        //同时要更新aliasSymbolValue的值
        var symbolValue = context.querySymbol(name);
        if (symbolValue instanceof CSValue){
            symbolValue.value = start;
        } else if (symbolValue instanceof HNode){
            symbolValue.setValue(start);
        } else {
            throw new Error("运行时内部错误。循环变量: " + name + " 意外为空");
        }
        //循环亦是不支持写，否则这是一个有歧义的语法，这样做与官方解析引擎是不同的.详见loop测试3
    }

    context.leaveScope();
});

ast.AST_MacroDef.proto("execJump", function(context, _symbolAlias, caller) {
    var scope = context.enterScope(this);
    scope.caller = caller;

    //初始化实参
    for(var name in _symbolAlias){
        if (_symbolAlias.hasOwnProperty(name)){
            if (_symbolAlias[name] instanceof ast.AST_Node){
                scope.symbolAlias[name] = null;
                scope.nonExistParams[name] = _symbolAlias[name];
            } else {
                scope.symbolAlias[name] = _symbolAlias[name];
            }
        }
    }
    this.gen_body(context);
    context.leaveScope();
});

def_execute(ast.AST_Escape, function(context){
    var escapeType = this.escapeType;// "html" || "js" || "url"

    context.pushEscapeFilter(escapeType);
    this.gen_body(context);
    context.popEscapeFilter();
});


def_execute(ast.AST_Program, function(context){
    //context.enterScope(this);

    this.gen_body(context);

    //context.leaveScope();
    this.executer.emit("end");
});
