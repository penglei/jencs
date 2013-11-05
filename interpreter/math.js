var ast = require("../parse/ast"),
    CSValue = require("./scope").CSValue;


ast.AST_BinMathExpr.proto("calc", function BinaryExprCalc(){
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return this[this.operator](leftValue, rightValue);
});

ast.AST_BinaryExpr.proto("+", function BinaryExprAdd(leftValue, rightValue) {
    /**
     * def:foo(num) var:num + "11"
     * call:foo(22)  这只会得到2211，而不是33，因为那个加法操作的左右表达式都无法直接判断出是数字相加
     * 但是，对于 (11 + "22") + (11 + "22")，却可以得到66，因为原引擎会把中间表达式的左右expr都计算为数字
     */

    if (leftValue.type == CSValue.Number || rightValue.type == CSValue.Number){
        //原cs引擎只对左右表达式有一个确定是数字常量才会运用数字相加操作，否则，都是字符串连接
        return new CSValue(CSValue.Number, leftValue.getNumber() + rightValue.getNumber());
    }

    return new CSValue(CSValue.String, leftValue.getString() + rightValue.getString());
});

ast.AST_BinaryExpr.proto("-", function BinaryExprPlus(leftValue, rightValue) {
    return new CSValue(CSValue.Number, leftValue.getNumber() - rightValue.getNumber());
});

ast.AST_BinaryExpr.proto("*", function BinaryExprMul(leftValue, rightValue) {
    return new CSValue(CSValue.Number, leftValue.getNumber() * rightValue.getNumber());
});

ast.AST_BinaryExpr.proto("/", function BinaryExprDiv(leftValue, rightValue) {
    var rightNumber = rightValue.getNumber();
    if (rightNumber == 0){
        //TODO 直接报错比较好
        return new CSValue(CSValue.Number, 0);
    } else {
        return new CSValue(CSValue.Number, parseInt(leftValue.getNumber() / rightValue.getNumber()));
    }
});

ast.AST_BinaryExpr.proto("%", function BinaryExprMod(leftValue, rightValue) {
    var rightNumber = rightValue.getNumber();
    if (rightNumber == 0){
        //TODO 直接报错比较好
        return new CSValue(CSValue.Number, 0);
    } else {
        return new CSValue(CSValue.Number, leftValue.getNumber() % rightValue.getNumber());
    }
});


ast.AST_UnaryForceNum.proto("calc", function UnaryForceNumber(){
    var resultVal = this.expression.calc();
    resultVal.type = CSValue.Number;
    var numVal = parseInt(resultVal.value);
    resultVal.value = isNaN(numVal) ? 0 : numVal;
    return resultVal;
});

ast.AST_UnaryPositive.proto("calc", function UnaryPositive(){
    var resultVal = this.expression.calc();
    resultVal.type = CSValue.Number;
    var numVal = parseInt(resultVal.value);
    resultVal.value = isNaN(numVal) ? 0 : numVal;
    return resultVal;
});


ast.AST_UnaryNegative.proto("calc", function UnaryNegative(){
    var resultVal = this.expression.calc();
    resultVal.type = CSValue.Number;
    var numVal = parseInt(resultVal.value);
    resultVal.value = isNaN(numVal) ? 0 : (0 - numVal);
    return resultVal;
});

