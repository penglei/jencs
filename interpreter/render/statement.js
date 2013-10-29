var ast = require("../../parse/ast"),
    Util = require("./util"),
    CSValue = require("./scope").CSValue,
    HNode = require("hdf2json").HNode,
    def_execute = require("./executer").def_execute;


def_execute(ast.AST_VarStmt, function(print){
    var resultVal = this.argument.calc();
    print(resultVal.getString());
});

def_execute(ast.AST_SetStmt, function(){
    debugger
    var targetNode = this.left.getNodeObject();
    if (targetNode){
        var rightValue = this.right.calc();
        targetNode.setValue(rightValue.getString());
    } else {
        //TODO notice
    }
});

def_execute(ast.AST_NameStmt, function(print){
    var result = this.argument.getSymbolValueNode();
    if (result) {
        if (result instanceof HNode){
            print(result.name);
        } else {
            //TODO notice
        }
    } else {
        //TODO notice
    }
});

def_execute(ast.AST_MacroCall, function(print) {
    //找到相应的macro
    var macro = this.refMacro;
    var macroParams = macro.parameters;

    //处理宏调用的实参
    for (var i = 0; i < this.args.length; i++) {
        var param = macroParams[i];//形参
        var arg = this.args[i], argValue;
        if (arg instanceof ast.AST_VariableAccess){
            argValue = arg.getValueNode();
            if (!argValue){
                resultVal = new CSValue(CSValue.Void);//用一个临时的值作为实参，注意这个实参是CSValue,而不是HNode
            }
        } else {
            argValue = arg.calc();
        }
        macro.symbolAlias[param.name] = resultVal;
    }

    macro.execute(print);
});
