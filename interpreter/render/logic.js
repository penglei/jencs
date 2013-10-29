var ast = require("../../parse/ast"),
    CSValue = require("./scope").CSValue;


ast.AST_BinaryExpr.proto("||", function BinaryLogicOr() {});

ast.AST_BinaryExpr.proto("&&", function BinaryLogicAnd() {});

ast.AST_BinaryExpr.proto("==", function BinaryLogicEqual() {});

ast.AST_BinaryExpr.proto("!=", function BinaryLogicNotEqual() {});

ast.AST_BinaryExpr.proto(">", function BinaryLogicGreater() {});
ast.AST_BinaryExpr.proto(">=", function BinaryLogicGreater_Equal() {});
ast.AST_BinaryExpr.proto("<", function BinaryLogicSmaller() {});
ast.AST_BinaryExpr.proto("<=", function BinaryLogicSmaller_Equal() {});


ast.AST_UnaryExist.proto("calc", function UnaryExist(){
});

ast.AST_UnaryNot.proto("calc", function UnaryNot(){
});

