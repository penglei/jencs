var ast = require("../parse/ast"),
    Scope = require("./scope"),
    Util = require("./util"),
    Interface = require("./interface");

//--------------------------//{
var Void = Interface.Void;
var def_genCode = Interface.def_genCode;
var isSymbol = Interface.isSymbol;
var isLogic = Interface.isLogic;
var Value = Interface.Value;
var CreateValue = Interface.CreateValue;
//--------------------------//}

def_genCode(ast.AST_If, function() {
    var output = this.output;
    var context = this.context;

    if (this.test instanceof ast.AST_VariableAccess){
        createTestVariable.call(this);
        return;
    }

    var resultValueSymbol;
    result_value_symble = context.newSymbol("bool", "", "true");
    output.ob_start();
    //计算表达式的值
    output.echo("do ").with_braces(function(){
        //var block = context.enterBlock();//把当前if_block作为一个临时的block，用来定义变量
        var block = context.enterTempBlock();

        output.indent().newline();

        output.ob_start();

        this.test.expr_gen_val({
            resultValueSymbol: result_value_symble,
            useBoolResult: true,
            logicExprStart: true
        });

        var cbuf = output.ob_get();
        output.ob_end();

        output.createDefinition(block);
        output.pushCodes(cbuf);

        output.undent();

        context.leaveBlock();
    }, this).echo(" while(0)").endline();
    var ifStatmemt = output.ob_get();
    output.ob_end();


    if (this.test.value.useLiterals) {
        //判断这个值是不是肯定为true
        if (this.test.value.get()){
            output.newline();
            output.pushCodes(this.gen_body());
        }
    } else {
        output.pushCodes(ifStatmemt);
        output.echo("if ").with_parens(function() {
            output.echo(this.test.value.name);
            /*
            var testVal = this.test.value.get();
            //output.echo(testVal.name, ".value.lval");
            */
        }, this).with_braces(function() {

            output.newline().indent();
            output.pushCodes(this.gen_body());
            output.undent();

        }, this);
    }

    if (this.alternate){
        output.echo(" else ").with_braces(function(){
            output.line();
            if (this.alternate instanceof ast.AST_If){
                this.alternate.gen_code();
            } else if (this.alternate instanceof ast.AST_Block){
                output.pushCodes(this.alternate.gen_body());
            }
            output.undent();
        }, this).newline();
    } else {
        output.newline();
    }
});

function createTestVariable(){
    var output = this.output;
    var context = this.context;

    this.test.expr_gen_val();
    if (this.test.value.get() == Void){
        //do nothing
    } else if (this.test.value.useLiterals){
        if (this.test.value.get()){
            output.pushCodes(this.gen_body());
        }
    } else {
        output.echo("if ").with_parens(function() {
            output.echo("cs_val_istrue").with_parens(this.test.value.get().name);
        }, this).with_braces(function(){
            output.indent().newline();
            output.pushCodes(this.gen_body());
            output.undent();
        }, this).newline();
    }
}
