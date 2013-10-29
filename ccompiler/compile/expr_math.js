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

ast.AST_BinaryExpr.proto("+", function(opts) {
    var output = this.output;
    var context = this.context;
    if (this.left.value.get() == Void || this.right.value.get() == Void) {
        //当前值就是左值或者右值
        //如果两者都是Void，那也是这个逻辑，只不过当前表达式的值是Void，这在使用表达式的任何地方都需要进行判断的
        if (this.left.value.get() == Void) {
            this.value = this.right.value;
        } else {
            this.value = this.left.value;
        }
    } else if (this.left.value.useLiterals && this.right.value.useLiterals) {
        if (this.left.value.isNumber() && this.right.value.isNumber()) {
            var val = parseInt(this.left.value.get()) + parseInt(this.right.value.get());
            this.value = CreateValue(Value.T.Number, val);
        } else {
            var val = this.left.value.get() + "" + this.right.value.get();
            this.value = CreateValue(Value.T.String, val);
        }
    } else {
        var addOperName, _cSymbolValue = context.newTempValueSymbol();
        if (this.left.value.useLiterals) {
            if (this.left.value.isNumber()) addOperName = "add_left_number";
            else addOperName = "add_left_string";
        } else if (this.right.value.useLiterals) {
            if (this.right.value.isNumber()) addOperName = "add_right_number";
            else addOperName = "add_right_string";
        } else {
            addOperName = "do_add";
        }
        this.value = CreateValue(Value.T.CSValue, _cSymbolValue);
        output[addOperName](this.left.value.get(), this.right.value.get(), _cSymbolValue);
    }
});

ast.AST_BinaryExpr.proto("-", function() {});

ast.AST_BinaryExpr.proto("*", function() {});

ast.AST_BinaryExpr.proto("/", function() {});

ast.AST_BinaryExpr.proto("%", function() {});

