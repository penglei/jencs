var ast = require("../../parse/ast"),
    Util = require("./util"),
    CSValue = require("./scope").CSValue,
    HNode = require("hdf2json").HNode,
    def_execute = require("./executer").def_execute;

function isSymbol(astNode) {
    return astNode instanceof ast.AST_Symbol;
}

require("./math");

ast.AST_Number.proto("calc", function(){
    return new CSValue(CSValue.Number, parseInt(this.literalValue));
});

ast.AST_String.proto("calc", function(argument) {
    return new CSValue(CSValue.String, this.literalValue);
});

ast.AST_UnaryExist

ast.AST_VariableAccess.proto("calc", function(opts){//opts可以用来确定究竟是获得什么类型的值，如!号表达式
    var resultVal = this.getSymbolValueNode();
    if (resultVal){
        if (resultVal instanceof CSValue){
            return resultVal;
        } else if (resultVal instanceof HNode){
            return new CSValue(CSValue.String, resultVal.getValue());
        } else {
            throw new Error("symbol value is unrecognized");
        }
    } else {
        return new CSValue(CSValue.Void);
    }
});

ast.AST_VariableAccess.proto("getSymbolValueNode", function(){ //opts可以用来确定究竟是获得什么类型的值，如!号表达式
    if (isSymbol(this.target)){
        return this.context.querySymbol(this.target.name);
    } else if (this.target instanceof ast.AST_Prop){
        var hdfNode = this.target._queryHdfNode();
        return hdfNode;
    } else {
        throw new Error("runtime error: unkonw hdf variable access");
    }
});


ast.AST_DotProp.proto("_queryHdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)){
        leftValue = this.context.querySymbol(this.left.name);
    } else {
        leftValue = this.left._queryHdfNode();
    }

    var attrName = this.right.name || this.right.literalValue; //right为AST_Symbol或AST_Number
    if (leftValue){
        if (leftValue instanceof HNode) {
            return leftValue.getChild(attrName);
        } else if (leftValue instanceof CSValue){
            return;//如 foo.bar.gar，但foo是一个宏参数，并且其值是非hdf节点
        } else {
            throw new Error("unkonw left type");
        }
    } else {//上一次递归返回空，所以这里也直接返回
        return;
    }
});

//VariableAccess只生成访问hdf节点的代码，不会去取节点的值
ast.AST_SubProp.proto("_queryHdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)) { //先取左节点
        leftValue = this.context.querySymbol(this.left.name);
    } else {
        //同样先成左值
        leftValue = this.left._queryHdfNode();
    }
    if (leftValue) {
        //分析右表达式（中括号的值）
        if (leftValue instanceof HNode){
            var rightValue = this.right.calc();
            var attr = rightValue.getString();
            if (attr) {
                return leftValue.getChild(attr);
            } else {
                //TODO notice
                return;
            }
        } else if (leftValue instanceof CSValue){
            //TODO notice
            return;
        } else {
            throw new Error("subprop right calculate error");
        }
    } else {
        return;
    }

});

//----读写分开实现----
ast.AST_VariableAccess.proto("getNodeObject", function(){
    if (isSymbol(this.target)){
        var hdfNode = this.context.querySymbol(this.target.name);
        if (hdfNode instanceof HNode){
            return hdfNode;
        } else {
            return this.context.updateScopeSymbolToNode(this.target.name);
        }
    } else if (this.target instanceof ast.AST_Prop){
        return this.target._fetchOrCreatehdfNode();//这里还是有可能为空的，比如 foo[empty]
    }
});

ast.AST_DotProp.proto("_fetchOrCreatehdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)){
        leftValue = this.context.querySymbol(this.left.name);
        if (leftValue instanceof CSValue){//只有宏参数为CSValue才会进这个逻辑
            var newLeftValueNode = this.context.updateScopeSymbolToNode(this.left.name);
            newLeftValueNode.setValue(leftValue.getString());
            leftValue = newLeftValueNode;
        }
    } else {
        leftValue = this.left._fetchOrCreatehdfNode();
        if (!leftValue){
            //TODO notice
            return;
        }
    }

    var attrName = this.right.name || this.right.literalValue; //right为AST_Symbol或AST_Number
    var node = leftValue.getChild(attrName);
    if (!node) node = this.context.createHNode(leftValue, attrName);
    return node;
});

ast.AST_SubProp.proto("_fetchOrCreatehdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)) { //先取左节点
        leftValue = this.context.querySymbol(this.left.name);
        if (leftValue instanceof CSValue){//只有宏参数为CSValue才会进这个逻辑
            var newLeftValueNode = this.context.updateScopeSymbolToNode(this.left.name);
            newLeftValueNode.setValue(leftValue.getString());
            leftValue = newLeftValueNode;
        }
    } else {
        //同样先成左值
        leftValue = this.left._fetchOrCreatehdfNode();
        if (!leftValue){
            //TODO notice
            return;
        }
    }

    //分析右表达式（中括号的值）
    var attrName = this.right.calc().getString();
    if (attrName) {
        var node = leftValue.getChild(attrName);
        if (!node) node = this.context.createHNode(leftValue, attrName);
        return node;
    } else {
        //don't notice, it will occur after soon.
        return;
    }

});

