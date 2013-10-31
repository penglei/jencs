var ast = require("../parse/ast"),
    CSValue = require("./scope").CSValue,
    HNode = require("./hdf").HNode,
    def_execute = require("./executer").def_execute;

def_execute(ast.AST_Block, function(context){
    this.gen_body(context);
});

ast.AST_Block.proto("gen_body", function(context) {
    this.body.forEach(function(stmt) {
        if (!(stmt instanceof ast.AST_MacroDef)) {
            stmt.execute(context);
        }
    }, this);
});

def_execute(ast.AST_If, function(context){
    if (this.test.calc().isTrue()){
        this.gen_body(context);
    } else if (this.alternate) {//alternate is ast.AST_Block
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
    //如果表达式对就的节点不存在。(或者没有值，这与标准和cs解析引擎行为会有差异）
    if (resultVal){
        context.enterScope(this); //必须先进入scope，才可以使用symbolAlias

        this.symbolAlias[this.alias.name] = resultVal;

        this.gen_body(context);

        context.leaveScope();
    } else {
        //TODO tips
    }
});

def_execute(ast.AST_Each, function(context){
    var resultVal = this.expression.getSymbolValueNode();
    if (resultVal && resultVal instanceof HNode){
        context.enterScope(this);

        for(var i = 0; i < resultVal.children.length; i++){
            this.symbolAlias[this.variable.name] = resultVal.children[i];
            this.gen_body(context);
        }

        context.leaveScope();
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

    context.enterScope(this);

    this.symbolAlias[name] = aliasSymbolValue;//setSymbolInCurrentScope

    var checkFun;
    //循环次数是固定的
    if (step > 0){
        //start <= end
        checkFun = function(){
            return start <= end
        }
    } else if (step < 0) {
        //start >= end
        checkFun = function(){
            return start >= end
        }
    } else {
        //TODO notice 不要执行
    }

    if (step != 0)
    while (checkFun()) {
        start += step;
        this.gen_body(context);

        //同时要更新aliasSymbolValue的值
        //为什么要这样更新，详见loop测试用例
        var symbolValue = context.querySymbol(name);
        if (symbolValue instanceof CSValue){
            symbolValue.value = start;
        } else if (symbolValue instanceof HNode){
            symbolValue.setValue(start);
        } else {
            throw new Error("运行时内部错误。循环变量: " + name + " 意外为空");
        }
        //但是，这是一个有歧义的语法，这样做与官方解析引擎是不同的.详见loop测试3
    }


    context.leaveScope();
});

def_execute(ast.AST_MacroDef, function(context, _symbolAlias) {
    context.enterScope(this);

    //初始化实参
    for(var name in _symbolAlias){
        if (_symbolAlias.hasOwnProperty(name)){
            this.symbolAlias[name] = _symbolAlias[name];
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

def_execute(ast.AST_Content, function(context){
    context.output(this.literalValue, this);
});

def_execute(ast.AST_Program, function(context) {
    context.enterScope(this);
    this.gen_body(context);
    context.leaveScope();
});
