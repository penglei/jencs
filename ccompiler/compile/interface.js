var ast = require("../parse/ast");

exports.Void = {};//即可以表示Vaule，也可以表示HDFNode，都只有一个意义：空值，不存在

//从值的确定性来说，目标代码(c)"量值"只有CSValue，Literals两种类型，而Literals又分为String, Integer, Float
//Value类是编译器内部对表达式的值类型抽象，它用于生成高效的代码
//用他来追踪一个"量"的确定性
//String表示这是一个可确定的String量
//Number表示这是一个可确定的Integer量(后续再考虑支持Float)
//CSValue是目标语言中对变量的抽象，它可以表示多种变量类型(char*, long long int, double)
function Value(type, val) {
    this.type = type;
    this._val = val;
}

Value.T = Value.prototype.Type = {
    "String": 1,
    "Number": 2,
    /*
    "Float": 3,
    "Integer": 4,
    */
    "CSValue": 5,
    "NULL": 7
};
Value.prototype.set = function(val) {
    this._val = val;
};
Value.prototype.get = function() {
    return this._val;
};
Value.prototype.isNumber = function() {
    return this.type == this.Type.Number;
};

function CreateValue(type, val) {
    var v = new Value(type, val);
    if (type == Value.T.String || type == Value.T.Number) {
        v.useLiterals = true;
    } else if (type == Value.T.CSValue) {
        //(说到底，所有的值不都是符号么？我们只是对不同的符号类型用不同的处理方式)
    }
    return v;
}

exports.Value = Value;
exports.CreateValue = CreateValue;

function def_genCode(nodeType, gencodeFun) {
    nodeType.proto("gen_code", gencodeFun);
}

function isSymbol(astNode) {
    return astNode instanceof ast.AST_Symbol;
}

function isLogic(astNode){
    return /&&|\|\|/.test(astNode.operator);
}
exports.def_genCode = def_genCode;
exports.isSymbol = isSymbol;
exports.isLogic = isLogic;
