var ast = require("../parse/ast");
var Walker = require("./walker").Walker;
var HNode = require("./types").HNode;
var CSValue = require("./types").CSValue;

function Empty(){}

//{internal function
function subcount(arg){
    if (arg instanceof HNode){
        return new CSValue(CSValue.Number, arg.childrenSize());
    } else {
        return new CSValue(CSValue.Number, 0);
    }
}

function name(hdfnode){
    if (hdfnode instanceof HNode){
        return new CSValue(CSValue.String, hdfnode.name);
    }
    return new CSValue(CSValue.String, "");
}

//internal function}

function  internalHtmlFilter(str){
    return str.replace(/&/g,'&amp;').replace(/>/g,'&gt;').replace(/</g,'&lt;').replace(/"/g,'&quot;').replace(/'/g, '&#39;');
}

function internalJsFilter(str){
    return str.replace(/&/g, '\\x26').replace(/\\/g, "\\0x5C").replace(/"/g, "\\x22").replace(/'/g, "\\x27")
            .replace(/;/g, "\\x3B").replace(/</g, "\\x3C").replace(/\//g, "\\x2F")
}

function internalUrlFilter(str){
    return encodeURIComponent(str);
}


function Context(){
    this._scopeStack = [];
    this.hdfData = {};
    this._renderSinppetsCallback = Empty;
    this._escapeFilters = [];//对输出进行过滤，后加入的filter会后运行
    this._externFilters = [];
}

Context.prototype = {
    constructor: Context,
    "enterScope": function(astNode){
        var scope = {
            "astNode": astNode,
            "nonExistParams":{},
            "symbolAlias": {}
        };
        this._scopeStack.push(scope);
        return scope;
    },
    "leaveScope": function(){
        this._scopeStack.pop();
    },
    "eachReverseScope": function(handler, that){
        for(var i = this._scopeStack.length - 1; this._scopeStack[i]; i--){
            if (handler.call(that, this._scopeStack[i])) break;
        }
    },
    "currentScope": function(){
        return this._scopeStack[this._scopeStack.length - 1];
    },
    "initHDFData": function(hdfdata){
        this.hdfData = hdfdata || {};
    },
    "_getData": function(path){
        var pathkeyArr = path.split(".");
        var node = this.querySymbol(pathkeyArr[0]), key;
        if (!node) return "";

        for (var i = 1; i < pathkeyArr.length; i++){
            key = pathkeyArr[i];
            if (key == "") return "";
            if (node instanceof CSValue){
                return "";
            } else if (node instanceof HNode){
                node = node.getChild(key);
            }

        }
        return node;
    },
    "setRenderListener": function(cb){
        if (typeof cb == "function") this._renderSinppetsCallback = cb;
    },
    //单独使用外围的filter，因为他们是先添加先运行，但是escape语法是后添加(filter)先运行。
    //这样子做的目的是方便理解(开发时的常规理解)
    "setExternalFilters": function(filters){
        this._externFilters = filters;
    },
    "output": function(str, astNode){
        if (str === undefined) return;
        try{
            str = this._runFilters(str, astNode);
            this._renderSinppetsCallback(str);
        } catch(msg){
            //do nothing
        }
    },
    "pushEscapeFilter": function(type){
        if (type == "html") this._escapeFilters.push(internalHtmlFilter);
        else if (type == "js") this._escapeFilters.push(internalJsFilter);
        else if (type == "url") this._escapeFilters.push(internalUrlFilter);
        else {
            //什么都不做，这也支持了escape:none
        }
    },
    "popEscapeFilter": function(){
        this._escapeFilters.pop();
    },
    "_runFilters": function OutputFilters(str, astNode) {
        var filter = null, i;
        //先执行externFilter
        for(i = 0; this._externFilters[i]; i++){
            filter = this._externFilters[i];
            str = filter(str, astNode);
        }
        //再执行内部的escape filters
        for(i = this._escapeFilters.length - 1; this._escapeFilters[i]; i--){
            filter = this._escapeFilters[i];
            if (astNode instanceof ast.AST_Content){
            } else {
                str = filter(str, astNode);
            }
        }
        return str;
    },

    /**
     * @return "" || HNode || CSValue  符号对应的变量(两种类型)
     */
    "querySymbol": function(key){
        var _scope = null;
        for(var i = this._scopeStack.length - 1; this._scopeStack[i]; i--){
            _scope = this._scopeStack[i];
            if (key in _scope.symbolAlias) {
                //这里有可能返回CSValue，也有可能返回HNode(宏参数是已经存在的hdf节点), 也有可能是null
                return _scope.symbolAlias[key];
            }
        }
        if (this.hdfData[key]){
            return this.hdfData[key];//HNode
        }
    },
    "getParamSymbolNonExistAst": function(name){
        var dotAccessAst;
        this.eachReverseScope(function(_scope){
            if (_scope.nonExistParams[name]) {
                dotAccessAst = _scope.nonExistParams[name];
                return true
            }
        }, this);
        return dotAccessAst;
    },
    "createHNode": function(_parent, key){
        var hdfNode = new HNode(key);
        _parent.setChild(key, hdfNode);
        return hdfNode;
    },
    "createGlobalNode": function (key) {
        var hdfNode = new HNode(key);
        this.hdfData[key] = hdfNode;
        return hdfNode;
    },
    //把一个局部参数由CSValue转成局部HNode
    "updateScopeSymbolToNode": function (key) {
        var _scope = null;
        for(var i = this._scopeStack.length - 1; this._scopeStack[i]; i--){
            _scope = this._scopeStack[i];
            if (key in _scope.symbolAlias) {
                break;
            }
        }
        var newNode = new HNode(key);
        if (_scope){
            _scope.symbolAlias[key] = newNode;
        } else {
            this.hdfData[key] = newNode;
        }
        return newNode;
    }
};

function initScopeLayer(astTree){
    var scope = astTree.parent_scope = null, macros = {}, result = {"functionsCallList":{}};
    var tree_scope_walker = new Walker(function(node, descend){
        if (node instanceof ast.AST_FunctionCall){
            if (node.id instanceof ast.AST_Symbol){
                result.functionsCallList[node.id.name] = 1;
            } else {
                result.functionsCallList[node.id] = 1;
            }
        }
        if (node instanceof ast.AST_Scope){
            var saved_scope = scope;
            if (!(node instanceof ast.AST_MacroDef)){
                //macrodef的作用域是运行时决定的。所以不需要给其加上parent_scope，等到call的时候再指定
                node.parent_scope = scope;
            } else {
                macros[node.id] = node;
                if (node.parameters.length > 1){
                    //TODO 检查去重，不允许同名形参
                }
            }
            scope = node;
            //保存上一级scope，后续与当前scope平级的定义才能找到这个scope
            descend();//手动递归下降访问，进入当前（新的）scope
            scope = saved_scope;//还原当前scope
            return true;
        }

        //XXX 如果要支持先使用，后定义，可以修改这个逻辑（先遍历一次，找出所有macro_def）
        if (node instanceof ast.AST_MacroCall){
            var macro = macros[node.id];
            if (macro){
                if (macro === scope){
                    //var errMsg = "不允许宏进行递归调用,在宏:\"" + node.id + "\"中";
                    //throw new Error(errMsg);
                }
                if (node.args.length != macro.parameters.length){
                    var i = node.args.length > macro.parameters.length;
                    var errMsg = 'call macro:"' + node.id + '" but arguments is too ' + (i? "more" : "less") + "!";
                    throw new Error(errMsg);
                }
                node.refMacro = macro;
            } else {
                var errMsg = 'call macro:"' + node.id + '" that was not been defined';
                throw new Error(errMsg);
            }
        }
    });
    astTree.walk(tree_scope_walker);
    return result;
}

Context.prototype._getLoopScopeByIterSymbol = function(symbol){
    if (symbol instanceof ast.AST_VariableAccess && symbol.target instanceof ast.AST_Symbol) {
        var i = this._scopeStack.length - 1;
        for (; this._scopeStack[i]; i--) {
            if (this._scopeStack[i].loopVarName == symbol.target.name) return this._scopeStack[i];
        }
    }
};

Context.prototype.first = function JudgeLoopFirst(symbol){
    var _scope = this._getLoopScopeByIterSymbol(symbol);
    if (_scope && _scope.isLoopFirst) {
        return new CSValue(CSValue.Number, 1);
    } else {
        return new CSValue(CSValue.Number, 0);
    }
};

Context.prototype.last = function JudgeLoopLast(symbol){
    var _scope = this._getLoopScopeByIterSymbol(symbol);
    if (_scope && _scope.isLoopLast) {
        return new CSValue(CSValue.Number, 1);
    } else {
        return new CSValue(CSValue.Number, 0);
    }
};

Context.prototype.externInterface = {
    "subcount": subcount,
    "len": subcount,
    "name": name
};

Context.prototype.getExternInterface = function(id){
    return this.externInterface[id];
};

exports.addExternInterface = function(id, fun){
    Context.prototype.externInterface[id] = fun;
};

exports.initScopeLayer = initScopeLayer;

exports.Context= Context;

exports.jsonEncode = internalJsFilter;
exports.htmlEncode = internalHtmlFilter;
exports.urlEncode = internalUrlFilter;
