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
ast.AST_BinaryExpr.proto("&&", function() {});

ast.AST_BinaryExpr.proto("||", function(opts) {
    var output = this.output;

    this.left.expr_gen_val();

    if (isLogic(this.left)) { //左边还是逻辑运算，递归处理开始
        //注意，|| 表达式这里直接计算右值，是因为后面有break，当目标程序运行到这里说明是必须要判断右值是不是为true
        //即使左边表达式有一项是常量为true的，也有break;语句使其跳过
    } else { //左值是其它表达式，到最左树叶了
        var leftVal = this.left.value;

        if (leftVal.get() == Void) {
            //这个时候左表达式相当于没有用
        } else if (leftVal.useLiterals) {
            //判断是不是为真，为真就停止分析右值
            if (leftVal.get()) {
                this.value = leftVal;
                //说明该 “或”表达式为真，后续可以不用求右值了
            }
            //这个时候左值也相当于无用
        } else {
            output.echo("if ").with_parens(function() {
                output.echo("cs_val_istrue").with_parens(leftVal.get().name);
            }).echo(" break").endline();
        }
    }

    this.right.expr_gen_val();
    var rightVal = this.right.value.get();
    if (isLogic(this.left)) {
        //
    } else {
        if (rightVal == Void) {
            //右值没有用，用左值
            this.value = this.left.value;
        } else if (this.right.value.useLiterals) {
            if (rightVal) {
                this.value = this.right.value;
            } else {
                //右值是false，用左值
                this.value = this.left.value;
            }
        } else {
            //XXX 右值会不会有逻辑表达式呢..?

            //如果右值运算为true，就break，说明当前condition为true
            output.echo("if ").with_parens(function() {
                output.echo("cs_val_istrue").with_parens(rightVal.name);
            }).echo(" break").endline();
        }
    }

    if (opts && opts.logicExprStart) { //这是开始
        if (opts.useBoolResult) {
            output.echo(opts.resultValueSymbol.name, " = false").endline();
            this.value = opts.resultValueSymbol;
        } else {
            output.echo("CSVAL_SET_INT").with_parens(opts.resultValueSymbol.name, ", 0").endline();
            this.value = CreateValue(Value.T.CSValue, opts.resultValueSymbol);
        }
    }

});
