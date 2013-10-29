var path = require("path"),
    fs = require("fs"),
    ast = require("../../parse/ast"),
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

//加载所有的编译组件
var FILES_COMP = ["expr_logic", "expr_math", "expr_compare", "unary", "hdfaccess", "stmt_if"];
Util.each(FILES_COMP, function(file, i){
    //require(path.resolve(path.dirname(module.filename), file));
});


//默认的生成函数是空
def_genCode(ast.AST_Node, function(){});

/**
 * 表达式分析，所有表达式分析生成结束后，除VariableAccess外，其value和cSymbol都是CSValue变量的名字
 * VariableAccess 的取值在什么地方完成呢？这要看它用的地方。所VariableAccess返回的都是节点名
 * 对于局部变量，它确返回值(CSValue)，这样看它的cSymbol是什么类型。
 */
ast.AST_Expression.proto("expr_gen_val", function(opts) {
    opts = opts || {};
    var output = this.output;

    if (this instanceof ast.AST_Number) {
        this.value = CreateValue(Value.T.Number, parseInt(this.literalValue));
    } else if (this instanceof ast.AST_String) {
        this.value = CreateValue(Value.T.String, this.literalValue);
    } else if (this instanceof ast.AST_VariableAccess) {
        this.gen_code(opts); //表达式才应该求值
        if ( this.cSymbol instanceof Scope.HDFNodeSymbol){
            var valSymbol = this.context.getHdfValueSymbol(this.cSymbol);
            if (!valSymbol.inited) {
                output.hdf_value(this.cSymbol, valSymbol);
            }
            this.value = CreateValue(Value.T.CSValue, valSymbol);
        } else if (this.cSymbol instanceof Value) { //引用的就是一个值（实参是一个值的时候）
            this.value = this.cSymbol;
        } else {
            this.value = CreateValue(Value.T.NULL, Void);//值也要为Void（这样才可以传递）
        }
    } else {
        this.gen_code(opts);
    }
});

def_genCode(ast.AST_BinaryExpr, function(opts) {
    //只有算术表达式先对左右节点求值，因为逻辑表达式有特殊的优化
    //考虑逻辑表达式不应该归为二元表达式么？
    if (!isLogic(this)) {
        this.left.expr_gen_val();
        this.right.expr_gen_val();
    }
    this[this.operator](opts);
});

def_genCode(ast.AST_Unary, function(opts) {
    this[this.operator](opts);
});

def_genCode(ast.AST_VarStmt, function() {
    var output = this.output;
    var expr = this.argument;
    expr.expr_gen_val(); //分析表达式
    var val = expr.value.get();
    if (val != Void) {
        if (expr.value.type == Value.T.CSValue) {
            output.out_value(val);
        } else {
            output.out_string(val);
        }
    }
});

def_genCode(ast.AST_SetStmt, function() {
    var output = this.output,
        context = this.context;

    //肯定是变量访问
    this.left.gen_code({useInLeftOfSET:true}); //先得到左节点,如果不存在，就生成

    var targetNodeSymbol;

    var leftCsymbol = this.left.cSymbol;
    if (leftCsymbol instanceof Scope.HDFNodeSymbol) {//hdf引用，大部份因该走这个逻辑
        if (leftCsymbol.valSymbol) leftCsymbol.valSymbol.inited = false;
        targetNodeSymbol = leftCsymbol;
    } else if (leftCsymbol instanceof Value) {
        if (leftCsymbol.type == Value.T.CSValue){
            targetNodeSymbol = leftCsymbol;
        } else {
            //这是具体的值，需要建立临时变量
            targetNodeSymbol = context.newTempValueSymbol();
            context.updateScopeSymbol(CreateValue(Value.T.CSValue, targetNodeSymbol), this.left.target.name);
        }
    } else {
        //Void hdf
        if (this.left.target instanceof ast.AST_Symbol){
            targetNodeSymbol = context.newTempValueSymbol();
            context.updateScopeSymbol(CreateValue(Value.T.CSValue, targetNodeSymbol), this.left.target.name);
        } else {
            return false;//原cs引擎对于在空节点上的set都扔掉
        }
    }

    this.right.expr_gen_val(); //算出右值
    //XXX 这里其实也可以对VariableAccess优化一下，减少一个取值的函数调用
    var val = this.right.value;
    if (val.get() == Void) {
        output.set_hdf_value(targetNodeSymbol, "", "Literals");
    } else if (val.useLiterals) {
        output.set_hdf_value(targetNodeSymbol, val.get(), "Literals");
    } else {
        output.set_hdf_value(targetNodeSymbol, val.get(), "CSValue");
    }
});


ast.AST_Block.proto("gen_body", function(flush) {
    var body = this.body, i = 0;
    var output = this.output;

    //{先合并直接输出的内容
    if (body.length > 1) do {
            if (body[i] instanceof ast.AST_Content) {
                for (var j = i + 1; j < body.length;) {
                    var nodeItem = body[j];
                    if (nodeItem instanceof ast.AST_Content) {
                        body[i].literalValue += body.splice(j, 1)[0].literalValue;
                    } else if (nodeItem instanceof ast.AST_SetStmt || nodeItem instanceof ast.AST_MacroDef) {
                        //没有任何输出的语法，可以继续合并
                        j++;
                        continue;
                    } else break;
                }
            }
            i++;
    }
    while (i < body.length);
    //}

    if (!flush) output.ob_start();
    body.forEach(function(stmt) {
        if (stmt instanceof ast.AST_Content) {
            output.out_string(stmt.literalValue);
        } else if (!(stmt instanceof ast.AST_MacroDef)) {
            stmt.gen_code();
        }
    });
    if (!flush){
        var codesBody = output.ob_get(); //会把buffer清掉
        output.ob_end();
        return codesBody;
    }
});

def_genCode(ast.AST_Program, function() {
    var context = this.context;
    context.scope = this;
    var block = context.enterBlock();
    var output = this.output;

    output.line();
    var codes = this.gen_body();
    context.leaveBlock();
    //把获得的定义输出
    output.createDefinition(block);
    output.pushCodes(codes);

    output.undent();
});

def_genCode(ast.AST_With, function() {
    var output = this.output;
    //expression只能是VariableAccess，这在语法分析阶段已经完成了
    this.expression.gen_code();
    this.symbolAlias[this.alias.name] = this.expression.cSymbol; //hdf节点的cSymbol

    this.context.scope = this; //expression的作用域是父scope，所以对expression分析结束后才改变scope

    //如果表达式对就的节点不存在，则不能进入body的生成
    output.jdugeWith(this.expression.cSymbol).with_braces(function() {
        output.newline().indent();
        this.gen_body();
        output.undent();
    }, this).newline();

    this.context.scope = this.parent_scope;
});

def_genCode(ast.AST_MacroDef, function() {
    //宏body的生成没有任何特殊之处，它的参数作用域全部有macroDef处理
    this.context.scope = this;
    this.gen_body(true);
    this.context.scope = this.parent_scope;
});

def_genCode(ast.AST_MacroCall, function() {
    //找到相应的macro
    var macro = this.refMacro;
    var macroParams = macro.parameters;

    //处理宏调用的实参
    for (var i = 0; i < this.args.length; i++) {
        var expr = this.args[i],
            param = macroParams[i];
        if (expr instanceof ast.AST_VariableAccess) {
            //macroCall是传hdf节点进去，而不是值
            expr.gen_code();
            macro.symbolAlias[param.name] = expr.cSymbol;
        } else {
            expr.expr_gen_val();//如果是常量，在编译器内部也是一个Value实例来表示
            macro.symbolAlias[param.name] = expr.value;
        }
    }

    //!!! 当前scope会作为macro_def的scope
    var currentScope = this.context.scope;
    macro.parent_scope = currentScope; //这个时候才能确定macro的scope

    //这里其实应该用clone的宏来生成代码，防止污染
    //macro.clone().gen_code();
    macro.gen_code();
});


exports.compile = function(astInstance, output, context) {
    //start generate code
    ast.AST_Node.proto("context", context);
    ast.AST_Node.proto("output", output);
    astInstance.gen_code();
};

