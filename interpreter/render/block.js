var ast = require("../../parse/ast"),
    Scope = require("./scope"),
    def_execute = require("./executer").def_execute;

ast.AST_Block.proto("gen_body", function(print) {
    var body = this.body;//, i = 0;

    /*//{先合并直接输出的内容
    if (body.length > 1) do {
        if (body[i] instanceof ast.AST_Content) {
            for (var j = i + 1; j < body.length;) {
                var nodeItem = body[j];
                if (nodeItem instanceof ast.AST_Content) {
                    body[i].literalValue += body.splice(j, 1)[0].literalValue;
                } else if (nodeItem instanceof ast.AST_SetStmt || nodeItem instanceof ast.AST_MacroDef) {
                    //没有任何输出的语法，可以继续合并
                    j++;
                    continue;
                } else break;
            }
        }
        i++;
    }
    while (i < body.length);
    //}*/

    body.forEach(function(stmt) {
        if (!(stmt instanceof ast.AST_MacroDef)) {
            stmt.execute(print);
        }
    }, this);
});

def_execute(ast.AST_With, function() {
    //expression只能是VariableAccess，这在语法分析阶段已经完成了
    var value = this.expression.calc();
    this.symbolAlias[this.alias.name] = value;

    this.context.enterScope(this); //expression的作用域是父scope，所以对expression分析结束后才改变scope

    //如果表达式对就的节点不存在
    if (value.isEmpty()){
        this.execute();
    }
    this.context.leaveScope();
});


def_execute(ast.AST_MacroDef, function() {
    //宏body的生成没有任何特殊之处，它的参数作用域全部有macroDef处理
    this.context.enterScope(this);
    this.execute(true);
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
