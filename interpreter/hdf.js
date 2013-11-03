var left_braces = "{",
    right_braces = "}";

var TOKEN_TYPE = {
    KEY: {name:"key"},
    LINE: {name:"line"},
    VAL: {name:"value"},
    CR : {name:"CR"},
    EOF: {name:"EOF"},
    WHITESPACE:{name: "WHITESPACE"},
    TOKENS: {name:"{}.=:"}
};
var all_rules = [
    [/^[ \t]+/, TOKEN_TYPE.WHITESPACE],
    [/^[\w\d-_]+/, TOKEN_TYPE.KEY],
    [/^[{}.=:]/, TOKEN_TYPE.TOKENS],
    [/^(?:\r\n|\r|\n)+/, TOKEN_TYPE.CR],
    [/^.*(?=\r\n|\n|\r|$)/, TOKEN_TYPE.VAL]
];

function HNode(name){
    //this._val = null;
    //this.children = {};
    //this._len = 0;
    this.name = name;
}

HNode.prototype = {
    getValue: function(){
        return this._val;
    },
    setValue: function(val){
        this._val = val;
    },
    setChild: function(name, childNode){
        this.children = this.children || {};
        this._len = this._len || 0;

        if (!this.children[name]){
            this._len++;
            this.children[name] = childNode;
        }
    },
    getChild: function(name){
        return this.children && this.children[name];
    },
    findChild: function(path){//支持形如 foo["a.b.c"]的访问
        if (typeof path == "number") path = path + "";//支持传0
        if (!path) return ;
        var pathkeyArr = (path + "").split("."), node = this, key, i;
        for (i = 0; i < pathkeyArr.length; i++){
            key = pathkeyArr[i];
            if (key == "") {
                return;
            } else {
                node = node.getChild(key);
            }
        }
        return node;
    },
    eachChild: function(handler, that){
        if (typeof handler != "function") return;
        for (var i in this.children){
            if (this.children.hasOwnProperty(i)){
                handler.call(that, this.children[i]);
            }
        }
    },
    childrenSize: function(){
        return this._len;
    }
}

function Lexer(input){
    this._input = input;
    this.pos = {
        line : 1,
        colum: 0
    };
    this.maText = "";
    this.stateStack = ["INITIAL"];
}

Lexer.prototype.next = function (){
    var rule, tokenType, i;
    var _rules = all_rules;
    var state = this.stateStack[this.stateStack.length - 1];

    if (state == "ST_VALUE"){
        var _rules = null;
        for(i = 0; all_rules[i]; i++){
            _rules = all_rules[i];
            if (_rules[1] == TOKEN_TYPE.VAL) break;
        }
        _rules = [_rules];
    }

    if (!this._input){
        //到达文件尾
        return TOKEN_TYPE.EOF;
    } else {
        var ma, useNext = false, rule;
        for(var i = 0; _rules[i]; i++){
            rule = _rules[i];
            if (ma = this._input.match(rule[0])) break;
        }

        if (ma){
            this.maText = ma[0];
            var lineMatch = (this.maText + "").match(/\r\n|\n|\r/g);
            if (lineMatch){
                this.pos.line += lineMatch.length;
            }
            switch(rule[1]){
                case TOKEN_TYPE.KEY://key
                    this._input = this._input.substring(ma[0].length);
                    tokenType = TOKEN_TYPE.KEY;
                    break;
                case TOKEN_TYPE.TOKENS://tokens char
                    this._input = this._input.substring(ma[0].length);
                    tokenType = ma[0];

                    if (tokenType == "="){
                        this.stateStack.push("ST_VALUE");
                    }
                    if (tokenType == ":"){
                        this.stateStack.push("ST_VALUE");
                        this.val_copy_flag = true;
                    }
                    break;
                case TOKEN_TYPE.CR://endline
                    this._input = this._input.substring(ma[0].length);
                    //tokenType = TOKEN_TYPE.CR;
                    useNext = true;
                    if (this.stateStack[this.stateStack.length - 1] == "ST_VALUE"){
                        this.stateStack.pop();
                    }
                    break;
                case TOKEN_TYPE.VAL://value string
                    this._input = this._input.substring(ma[0].length);//把当前匹配的文本删掉
                    if (this.val_copy_flag){
                        this.val_copy_flag = false;
                    }
                    tokenType = TOKEN_TYPE.VAL;
                    this.maText = this.maText.replace(/^\s+/, "");//trim left
                    this.stateStack.pop();
                    break;
                case TOKEN_TYPE.WHITESPACE://tokenType = null;//空白，跳过，继续扫描
                    this._input = this._input.substring(ma[0].length);
                    useNext = true;
                    break;
                default:
                    break;
            }
        } else {
            makeError("unknow character!");
        }
    }

    if (useNext){
        return this.next();
    } else {
        return tokenType;
    }

};

// hdf -> hdfItem EOF
//
// hdfItem -> path val hdfItem
//         ->
//
// path -> key ph
//
// ph -> "." key
//    ->
//
// val-> "=" value
//    -> "{" hdfItem "}"
//    -> ":" value

function Parser(lexer){
    this.lexer = lexer;
    this.getToken();
}

Parser.prototype.getToken = function(){
    this.tok = this.lexer.next();
    this.tokenText = this.lexer.maText;
    //this.pos = extend({}, this.lexer.pos);
};

Parser.prototype.match = function(expectTokenType){
    if (expectTokenType == this.tok){
        var ret = {
            tokenText: this.tokenText
        };
        this.getToken();
        return ret;
    } else {
        makeError("Token:" + (expectTokenType.name || expectTokenType) + " is not got.");
    }
}

Parser.prototype.lookahead = function(argument) {
    return this.tok;
};

Parser.prototype.hdf = function(){
    var result = this.root = new HNode();
    this.hdfItem(result);
    this.match(TOKEN_TYPE.EOF);
    return result.children || {};
};

Parser.prototype.hdfItem = function(node){
    if (this.lookahead() == TOKEN_TYPE.KEY) {
        var ret = this.path(node);
        //这里非常绕，首先是根据path返回根节点，然后在再根节点上对path的名字赋值
        var _v = this.val(ret.node, ret.name);
        ret.node.setChild(ret.name, _v);
        this.hdfItem(node);
    } else if (this.lookahead() == right_braces){
        return null;
    } else if (this.lookahead() == TOKEN_TYPE.EOF){
        return null;
    } else {
        makeError("hdf item error");
    }
};

Parser.prototype.path = function(rootNode){
    var r = this.match(TOKEN_TYPE.KEY);//得到键名字 !important

    var keyText = r.tokenText;

    var r = this.ph(rootNode, keyText);
    if (r){
        return r;
    } else {
        return {
            "node":rootNode,
            "name":keyText
        }
    }
};

Parser.prototype.ph = function(rootNode, name){
    if (this.lookahead() == "."){
        var newHNode = rootNode.getChild(name);
        if (!newHNode){
            rootNode.setChild(name, newHNode = new HNode(name));
        }

        this.match(".");
        return this.path(newHNode);
    } else if (this.lookahead() == "=" || this.lookahead() == left_braces || this.lookahead() == ":"){
        return null;//ε空生产式
    } else {
        makeError("unexpected token:" + this.lookahead());
    }
};

Parser.prototype.val = function(rootNode, name){
    if (this.lookahead() == "=") {
        this.match("=");
        var r = this.match(TOKEN_TYPE.VAL);

        var newHNodeInVal = rootNode.getChild(name);
        if (!newHNodeInVal){
            rootNode.setChild(name, newHNodeInVal = new HNode(name))//这个逻辑可以保证同名path不会被覆盖
        }

        newHNodeInVal.setValue(r.tokenText);
        return newHNodeInVal;
    } else if (this.lookahead() == left_braces) {
        this.match(left_braces);

        var newHNodeInVal = rootNode.getChild(name);
        if (!newHNodeInVal){
            rootNode.setChild(name, newHNodeInVal = new HNode(name))//这个逻辑可以保证同名path不会被覆盖
        }

        this.hdfItem(newHNodeInVal);
        this.match(right_braces);
        return newHNodeInVal;
        //performAction
    } else if (this.lookahead() == ":") {
        this.match(":");
        var r = this.match(TOKEN_TYPE.VAL);

        var newHNodeInVal = rootNode.getChild(name);
        if (!newHNodeInVal){
            rootNode.setChild(name, newHNodeInVal = new HNode(name))//这个逻辑可以保证同名path不会被覆盖
        }
        if (r.tokenText == ""){
            //这个时候它的值是undefined
        } else {
            var pathcpArr = r.tokenText.split("."), node = this.root;
            for(var i = 0; i < pathcpArr.length; i++){
                if (!pathcpArr[i]) {
                    makeError("copy a value on an error path:" + r.tokenText);
                }
                node = node.getChild(pathcpArr[i]);
                if (!node) {
                    makeError("copy a not exist node value");
                }
            }
            //newHNodeInVal = node;//这里不应该把被copy的节点返回，而是要把其内容返回
            //具体来说就是value, children
            newHNodeInVal.setValue(node.getValue());
            newHNodeInVal.children = node.children;
            newHNodeInVal._len = node._len;
            //!要保留自己的名字
        }
        return newHNodeInVal;
    } else {
        makeError("unexpected token:" + this.lookahead());
    }
};

function makeError(message){
    throw new Error(message);
};


/**
 * @param String str hdf 格式字符串
 * @return JSON  JSON格式的数据
 */
function parseHdfStr(str){
    var lexer = new Lexer(str + "");
    var hdfParser = new Parser(lexer);
    return hdfParser.hdf();
}

exports.parse = parseHdfStr;
exports.HNode = HNode;
exports.dumpHdf = function dumpHdf(hdf){
    var buffer = "";
    var deep = [];
    function print(str){
        buffer += str;
    }

    function print_node(node){
        deep.push(node.name);

        if (node.getValue() !== undefined){
            print(deep.join("."));
            print(" =");
            print(node.getValue());
            print("\n");
        }
        var _len = node.childrenSize();
        if (_len){
            for(var name in node.children){
                if (node.children.hasOwnProperty(name)){
                    print_node(node.getChild(name));
                }
            }
        }
        deep.pop();
    }

    for(var i in hdf){
        if (hdf.hasOwnProperty(i)){
            print_node(hdf[i]);
        }
    }
    return buffer;
};
