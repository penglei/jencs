var ast = require("../parse/ast"),
    CSValue = require("./scope").CSValue;

ast.AST_BinLogicExpr.proto("calc", function(){
    return this[this.operator]();
});

ast.AST_BinaryExpr.proto("||", function BinaryLogicOr(leftValue, rightValue) {
    var leftValue = this.left.calc();
    if (leftValue.isTrue()){
        return new CSValue(CSValue.Number, 1);
    } else {
        return this.right.calc();
    }
});

ast.AST_BinaryExpr.proto("&&", function BinaryLogicAnd() {
    var leftValue = this.left.calc();
    if (!leftValue.isTrue()){
        return new CSValue(CSValue.Number, 0);
    } else {
        return this.right.calc();
    }
});

ast.AST_BinaryExpr.proto("==", function BinaryLogicEqual() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    if (leftValue.type == CSValue.Void && rightValue.type == CSValue.Void){
        return new CSValue(CSValue.Number, 1);
    }
    if (leftValue.type == CSValue.Void){
        return new CSValue(CSValue.Number, rightValue.isTrue() ? 0 : 1);
    }

    if (rightValue.type == CSValue.Void){
        return new CSValue(CSValue.Number, leftValue.isTrue() ? 0 : 1);
    }

    return new CSValue(CSValue.Number, leftValue.value == rightValue.value ? 1 : 0);
});

ast.AST_BinaryExpr.proto("!=", function BinaryLogicNotEqual() {
    var resultVal = this["=="]();
    resultVal.value = resultVal.isTrue() ? 0 : 1;
    return resultVal;
});

ast.AST_BinaryExpr.proto(">", function BinaryLogicGreater() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.value > rightValue.value ? 1 : 0);
});

ast.AST_BinaryExpr.proto(">=", function BinaryLogicGreater_Equal() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.value >= rightValue.value ? 1 : 0);
});

ast.AST_BinaryExpr.proto("<", function BinaryLogicSmaller() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.value < rightValue.value ? 1 : 0);
});

ast.AST_BinaryExpr.proto("<=", function BinaryLogicSmaller_Equal() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.value <= rightValue.value ? 1 : 0);
});


ast.AST_UnaryExist.proto("calc", function UnaryExist(){
    var resultVal = this.expression.getSymbolValueNode();
    return new CSValue(CSValue.Number, resultVal ? 1 : 0);
});

ast.AST_UnaryNot.proto("calc", function UnaryNot(){
    var resultVal = this.expression.calc();
    return new CSValue(CSValue.Number, resultVal.isTrue() ? 0 : 1);//注意这里是 非运算符 
});

