var ast = require("../../parse/ast");
var Util = require("./util");
var HNode = require("hdf2json").HNode;

/**
 *hdf节点值的符号
 */
function CSValue(type, value){
    this.type = type || CSValue.Void;
    this.value = (value !== undefined ? value : "");//默认用空字符串比较方便处理
}

CSValue.prototype.getNumber = function(){
    if (this.type == CSValue.Number) return this.value;
    if (this.type == CSValue.String) {
        var v = parseInt(this.value);
        return isNaN(v) ? 0 : v;
    }
    if (this.type == CSValue.Void) return 0;
};

CSValue.prototype.getString = function () {
    return this.value !== undefined ? this.value + "" : "";
}


CSValue.Void = 1;
CSValue.String = 2;
CSValue.Number = 3;

function Context(){
    this._scopeStack = [];
    this.hdf = null;
    this._renderListeners = [];
}

Context.prototype = {
    constructor: Context,
    "enterScope": function(scope){
        scope.symbolAlias = {};
        this._scopeStack.push(scope);
    },
    "leaveScope": function(){
        //delete scope.symbolAlias;
        this._scopeStack.pop();
    },
    "currentScope": function(){
        return this._scopeStack[this._scopeStack.length - 1];
    },
    "initData": function(hdfdata){
        this.hdf = hdfdata;
    },
    "setRenderListener": function(cb){
        for(var i = 0; this._renderListeners[i]; i++){
            if (this._renderListeners[i] === cb) return;
        }
        this._renderListeners.push(cb);
    },
    "output": function(str){
        this._renderListeners.forEach(function(cb){
            try{
                cb(str);
            } catch(msg){
                //
            }
        }, this);
    },
    /**
     * @return HNode || CSValue 符号对应的变量(两种类型)
     */
    "querySymbol": function(key){
        var _scope = null;
        for(var i = this._scopeStack.length - 1; this._scopeStack[i]; i--){
            _scope = this._scopeStack[i];
            if (key in _scope.symbolAlias) {
                return _scope.symbolAlias[key];//这里有可能返回CSValue，也有可能返回HNode(宏参数)
            }
        }
        if (this.hdf[key]){
            return this.hdf[key];//HNode
        }
        return new CSValue(CSValue.Void);
    },
    "newSymbolNode": function(key){
        return (this.hdf[key] = new HNode());//直接扔在全局下面
    },
    "createHNode": function(_parent, key){
        var hdfNode = new HNode;
        _parent.setChild(key, hdfNode);
        return hdfNode;
    },
    "updateScopeSymbolToNode": function (key) {
        var _scope = null;
        for(var i = this._scopeStack.length - 1; this._scopeStack[i]; i--){
            _scope = this._scopeStack[i];
            if (key in _scope.symbolAlias) {
                break;
            }
        }
        var newNode = new HNode();
        if (_scope){
            _scope.symbolAlias[key] = newNode;
        } else {
            this.hdf[key] = newNode;
        }
        return newNode;
    }
};

ast.AST_Scope.proto("init_scope", function(){
    this.parent_scope = null;//父作用域
    //下面这个字段只在with和macrodef中用到。为了简单，就写在这里了
    //用来保存当前scope中变量的别名（宏参数调用时传递的hdf节点），一种是Symbol，另一种是Value
    this.symbolAlias = {};
    //this.symbolExprArgs = {};
});

exports.CSValue = CSValue;
exports.Context= Context;
        //
