var ast = require("../parse/ast"),
    CSValue = require("./types").CSValue,
    HNode = require("./types").HNode,
    def_execute = require("./executer").def_execute;

function isSymbol(astNode) {
    return astNode instanceof ast.AST_Symbol;
}

require("./math");
require("./logic");

//这其实是变量访问的一种
ast.AST_Unary.proto("$", function(){
    if (this.expression instanceof ast.AST_VariableAccess) {
        return this.expression.calc();
    } else {
        var keyVal = this.expression.calc().getString();
        if (keyVal){//不知道是不是这个逻辑，需要再仔细查看原cs引擎
            var result = this.context.querySymbol(keyVal);
            if (result){
                if (result instanceof HNode){
                    result = new CSValue(CSValue.String, result.getValue());
                } else {
                    //CSValue，不用做什么，后面直接返回
                }
                return result;
            }
        }
    }
    return new CSValue(CSValue.String, "");
});

ast.AST_Unary.proto("calc", function(){
    if (this[this.operator]) return this[this.operator]();
    return new CSValue(CSValue.String, "");
});

ast.AST_Number.proto("calc", function(){
    return new CSValue(CSValue.Number, parseInt(this.literalValue));
});

ast.AST_HexNumber.proto("calc", function(){
    return new CSValue(CSValue.Number, parseInt(this.literalValue, 16));
});

ast.AST_String.proto("calc", function(argument) {
    return new CSValue(CSValue.String, this.literalValue);
});

ast.AST_CommaExpr.proto("calc", function(){
    var expr = this.expressions.pop();//由于cs语法没有表达式内的负作用，所以前面的表达式直接扔掉
    return expr.calc();
});

ast.AST_VariableAccess.proto("calc", function(opts){//opts可以用来确定究竟是获得什么类型的值，如!号表达式
    var resultVal = this.getSymbolValueNode();
    if (resultVal){
        if (resultVal instanceof CSValue){
            return resultVal.type == CSValue.String ? resultVal : new CSValue(CSValue.String, resultVal.getString());
        } else if (resultVal instanceof HNode){
            var hdfValue = resultVal.getValue();
            return new CSValue(CSValue.String, hdfValue);
        } else {
            throw new Error("symbol value is unrecognized");
        }
    } else {
        return new CSValue();
    }
});

/**
 * 获得一个表达式的结果，与calc的区别是，对于hdfnode，它会返回节点，而不是值
 * 并且，它也可能返回null，而不是(CSValue)
 * @return NULL || CSValue || HNode
 */
ast.AST_VariableAccess.proto("getSymbolValueNode", function(){
    if (isSymbol(this.target)){
        return this.context.querySymbol(this.target.name);
    } else if (this.target instanceof ast.AST_Prop){
        var hdfnode = this.target._queryHdfNode();
        return hdfnode;
    }
});


ast.AST_DotProp.proto("_queryHdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)){
        leftValue = this.context.querySymbol(this.left.name);
    } else {
        leftValue = this.left._queryHdfNode();
    }

    var attrName = this.right.name; //right为AST_Symbol或AST_Number
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
                return leftValue.findChild(attr);
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

//----写变量分开实现----
ast.AST_VariableAccess.proto("getOrCreateNodeObject", function(){
    if (isSymbol(this.target)){
        var symbolValue = this.context.querySymbol(this.target.name);
        if (symbolValue instanceof HNode){
            return symbolValue;
        } else if (symbolValue instanceof CSValue) {
            var hdfnode = this.context.updateScopeSymbolToNode(this.target.name);
            hdfnode.setValue(symbolValue.value);//把一个Local variable转成hdf后，要操持它的值
            return hdfnode;
        } else {//undefined, so create it
            //用原来的语法树名字创建
            var hdfNodeAst = this.context.getParamSymbolNonExistAst(this.target.name);
            //TODO 还有好下面两个地方需要修改
            if (hdfNodeAst){
                var hdfnode = hdfNodeAst.getOrCreateNodeObject();
                return hdfnode;
            } else {
                //TODO 还是没找到，是在全局hdf下面创建一个好么..这与原cs引擎不符
                return this.context.createGlobalNode(this.target.name);
            }
        }
    } else if (this.target instanceof ast.AST_Prop){
        return this.target._fetchOrCreatehdfNode();//这里还是有可能为空的，比如 foo[empty]
    }
});

ast.AST_DotProp.proto("_fetchOrCreatehdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)){
        leftValue = this.context.querySymbol(this.left.name);
        if (!leftValue) {//这种情况是新建hdf节点，必然是根节点
            leftValue = this.context.createGlobalNode(this.left.name);
        } else if (leftValue instanceof CSValue){//宏参数为CSValue会, loop的循环变量也是CSValue
            var newLeftValueNode = this.context.updateScopeSymbolToNode(this.left.name);
            newLeftValueNode.setValue(leftValue.value);
            leftValue = newLeftValueNode;
        }
    } else {
        leftValue = this.left._fetchOrCreatehdfNode();
        if (!leftValue){
            //TODO notice
            return;
        }
    }

    var attrName = this.right.name; //right为AST_Symbol或AST_Number
    var node = leftValue.getChild(attrName);
    if (!node) node = this.context.createHNode(leftValue, attrName);
    return node;
});

ast.AST_SubProp.proto("_fetchOrCreatehdfNode", function() {
    var leftValue;
    if (isSymbol(this.left)) { //先取左节点
        leftValue = this.context.querySymbol(this.left.name);
        if (!leftValue){//这种情况是新建hdf节点，必然是根节点
            leftValue = this.context.createGlobalNode(this.left.name);
        } else if (leftValue instanceof CSValue){
            var newLeftValueNode = this.context.updateScopeSymbolToNode(this.left.name);
            newLeftValueNode.setValue(leftValue.value);
            leftValue = newLeftValueNode;
        }
    } else {
        //同样先成左值
        leftValue = this.left._fetchOrCreatehdfNode();
        if (!leftValue){//如果 foo[null]，这当然也要直接返回
            //TODO notice
            return;
        }
    }

    //分析右表达式（中括号的值）
    var path = this.right.calc().getString();
    if (path) {
        var pathkeyArr = path.split(".");
        var node = null, key;
        for (var i = 0; i < pathkeyArr.length; i++){
            key = pathkeyArr[i];
            if (key != "") {
                node = leftValue.getChild(key);
                if (!node) node = this.context.createHNode(leftValue, key);
                leftValue = node;
            }
        }
        return node;
    } else {
        //don't notice, it will occur after soon.
        return;
    }
});

ast.AST_FunctionCall.proto("calc", function(){

    var id = this.id;
    if (this.id instanceof ast.AST_Symbol){
        id = this.id.name;
    }

    if (id == "last" || id == "first"){//last 和 first函数的运行依赖执行环境
        return this.context[id](this.args[0]);
    }

    var argValue, i, argsList = [];

    for(i = 0; this.args[i]; i++){
        if (this.args[i] instanceof ast.AST_VariableAccess){
            argValue = this.args[i].getSymbolValueNode();
            if (!argValue) argValue = new CSValue();//对于不存在的节点传空值进去
        } else {
            argValue = this.args[i].calc();
        }
        argsList.push(argValue);
    }

    var fun = this.context.getExternInterface(id);
    if (fun) {
        return fun.apply(null, argsList) || new CSValue();
    } else {
        return new CSValue();
    }
});
