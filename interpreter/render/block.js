var ast = require("../../parse/ast"),
    CSValue = require("./scope").CSValue,
    HNode = require("hdf2json").HNode,
    def_execute = require("./executer").def_execute;

ast.AST_Block.proto("gen_body", function(print) {
    this.body.forEach(function(stmt) {
        if (!(stmt instanceof ast.AST_MacroDef)) {
            stmt.execute(print);
        }
    }, this);
});

def_execute(ast.AST_If, function(print){
    if (this.test.calc().isTrue()){
        this.gen_body(print);
    } else if (this.alternate) {//alternate is ast.AST_Block
        this.alternate.gen_body(print);
    }
});

def_execute(ast.AST_Alt, function(print){
    var echoValue = this.expression.calc();
    if (echoValue.isTrue()){
        print(echoValue.getString());
    } else {
        this.gen_body(print);
    }
});


def_execute(ast.AST_With, function(print) {
    //expression只能是VariableAccess，这在语法分析阶段已经完成了
    var resultVal = this.expression.getSymbolValueNode();
    //如果表达式对就的节点不存在。(或者没有值，这与标准和cs解析引擎行为会有差异）
    if (resultVal){
        this.context.enterScope(this); //必须先进入scope，才可以使用symbolAlias

        this.symbolAlias[this.alias.name] = resultVal;

        this.gen_body(print);

        this.context.leaveScope();
    } else {
        //TODO tips
    }
});

def_execute(ast.AST_Each, function(print){
    var resultVal = this.expression.getSymbolValueNode();
    if (resultVal && resultVal instanceof HNode){
        this.context.enterScope(this);

        this.symbolAlias[this.variable.name] = resultVal;
        this.gen_body(print);

        this.context.leaveScope();
    }
});

def_execute(ast.AST_Loop, function(print){
    //this.initvar is AST_Symbol;

    //循环在遍历之前已经把start end step全部确定
    var start = this.initVarSymbol.initValue.calc().getNumber();
    var end = this.endexpr.calc().getNumber();
    var step = this.step.calc().getNumber();


    var name = this.initVarSymbol.name;
    var aliasSymbolValue = new CSValue(CSValue.Number, start);

    this.context.enterScope(this);

    this.symbolAlias[name] = aliasSymbolValue;//setSymbolInCurrentScope

    //循环次数是固定的
    while (start <= end) {
        this.gen_body(print);
        start += step;

        //同时要更新aliasSymbolValue的值
        //为什么要这样更新，详见loop测试用例
        var symbolValue = this.context.querySymbol(name);
        if (symbolValue instanceof CSValue){
            symbolValue.value = start;
        } else if (symbolValue instanceof HNode){
            symbolValue.setValue(start);
        }
        //但是，这是一个有歧义的语法，这样做与官方解析引擎是不同的.详见loop测试3
    }


    this.context.leaveScope();
});

def_execute(ast.AST_MacroDef, function(print) {
    //宏body的生成没有任何特殊之处，它只需要进入自己的scope，并且不用设置symbolAlias，因为这些已经由macro_call处理
    this.context.enterScope(this);
    this.execute(print);
    this.context.leaveScope();
});

def_execute(ast.AST_Escape, function(print){
    var escapeType = this.escapeType;
    var self = this;
    function _print(str){
        if (escapeType == 'html'){
            print(str.replace(/&/g,'&amp;').replace(/>/g,'&gt;').replace(/</g,'&lt;').replace(/"/g,'&quot;'));
        } else if (escapeType == 'url'){
            print(encodeURIComponent(str));
        } else if (escapeType == 'js'){
            print(str.replace(/\\/, "\\\\").replace(/"/g, '\\"').replace(/'/g, "\\'"));
        }
    }
    this.gen_body(_print);
});

def_execute(ast.AST_Content, function(print){
    print(this.literalValue);
});

def_execute(ast.AST_Program, function(print) {
    this.context.enterScope(this);
    this.gen_body(print);
    this.context.leaveScope();
});
