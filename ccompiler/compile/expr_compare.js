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

ast.AST_BinaryExpr.proto(">", function() {});

ast.AST_BinaryExpr.proto("<", function() {});

ast.AST_BinaryExpr.proto("<=", function() {});

ast.AST_BinaryExpr.proto(">=", function() {});

ast.AST_BinaryExpr.proto("!=", function(opts) {
    var output = this.output;
    this["=="].call(this, opts);
    //对取得的址求反
    output.echo(this.value.get().name, ".value.lval = !", this.value.get().name, ".value.lval").endline();
});

ast.AST_BinaryExpr.proto("==", function(opts) {
    var output = this.output;
    var context = this.context;

    var leftVal = this.left.value.get(),
        rightVal = this.right.value.get();
    if (leftVal == Void || rightVal == Void) {
        if (leftVal == Void && rightVal == Void) {
            this.value = CreateValue(Value.T.Number, 1); //用Number代替
        } else if (leftVal == Void) {
            if (this.right.value.useLiterals) {
                if (rightVal === "" || rightVal === 0) {
                    this.value = CreateValue(Value.T.Number, 1);
                } else {
                    this.value = CreateValue(Value.T.Number, 0);
                }
            } else { //CSValue
                //需要对右值进行判断
                this.value = CreateValue(Value.T.CSValue);
            }
        } else if (rightVal == Void) {}
    } else if (this.left.value.useLiterals || this.right.value.useLiterals) {
        if (this.left.value.useLiterals && this.right.value.useLiterals) {
            //this.
        }
    } else {
        if (opts.useBoolResult){
            output.echo(opts.resultValueSymbol.name, " = ");
            output.echo("cs_isEqual").with_parens(function() {
                output.echo("&", leftVal.name).comma();
                output.echo("&", rightVal.name);
            }).endline();
            this.value = opts.resultValueSymbol;

        } else {
            var _cSymbolValue = opts.resultValueSymbol;
            output.echo("CSVAL_SET_INT").with_parens(function() {
                output.echo(_cSymbolValue.name).comma();
                output.echo("cs_isEqual").with_parens(function() {
                    output.echo("&", leftVal.name).comma();
                    output.echo("&", rightVal.name);
                });
            }).endline();

            this.value = CreateValue(Value.T.CSValue, _cSymbolValue);
        }
    }
});

