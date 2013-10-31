var left_braces = "{",
    right_braces = "}";

var TOKEN_TYPE = {
    KEY: {name:"key"},
    LINE: {name:"line"},
    VAL: {name:"value"},
    CR : {name:"CR"},
    EOF: {name:"EOF"},
    WHITESPACE:{name: "WHITESPACE"},
    TOKENS: {name:"{}.="}
};
var all_rules = [
    [/^[ \t]+/, TOKEN_TYPE.WHITESPACE],
    [/^[\w\d-_]+/, TOKEN_TYPE.KEY],
    [/^[{}.=]/, TOKEN_TYPE.TOKENS],
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
    subcount: function(){
        return this._len;
    }
}

function Lexer(input){
    this._input = input;
    this.pos = {
        line : 0,
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
                    this._input = this._input.substring(ma[0].length);
                    tokenType = TOKEN_TYPE.VAL;
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
//

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
        makeError("Token:" + expectTokenType + " is not got.");
    }
}

Parser.prototype.lookahead = function(argument) {
    return this.tok;
};

Parser.prototype.hdf = function(){
    var result = new HNode();
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
    } else if (this.lookahead() == "=" || this.lookahead() == left_braces){
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
    } else {
        makeError("unexpected token:" + this.lookahead());
    }
};

function makeError(message){
    //TODO pos
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
