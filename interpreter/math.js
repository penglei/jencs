var ast = require("../parse/ast"),
    CSValue = require("./scope").CSValue;


ast.AST_BinMathExpr.proto("calc", function BinaryExprCalc(){
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    if (this.operator == "+"){
        return this[this.operator](leftValue, rightValue);
    } else {
        if (isNaN(parseInt(leftValue.value)) || isNaN(parseInt(rightValue.value))) {
            return new CSValue(CSValue.Void);
        }
        return this[this.operator](leftValue, rightValue);
    }
});

ast.AST_BinaryExpr.proto("+", function BinaryExprAdd(leftValue, rightValue) {
    if (leftValue.type == CSValue.Number || rightValue.type == CSValue.Number){
        return new CSValue(CSValue.Number, leftValue.getNumber() + rightValue.getNumber());
    } else {
        return new CSValue(CSValue.String, leftValue.getString() + rightValue.getString());
    }
});

ast.AST_BinaryExpr.proto("-", function BinaryExprPlus(leftValue, rightValue) {
    return new CSValue(CSValue.Number, leftValue.getNumber() - rightValue.getNumber());
});

ast.AST_BinaryExpr.proto("*", function BinaryExprMul(leftValue, rightValue) {
    return new CSValue(CSValue.Number, leftValue.getNumber() * rightValue.getNumber());
});

ast.AST_BinaryExpr.proto("/", function BinaryExprDiv(leftValue, rightValue) {
    return new CSValue(CSValue.Number, leftValue.getNumber() / rightValue.getNumber());
});

ast.AST_BinaryExpr.proto("%", function BinaryExprMod(leftValue, rightValue) {
    return new CSValue(CSValue.Number, leftValue.getNumber() / rightValue.getNumber());
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
