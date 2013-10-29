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
//一元操作符
ast.AST_Unary.proto("-", function() {});

ast.AST_Unary.proto("+", function() {});

ast.AST_Unary.proto("#", function() {});

ast.AST_Unary.proto("$", function() {});

ast.AST_Unary.proto("!", function() {});

ast.AST_Unary.proto("?", function() {});
