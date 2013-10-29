var Walker = require("./walker").Walker;
var ast = require("../parse/ast");
var Util = require("./util");

function Symbol(name, type){
    this.name = name;
    this.type = type
}

function HDFNodeSymbol(name, _parent){
    Symbol.call(this,  name, "HDFNode");
    if (_parent){
        this._parent = _parent;
    }

    this.inited = false;
    //建立起父子关系
    this._children = {};
    this.valSymbol = null;
}

var HDFNodePrototype = Util.inherit(HDFNodeSymbol, Symbol);

/**
 * set指令改变一个变量后，必须更新相关变量（让其缓存失效）
 * 主要是为了改进通过var:a.b.c 和 set: a["b" + ".c"]这种方式
 */
HDFNodePrototype.unsetInitedChildren = function(flag){
    this.inited = flag;
    if (this.valSymbol) this.valSymbol.inited = false;
    this.eachChild(function(s){
        s.unsetInitedChildren(false);
    });//递归更新所有子树对应的变量为未更新状态
};

HDFNodePrototype.eachChild = function(cb, _this){
    if (typeof cb != "function") return ;
    for(var i in this._children){
        if (this._children.hasOwnProperty(i)){
            cb.call(_this, this._children[i]);
        }
    }
};

/**
 * 为了效率，我们把foo.bar.a编译成三个变量：
 * HDFNode foo, foo_bar, foo_bar_a
 * 这个时候，我们必须建立这三个变量的关系
 * (当我们要让foo的children失效重新获取的时候，才能统一操作)
 */
HDFNodePrototype.insertChild = function(childSymbol){
    if (childSymbol && childSymbol.name){
        var name = childSymbol.name;
        if (!this._children[name]){
            this._children[name] = childSymbol
            childSymbol._parent = this;//XXX 这种循环引用需要在最后手动释放掉
        } else {
            //nothing
        }
    } else {
        throw new Error("child symbol is unrecognized");
    }
};


/**
 *hdf节点值的符号
 */
function CSValueSymbol(name){
    Symbol.call(this, name, "CSValue");
    this.inited = false;
    this._init_create = false;
}

CSValueSymbol.prototype.setInitValue = function(type, val){
    this._init_create = true;
    this.csval_type = type;
    this.csval_val = val;
};

//variable definition
function Dictionary() {
    this._values = {};
    this._size = 0;
}

Dictionary.prototype = {
    set: function(key, val) {
        if (!this.has(key)) ++this._size;
        this._values["$" + key] = val;
        return this;
    },
    add: function(key, val) {
        if (this.has(key)) {
            this.get(key).push(val);
        } else {
            this.set(key, [ val ]);
        }
        return this;
    },
    get: function(key) { return this._values["$" + key] },
    del: function(key) {
        if (this.has(key)) {
            --this._size;
            delete this._values["$" + key];
        }
        return this;
    },
    has: function(key) { return ("$" + key) in this._values },
    each: function(f) {
        var size = this._size;
        for (var i in this._values){
            f(this._values[i], i.substr(1), size-- == 1);
        }
    },
    size: function() {
        return this._size;
    },
    map: function(f) {
        var ret = [];
        for (var i in this._values)
            ret.push(f(this._values[i], i.substr(1)));
        return ret;
    }
};

function convertStrToSymbol(str){
    var _typename = "";
    for(var c, i = 0; str[i]; i++){
        c = str.charAt(i);
        if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || (c == '_'))){
            c = "_" + c.charCodeAt(0);
        }
        _typename += c;
    }
    return _typename;
}

function Block(){
    this.hdfVariables = new Dictionary();//局部节点临时变量
    this.valueVariables = new Dictionary();//局部节点值临时变量
    this.otherVariables = {};//局部其它类型的临时变量
    this._level = 0;
}

//Block.prototype.createDefinition(output)

function Context(){
    this.scope = null;
    //一个block就是一个符号集合的开始
    //对于hdfNode需要特别处理，已经存在的hdfNode就不能再建立同名符号去覆盖
    //分支中对hdfNode的访问需要递归向上综合，才能保证后续不需要对hdfNode重新访问
    //每个临时的hdfNode都建立在当前block中，当前block退出后被自动释放掉
    //两个block之间可能会有变量传递，要防止变量覆盖，导致类似 if bug的出现
    this._blockStack = [];
    this._currentBlockScope = null;
}

Context.prototype = {
    constructor: Context,
    "_getBlockStackIndex": function(bs){
        for(var i = 0; i < this._blockStack.length; i++){
            if (bs == this._blockStack[i]) break;
        }
        return i;
    },
    "updateScopeSymbol": function(newValueSymbol, name){
        var scope = this._getScopeHasSymbol(name);
        if (!scope) Util.makeError("bug........");
        scope.symbolAlias[name] = newValueSymbol;
    },
    "_getScopeHasSymbol": function(hdfname){
        //首先，在scope链中寻找访问的变量(hdf节点)符号，看访问的是哪个符号
        //然后，在blockStack中寻找这个符号对应的c变量符号
        //如果没有找到，在当前block中生成一个符号(符号是否向上层block移动由“业务”控制。)
        //而每一个新生成的符号放在哪个位置(block)，是可以控制的（可随意新建block并且进入，然后“新生成的变量”全部放在这个block中
        //对于hdf及其value在新生成一个符号前，应查看当前blockStack中的符号是否已经存在了，有的话就直接使用
        //临时变量都直接入在当前block中，但是，有可能新的block中的符号（临时）符号覆盖上层block中的符号
        //而这个被覆盖的符号是会在当前block中用的，这导致当前block中对那个符号的访问变得无效
        //一种好的做法是保证每一层的临时变量符号都不同即可以规避这个问题。
        var _scope = this.scope;
        while(_scope){
            if (hdfname in _scope.symbolAlias) {
                return _scope;
            }
            _scope = _scope.parent_scope;
        }
    },
    //在目标语言的blockScopeStack 中寻找一个变量
    "_getSymbolInBlockStack": function(targetSymbolName){
        var targetBlockScope;
        for (i = this._blockStack.length; i > 0; i--){
            targetBlockScope = this._blockStack[i - 1];
            if (targetBlockScope.hdfVariables.has(targetSymbolName)){
                return targetBlockScope.hdfVariables.get(targetSymbolName);
            }
        }
    },
    //hdf根节点符号
    "_initVirtualRootSymbol": function(){
        //注意root的特殊处理
        var rootHdfSymbolName = "root",
            _rootSymbol = new HDFNodeSymbol(rootHdfSymbolName);
        _rootSymbol.inited = true;
        _rootSymbol.name = rootHdfSymbolName;
        this._rootSymbol = _rootSymbol;
    },
    //一个AST_VariableAccess的起始符号
    "hdfStartSymbol": function(hdfkey){
        var bs = this._currentBlockScope;
        var scp = this._getScopeHasSymbol(hdfkey);
        if (!scp){//如果没有找到，说明访问的是根scope(注意，这是cs语法的scope)下面的hdf变量
            //然后，我们要看blockStack中是否已经存在这个符号了
            var targetSymbolName = "hn_" + hdfkey;//(加个前辍，防止名字与root冲突
            var s = this._getSymbolInBlockStack(targetSymbolName);
            if (!s){//如果还是没有找到，这个时候需要新建一个符号了
                //这个符号肯定是在rootSymbol下的
                s = new HDFNodeSymbol(targetSymbolName, this._rootSymbol);
                s.hdfkey = hdfkey;//保存hdf的key
                //需要把这个符号放入一个blockScope中，以后可以继续使用（生成value符号时用）
                s.blockScope = this._currentBlockScope;//这样可以准确定位出这个符号所片的targetScope
                this._currentBlockScope.hdfVariables.set(targetSymbolName, s);
                return s;
            } else {//也可以直接返回
                return s;
            }
        } else {
            //找到了，说明是引用，直接返回即可，不用放入blockScope中
            return scp.symbolAlias[hdfkey];
        }
    },
    "hdfChildSymbol": function (objSymbol, attrName){
        var _attrStrName = convertStrToSymbol(attrName);
        var name = objSymbol.name + "_" + _attrStrName;
        var s = this._getSymbolInBlockStack(name);
        if (!s){//逻辑同hdfStartSymbol相同
                s = new HDFNodeSymbol(name, objSymbol);
                s.hdfkey = attrName;
                s.blockScope = this._currentBlockScope;
                this._currentBlockScope.hdfVariables.set(name, s);
                objSymbol.insertChild(s);//这是为了在set中使所有变量失效。在hdfStartSymbol中不需要的，因为root不会被set
                return s
        } else {
            return s;
        }
    },
    //@param HDFNodeSymbol objSymbol 父节点
    "hdfTempSymbol": function(objSymbol){
        var targetBlockScope = objSymbol.blockScope;
        var i = this._getBlockStackIndex(targetBlockScope);
        var name = objSymbol.name + i + "_node" + targetBlockScope.hdfVariables.size();

        var s = new HDFNodeSymbol(name, objSymbol);
        //XXX s.hdfkey = ...; 这是没有用的？要考虑局部变量set的问题，这里就有用了
        s.blockScope = this._currentBlockScope;
        this._currentBlockScope.hdfVariables.set(name, s);
        objSymbol.insertChild(s);//这种临时的变量，在macro调用的时候它是有用的，所以也要添加父子关系
        return s;
    },
    //hdf 节点值的变量符号，如a.b表达式，需要一个变量来保存a.b的值，这个变量名就是通过该方法生成的
    "getHdfValueSymbol": function(hdfSymbol){//把hdf的值放在和hdfSymbol同一级的blockScope中
        var targetBlockScope = hdfSymbol.blockScope;
        //保证符号不会覆盖（临是hdfSymbol）
        var i = this._getBlockStackIndex(targetBlockScope);
        var name = "val_" + i + "_" + hdfSymbol.name;

        if (!targetBlockScope.valueVariables.has(name)){
            var vs = new CSValueSymbol(name);
            hdfSymbol.valSymbol = vs;
            targetBlockScope.valueVariables.set(name, vs);
        }
        return targetBlockScope.valueVariables.get(name);
    },
    "newTempValueSymbol": function(){
        var bs = this._currentBlockScope;
        var i = this._getBlockStackIndex(bs);
        var name = "bs" + i + "_value" + bs.valueVariables.size();
        var symbol = new CSValueSymbol(name);

        bs.valueVariables.set(name, symbol);
        symbol.inited = true;

        return symbol;
    },
    "newSymbol": function(type, name, initValue){
        var bs = this._currentBlockScope;
        var i = this._getBlockStackIndex(bs);

        var variables = bs.otherVariables[type];
        if (!variables){
            variables = bs.otherVariables[type] = new Dictionary();
        }
        if (!name){
            name = convertStrToSymbol(type) + "_bs" + i + "_" + variables.size();
        }
        var s = new Symbol(name, type);
        variables.set(name, s);
        if (initValue !== undefined) s.initValue = initValue;
        return s;
    },
    enterBlock: function(){
        var blockScope = new Block();
        this._currentBlockScope = blockScope;
        this._blockStack.push(blockScope);
        return blockScope;
    },
    //在这个block中只定义临时变量
    enterTempBlock: function(){
        var bs = this.enterBlock();
        bs.inTempBlock = true;//用来定义临时变量
        return bs;
    },
    leaveBlock: function(){
        if (!this._blockStack.length) Util.makeError("leave block but block stack is empty!");
        var bs = this._blockStack.pop();
        this._currentBlockScope = this._blockStack[this._blockStack.length - 1];
        return bs;
    }
};

ast.AST_Scope.proto("init_scope", function(){
    this.parent_scope = null;//父作用域
    //下面这个字段只在with和macrodef中用到。为了简单，就写在这里了
    //用来保存当前scope中变量的别名（宏参数调用时传递的hdf节点），它只有两种值，一种是Symbol，另一种是Value
    this.symbolAlias = {};
    //this.symbolExprArgs = {};
});

exports.Symbol = Symbol;
exports.HDFNodeSymbol = HDFNodeSymbol;
exports.CSValueSymbol = CSValueSymbol;

exports.parse = function(astTree){
    var scope = astTree.parent_scope = null,
        macros = {};
    var tree_scope_walker = new Walker(function(node, descend){
        if (node instanceof ast.AST_Scope){
            node.init_scope();
            var saved_scope = scope;
            if (!(node instanceof ast.AST_MacroDef)){
                //macrodef的作用域是运行时决定的。所以不需要给其加上parent_scope，等到call的时候再指定
                //当然，它自己还是一个scope，只不过parent_scope确认得比较晚而已
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
                    var errMsg = "不允许宏进行递归调用,在宏:\"" + node.id + "\"中";
                    throw new Error(errMsg);
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
    var context = new Context();
    context._initVirtualRootSymbol(); //初始化root hdf节点的包装器
    return context;
};
