function declare(typename, protos, base) {
    var code = ["return function AST_", typename, "(){",
            "var args = Array.prototype.slice.call(arguments, 0), $base, current, _this;",
            "if (this.type) {",
            "current = this.base;",
            "if (this.base) $base = this.base = this.base.prototype.base;",
            "} else {",
            "this.type = '", typename, "';",
            "$base = this.base;",
            "_this = this;",
            "}",
            "if ($base) $base.apply(this, args);",
            "if (current && current.prototype && typeof current.prototype.initialize == 'function') {",
            "current.prototype.initialize.apply(this, args);",
            "} else if (_this && _this.initialize){",
            "_this.initialize.apply(this, args);",
            "if (!this.base) delete this.base;",
            "}",
            "};"
    ].join("");
    var Node = new Function(code)();

    function Node() {
        var args = Array.prototype.slice.call(arguments, 0),
            $base, current, _this;
        //!!如果这个已经存在，说明是构造父类
        if (this.type) {
            current = this.base;
            if (this.base) $base = this.base = this.base.prototype.base;
        } else {
            this.type = typename;
            $base = this.base;
            _this = this;
        }
        //构造父类
        if ($base) $base.apply(this, args);
        //initalize
        if (current && current.prototype && typeof current.prototype.initialize == "function") { //父类的initialize
            current.prototype.initialize.apply(this, args);
        } else if (_this && _this.initialize) { //最终要调一下自己的initialize
            _this.initialize.apply(this, args);
            if (!this.base) delete this.base; //当对象已经建立好，base属性对自己来说已经没有用了
        }
    } //Node

    if (typeof base == "function") {
        var baseProto = base.prototype || {};
        function basefun() {
            this.constructor = Node; //保证instanceof的正确性
            this.__parent__ = baseProto;
        }
        basefun.prototype = baseProto;
        Node.prototype = new basefun();
        Node.prototype.base = base; //base一定要放在这里才方便访问(prototype上)，并且new一个实例后可以把base属性删掉
    }
    Node.proto = function(name, fun) {
        this.prototype[name] = fun;
    };
    Node.unproto = function(name){
        delete this.prototype[name];
    };
    if (typeof protos == "function") {
        protos = {
            initialize: protos
        };
    }
    for (var k in protos) {
        if (protos.hasOwnProperty(k)) {
            Node.proto(k, protos[k]);
        }
    }
    return Node;
}

/*
function SourceLocation(source, s, e){
    this.source = source;
    this.start = s;
    this.end = e;
}

function Position(line, column) {
    this.line = line || 1;
    this.column = column || 0;
}

*/

var AST_Node = exports.AST_Node = declare('Node', {
    walk: function(visitor) {
        return visitor.visit(this);
    }
});

var AST_Statement = declare("Statement", null, AST_Node);

var AST_Expression = exports.AST_Expression = declare("Expression", null, AST_Node);

var AST_Unary = exports.AST_Unary = declare("Unary", function(op, expr) {
    this.operator = op;
    this.expression = expr;
}, AST_Expression);

var AST_UnaryNot = exports.AST_UnaryNot = declare("UnaryNot", null, AST_Unary);
var AST_UnaryExist = exports.AST_UnaryExist = declare("UnaryExist", null, AST_Unary);
var AST_UnaryForceNum = exports.AST_UnaryForceNum = declare("UnaryForceNum", null, AST_Unary);
var AST_UnaryNegative = exports.AST_UnaryNegative = declare("UnaryNegative", null, AST_Unary);
var AST_UnaryPositive = exports.AST_UnaryPositive = declare("UnaryPositive", null, AST_Unary);

var AST_BinaryExpr = exports.AST_BinaryExpr = declare("BinaryExpr", {
    //construct
    initialize: function(op, left, right) {
        this.operator = op;
        this.left = left;
        this.right = right;
    },
    walk: function(visitor) {
        var self = this;
        visitor.visit(this, function(){
            self.left.walk(visitor);
            self.right.walk(visitor);
        });
    }
}, AST_Expression);

var AST_BinLogicExpr = exports.AST_BinLogicExpr = declare("BinLogicExpr", null, AST_BinaryExpr);
var AST_BinMathExpr = exports.AST_BinMathExpr = declare("BinMathExpr", null, AST_BinaryExpr);

var AST_CommaExpr = exports.AST_CommaExpr = declare("CommaExpr", function(list){
    this.expressions = list;
}, AST_Expression);

var AST_Symbol = exports.AST_Symbol = declare("Symbol", function(name) {
    this.name = name;
}, AST_Expression);

var AST_VariableAccess = exports.AST_VariableAccess = declare("VariabelAccess", {
    initialize: function(obj) {
        this.target = obj;
    },
    walk: function(visitor) {
        return visitor.visit(this, function() {
            this.target.walk(visitor);
        });
    }
}, AST_Expression);

var AST_Prop = exports.AST_Prop = declare("Prop", {
    initialize: function(right) {
        this.right = right;
    },
    walk: function(visitor) {
        return visitor.visit(this, function() {
            this.left.walk(visitor);
            this.right.walk(visitor);
        });
    }
}, AST_Expression);

var AST_SubProp = exports.AST_SubProp = declare("SubProp", null, AST_Prop);
var AST_DotProp = exports.AST_DotProp = declare("DotProp", null, AST_Prop);

var AST_Constant = declare("Constant", function($) {
    this.literalValue = $;
}, AST_Expression);

var AST_Content = exports.AST_Content = declare("Content", null, AST_Constant);

var AST_String = exports.AST_String = declare("String", null, AST_Constant);
var AST_Number = exports.AST_Number = declare("Number", null, AST_Constant);

var AST_Invoke = declare('Invoke', {
    initialize: function InvokeInitialize(name, args /*argsTree*/ ) {
        this.id = name;
        this.args = args;
        //在语法分析阶段可以使用ebnf直接生成args数组，不用在这里进行转换
        //this.args = this.convertToArray(argsTree);
    },
    /**
     * @deprecated
     */
    convertToArray: function(paramTree) {
        if (!paramTree) return []; //空参数调用
        var result = [];
        do {
            result.unshift(paramTree.expression);
            paramTree = paramTree.prev;
        } while (paramTree);
        return result;
    }
}, AST_Node);

//@deprecated
var AST_Parameter = exports.AST_Parameter = declare("Parameter", function(prevParam, thisParam) {
    if (thisParam == undefined) { //只有一个参数的时候这里会是空
        this.expression = prevParam;
    } else {
        this.expression = thisParam;
        this.prev = prevParam;
    }
}, AST_Node);

var AST_FunctionCall = exports.AST_FunctionCall = declare("FunctionCall", function FunctionCallInitialize(name, args) {
    this.id = name;
    this.args = args;
}, AST_Expression);

var AST_MacroCall = exports.AST_MacroCall = declare("MacroCall", null, AST_Invoke);

var AST_VarStmt = exports.AST_VarStmt = declare("Var", {
    initialize: function($) {
        this.argument = $;
    },
    walk: function(visitor) {
        visitor.visit(this, function() {
            this.argument.walk(visitor);
        });
    }
}, AST_Statement);

var AST_NameStmt = exports.AST_NameStmt = declare("Name", {
    initialize: function($) {
        this.argument = $;
    },
    walk: function(visitor) {
        visitor.visit(this, function() {
            this.argument.walk(visitor);
        });
    }
}, AST_Statement);

var AST_SetStmt = exports.AST_SetStmt = declare("Set", {
    initialize: function(left, right) {
        this.left = left;
        this.right = right;
    },
    walk: function(visitor) {
        visitor.visit(this, function() {
            this.left.walk(visitor);
            this.right.walk(visitor);
        });
    }
}, AST_Statement);

var AST_Block = exports.AST_Block = declare("Block", {
    initialize: function(body) {
        this.body = body;
    },
    walk: function(visitor) {
        visitor.visit(this, function() {
            this.body.forEach(function(item) {
                item.walk(visitor);
            });
        });
    }
}, AST_Statement);

var AST_If = exports.AST_If = declare("If", function(consequent, testExpr, alternate) {
    this.test = testExpr;
    //this.consequent = consequent;//replace by body
    this.alternate = alternate;
}, AST_Block);

var AST_Alt = exports.AST_Alt = declare("Alt", function(expr, alternate) {
    this.expression = expr;
    this.alternate = alternate;
}, AST_Block)

var AST_Escape = exports.AST_Escape = declare("Escape", function(body, type){
    this.escapeType = type;
}, AST_Block);

var AST_Scope = exports.AST_Scope = declare("Scope", null , AST_Block);

var AST_MacroDef = exports.AST_MacroDef = declare("MacroDef", function(body, name, defParams) {
    this.id = name;
    this.parameters = defParams;
}, AST_Scope);

var AST_Each = exports.AST_Each = declare("Each", function(body, variable, expr) {
    this.variable = variable;
    this.expression = expr;
}, AST_Scope);

var AST_With = exports.AST_With = declare("With", function(body, alias, expr) {
    this.alias = alias;//Symbol
    this.expression = expr;
}, AST_Scope);

var AST_Loop = exports.AST_Loop = declare("Loop", function(body, initVar, endExpr, step){
    this.step = step;
    this.endexpr = endExpr;
    this.initvar = initVar;
}, AST_Scope);


var AST_Program = exports.AST_Program = declare("Program", null, AST_Scope);
