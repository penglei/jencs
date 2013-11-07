var ast = require("../parse/ast"),
    CSValue = require("./types").CSValue;

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

//对于empty(空串)和null是区分对待的，导致它的逻辑变得异常复杂
ast.AST_BinaryExpr.proto("==", function BinaryLogicEqual() {
    var leftValue, rightValue;

    if (this.left instanceof ast.AST_VariableAccess && this.right instanceof ast.AST_VariableAccess){
        var leftResult = this.left.getSymbolValueNode();
        var rightResult = this.right.getSymbolValueNode();

        //all is not exist, so they're equal
        if (!leftResult && !rightResult) return new CSValue(CSValue.Number, 1);

        //一个是null，那另一个只要不为null就不等
        if ((!leftResult && rightResult) || (leftResult && !rightResult)) return new CSValue(CSValue.Number, 0);

        //然后走普通逻辑

        leftValue = this.left.calc();
        rightValue = this.right.calc();

    } else if (this.left instanceof ast.AST_VariableAccess){
        var leftResult = this.left.getSymbolValueNode();

        rightValue = this.right.calc();

        if (!leftResult) {
            if (rightValue.type == CSValue.Number) {
                return new CSValue(CSValue.Number, rightValue.value == 0 ? 1 : 0);
            } else {//一个有，一个没有，当然返回false
                return new CSValue(CSValue.Number, 0);
            }
        } else {
            leftValue = this.left.calc();
            //走普通逻辑
        }

    } else if (this.right instanceof ast.AST_VariableAccess){
        var rightResult = this.right.getSymbolValueNode();

        leftValue = this.left.calc();

        if (!rightResult) {
            if (leftValue.type == CSValue.Number) {
                return new CSValue(CSValue.Number, leftValue.value == 0 ? 1 : 0);
            } else {
                return new CSValue(CSValue.Number, 0);
            }
        } else {
            rightValue = this.right.calc();
            //走普通逻辑
        }
    } else {
        leftValue = this.left.calc();
        rightValue = this.right.calc();
    }

    //然后再走是否是数字的逻辑判断，比如 123 == "123a"，那个坑爹的cs引擎
    if (leftValue.type == CSValue.Number || rightValue.type == CSValue.Number) {
        return new CSValue(CSValue.Number, leftValue.getNumber() == rightValue.getNumber() ? 1 : 0);
    }
    return new CSValue(CSValue.Number, leftValue.value == rightValue.value ? 1 : 0);

});

ast.AST_BinaryExpr.proto("!=", function BinaryLogicNotEqual() {
    var resultVal = this["=="]();
    resultVal.value = resultVal.isTrue() ? 0 : 1;
    return resultVal;
});

//{cs引擎L2398 CS_OP_GT |  CS_OP_GTE可以看出，比较操作全部是当成数字来计算的
ast.AST_BinaryExpr.proto(">", function BinaryLogicGreater() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.getNumber() > rightValue.getNumber() ? 1 : 0);
});

ast.AST_BinaryExpr.proto(">=", function BinaryLogicGreater_Equal() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.getNumber() >= rightValue.getNumber() ? 1 : 0);
});

ast.AST_BinaryExpr.proto("<", function BinaryLogicSmaller() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.getNumber() < rightValue.getNumber() ? 1 : 0);
});

ast.AST_BinaryExpr.proto("<=", function BinaryLogicSmaller_Equal() {
    var leftValue = this.left.calc();
    var rightValue = this.right.calc();
    return new CSValue(CSValue.Number, leftValue.getNumber() <= rightValue.getNumber() ? 1 : 0);
});
//}


ast.AST_UnaryExist.proto("calc", function UnaryExist(){
    //常量可以有这种操作符..
    if (this.expression instanceof ast.AST_Constant) {
        return new CSValue(CSValue.Number, 1);
    }
    if (this.expression instanceof ast.AST_CommaExpr) {//因为CommaExpr也是表达式，所以要特殊处理
        //TODO 这个要看最后一个表达式是什么东西
    }
    if (this.expression instanceof ast.AST_Expression) {
        return new CSValue(CSValue.Number, 1);
    }
    if (this.expression instanceof ast.AST_VariableAccess){
        var resultVal = this.expression.getSymbolValueNode();
        return new CSValue(CSValue.Number, resultVal ? 1 : 0);
    }
});

ast.AST_UnaryNot.proto("calc", function UnaryNot(){
    var resultVal = this.expression.calc();
    return new CSValue(CSValue.Number, resultVal.isTrue() ? 0 : 1);//注意这里是 非运算符 
});

