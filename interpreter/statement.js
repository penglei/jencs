var ast = require("../parse/ast"),
    Types = require("./types"),
    def_execute = require("./executer").def_execute;

var HNode = Types.HNode;
var CSValue = Types.CSValue;

def_execute(ast.AST_VarStmt, function(context){
    var resultVal = this.argument.calc();
    context.output(resultVal.getString(), this);
});

def_execute(ast.AST_Content, function(context){
    context.output(this.literalValue, this);
});

def_execute(ast.AST_SetStmt, function(){
    var targetNode = this.left.getOrCreateNodeObject();
    if (targetNode){
        var rightValue = this.right.calc();
        targetNode.setValue(rightValue.value);
    } else {
        //TODO notice
    }
});

def_execute(ast.AST_NameStmt, function(context){
    var result = this.argument.getSymbolValueNode();
    if (result) {
        if (result instanceof HNode){
            context.output(result.name, this);
        } else {
            //TODO notice
        }
    } else {
        //TODO notice
    }
});

def_execute(ast.AST_MacroCall, function(context) {
    //找到相应的macro
    var macro = this.refMacro;
    var macroParams = macro.parameters;

    var _symbolAlias = {};//通过这个东西传递参数
    //处理宏调用的实参
    for (var i = 0; i < this.args.length; i++) {
        var param = macroParams[i];//形参
        var arg = this.args[i], argValue;
        if (arg instanceof ast.AST_VariableAccess){
            argValue = arg.getSymbolValueNode();
            if (!argValue) argValue = arg; //没有就把ast节点传过去，都是为了那个macro set功能
        } else {
            argValue = arg.calc();
        }
        _symbolAlias[param.name] = argValue;
    }

    macro.execJump(context, _symbolAlias);
});

def_execute(ast.AST_Include, function(context){
    if (this.subast){
        //subAst instanceof ast.AST_Program
        //这里是不能直接执行subAst的，因为那样会进入一个新的scope，并且发出一个结束通知
        //Include相当于是把代码嵌入到这里，所以只需要关心它的body即可
        this.subast.gen_body(context);
    } else {
        //throw new Error("include execute faild for file:" + this.name);
    }
});

def_execute(ast.AST_CSDebugger, function(context){
    debugger
});
