var ast = require("../parse/ast"),
    Util = require("./util"),
    CSValue = require("./scope").CSValue,
    HNode = require("./hdf").HNode,
    def_execute = require("./executer").def_execute;


def_execute(ast.AST_VarStmt, function(context){
    var resultVal = this.argument.calc();
    context.output(resultVal.getString(), this);
});

def_execute(ast.AST_SetStmt, function(){
    var targetNode = this.left.getNodeObject();
    if (targetNode){
        var rightValue = this.right.calc();
        targetNode.setValue(rightValue.getString());
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
    if (macro.id == "data_comments_inputbox") debugger
    var _symbolAlias = {};//通过这个东西传递参数
    //处理宏调用的实参
    for (var i = 0; i < this.args.length; i++) {
        var param = macroParams[i];//形参
        var arg = this.args[i], argValue;
        if (arg instanceof ast.AST_VariableAccess){
            argValue = arg.getSymbolValueNode();
            if (!argValue) argValue = new CSValue(CSValue.Void);//用一个临时的CSValue作为实参(注意这个实参是CSValue,而不是HNode)
        } else {
            argValue = arg.calc();
        }
        _symbolAlias[param.name] = argValue;
    }

    macro.execute(context, _symbolAlias);
});

def_execute(ast.AST_Include, function(context){
    if (this.subast){
        //subAst instanceof ast.AST_Program
        //这里是不能直接执行subAst的，因为那样会进入一个新的scope
        //其实Include相当于是把代码嵌入到这里，所以只需要关心它的body即可
        this.subast.gen_body(context);
    } else {
        //throw new Error("include execute faild for file:" + this.name);
    }
});
