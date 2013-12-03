var left_braces = "{",
    right_braces = "}";

var TOKEN_TYPE = {
    KEY: {name:"key"},
    LINE: {name:"line"},
    VAL: {name:"value"},
    CR : {name:"CR"},
    EOF: {name:"EOF"},
    WHITESPACE:{name: "WHITESPACE"},
    TOKENS: {name:"{}.=:<<"}
};
var all_rules = [
    [/^[ \t]+/, TOKEN_TYPE.WHITESPACE],
    [/^[\w\d-_]+/, TOKEN_TYPE.KEY],
    [/^(?:[{}.=:]|<<)/, TOKEN_TYPE.TOKENS],
    [/^(?:\r\n|\r|\n)+/, TOKEN_TYPE.CR],
    [/^.*(?=\r\n|\n|\r|$)/, TOKEN_TYPE.VAL]
];

var ml_value_rule = [/^\s*(\w+)(?:\r\n|\n|\r)([\s\S]*)(?:\r\n|\n|\r)\1(?=\r\n|\n|\r|$)/, TOKEN_TYPE.VAL];

var r_multiline = /\r\n|\n|\r/g;
var r_val_left_space = /^[ ]+/;

function HNode(name){
    this._val = "";
    this.children = null;//children的hash表，加快查找儿子
    this._len = 0;
    this.child = null;//第一个儿子
    this.last_child = null;//最后一个儿子，加快set new child的访问速度
    this.next = null;//儿子间的兄弟关系
    //this.top = null;//父结点，暂时不用
    this.name = name;
}

HNode.prototype = {
    getValue: function(){
        return this._val;
    },
    setValue: function(val){
        this._val = val;
    },
    setChild: function(name, childNode){//name不支持foo.bar.gar形式
        this.children = this.children || {};
        this._len = this._len || 0;

        if (!this.children[name]){
            this._len++;
            this.children[name] = childNode;//儿子都放在这里加快访问

            if (!this.child) {//第一个儿子
                this.child = childNode;
                this.last_child = childNode;
            } else {//其它的儿子作为相应的兄弟
                //hdf支持引用，但那只影响value和children，不影响兄弟节点关系，因此没有循环
                //参见 ":"的实现代码
                this.last_child.next = childNode;
                this.last_child = childNode;
            }
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
    //直接根据key的排序遍历子节占，注意cs中不能使用这方法
    //cs只能通过child指针遍历链表，否帽与官方的遍历顺序不同
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
        column: 0
    };
    this.maText = "";
    this.stateStack = ["INITIAL"];
}

Lexer.prototype.next = function (){
    var rule, tokenType, i;
    var _rules = all_rules;
    var currentState = this.stateStack[this.stateStack.length - 1];

    if (currentState == "ST_VALUE"){
        _rules = null;
        for(i = 0; all_rules[i]; i++){
            _rules = all_rules[i];
            if (_rules[1] == TOKEN_TYPE.VAL) break;
        }
        _rules = [_rules];
    } else if (currentState == "ST_ML_VALUE") {
        _rules = [ml_value_rule];
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
            var lineMatch = (this.maText + "").match(r_multiline);
            if (lineMatch){
                this.pos.column = 0;
                this.pos.line += lineMatch.length;
            } else
                this.pos.column += this.maText ? this.maText.length : 0;

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
                    if (tokenType == "<<"){
                        this.stateStack.push("ST_ML_VALUE");
                    }
                    break;
                case TOKEN_TYPE.CR://endline
                    this._input = this._input.substring(ma[0].length);
                    tokenType = TOKEN_TYPE.CR;
                    if ( currentState == "ST_VALUE" || currentState  == "ST_ML_VALUE"){
                        this.stateStack.pop();
                    }
                    //useNext = true;
                    break;
                case TOKEN_TYPE.VAL://value string
                    this._input = this._input.substring(ma[0].length);//把当前匹配的文本删掉
                    if (this.val_copy_flag){
                        this.val_copy_flag = false;
                    }
                    tokenType = TOKEN_TYPE.VAL;
                    if (currentState ==  "ST_ML_VALUE"){
                        this.maText = ma[2];//[1]是EOM
                    } else {
                        this.maText = this.maText.replace(r_val_left_space, "");//trim left space
                    }
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
            makeError("unknow character!", this.pos);
        }
    }

    if (useNext){
        return this.next();
    } else {
        return tokenType;
    }

};

// $hdf -> CR $hdfItem EOF
//      -> $hdfItem
//
// $hdfItem -> $path $value $hdfItemMore
//          ->
//
// $hdfItemMore -> CR $hdfItem
//              ->
//
// $path -> KEY $ph
//
// $ph -> "." KEY
//     ->
//
// $value-> "=" $val
//       -> "{" CR $hdfItem "}"
//       -> ":" VAL
//       -> "<<" VAL
//
// $val -> VAL
//      ->

function Parser(lexer){
    this.lexer = lexer;
    this.getToken();
}

Parser.prototype._error = function(message){
    makeError(message, this.lexer.pos);
};

Parser.prototype._unexpectTokenError = function(tokenType){
    var tok = this.lookahead();
    makeError("Unexpect Token:'" + (tok.name || tok) + "' is got.", this.lexer.pos);
};

Parser.prototype.getToken = function(){
    this.tok = this.lexer.next();
    this.tokenText = this.lexer.maText;
};

Parser.prototype.match = function(expectTokenType){
    if (expectTokenType == this.tok){
        var ret = {
            tokenText: this.tokenText
        };
        this.getToken();
        return ret;
    } else {
        makeError("Expect Token:'" + (expectTokenType.name || expectTokenType) + "' is not got.", this.lexer.pos);
    }
}

Parser.prototype.lookahead = function(argument) {
    return this.tok;
};

Parser.prototype.hdf = function(){
    var result = this.root = new HNode();
    if (this.lookahead() == TOKEN_TYPE.CR){
        this.match(TOKEN_TYPE.CR);
    }
    this.hdfItem(result);
    this.match(TOKEN_TYPE.EOF);
    return result.children || {};
};

Parser.prototype.hdfItemMore = function(node){
    if (this.lookahead() == TOKEN_TYPE.CR){
        this.match(TOKEN_TYPE.CR);
        this.hdfItem(node);
    }
};

Parser.prototype.hdfItem = function(node){
    if (this.lookahead() == TOKEN_TYPE.KEY) {
        var ret = this.path(node);
        //这里非常绕，首先是根据path返回根节点，然后在再根节点上对path的名字赋值
        var _v = this.value(ret.node, ret.name);
        this.hdfItemMore(node);
    } else if (this.lookahead() == right_braces){
        return null;
    } else if (this.lookahead() == TOKEN_TYPE.EOF){
        return null;
    } else {
        this._unexpectTokenError();
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
    var nextToken = this.lookahead();
    if (nextToken == "."){
        var newHNode = rootNode.getChild(name);
        if (!newHNode){
            rootNode.setChild(name, newHNode = new HNode(name));
        }

        this.match(".");
        return this.path(newHNode);
    } else if (nextToken == "=" || nextToken == left_braces || nextToken == ":" || nextToken == "<<"){
        return null;//ε空生产式
    } else {
        this._unexpectTokenError();
    }
};

Parser.prototype.value = function(rootNode, name){
    var nextToken = this.lookahead();
    if (nextToken == "=") {
        this.match(nextToken);
        return this.val(rootNode, name);
    } else if ( nextToken == "<<") {
        this.match(nextToken);

        var r = this.match(TOKEN_TYPE.VAL);
        var newHNodeInVal = rootNode.getChild(name);
        if (!newHNodeInVal){
            rootNode.setChild(name, newHNodeInVal = new HNode(name))//这个逻辑可以保证同名path不会被覆盖
        }

        newHNodeInVal.setValue(r.tokenText);
        return newHNodeInVal;
    } else if (nextToken == left_braces) {
        this.match(left_braces);
        this.match(TOKEN_TYPE.CR);

        var newHNodeInVal = rootNode.getChild(name);
        if (!newHNodeInVal){
            rootNode.setChild(name, newHNodeInVal = new HNode(name))//这个逻辑可以保证同名path不会被覆盖
        }

        this.hdfItem(newHNodeInVal);
        this.match(right_braces);
        return newHNodeInVal;
        //performAction
    } else if (nextToken == ":") {
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
                    this._error("copy a value on an error path:" + r.tokenText);
                }
                node = node.getChild(pathcpArr[i]);
                if (!node) {
                    this._error("copy a not exist node value");
                }
            }
            //newHNodeInVal = node;//这里不应该把被copy的节点返回，而是要把其内容返回
            //具体来说就是value, children
            newHNodeInVal.setValue(node.getValue());
            newHNodeInVal.children = {};
            node.eachChild(function(hn){
                newHNodeInVal.setChild(hn.name, hn);
            });
            newHNodeInVal._len = node._len;
            newHNodeInVal.child = node.child;
            newHNodeInVal.last_child = node.last_child;
            //!要保留自己的名字
        }
        return newHNodeInVal;
    } else {
        this._unexpectTokenError();
    }
};

Parser.prototype.val = function(rootNode, name){
    if (this.lookahead() == TOKEN_TYPE.VAL){
        var r = this.match(TOKEN_TYPE.VAL);
        var newHNodeInVal = rootNode.getChild(name);
        if (!newHNodeInVal){
            rootNode.setChild(name, newHNodeInVal = new HNode(name))//这个逻辑可以保证同名path不会被覆盖
        }

        newHNodeInVal.setValue(r.tokenText);
        return newHNodeInVal;
    }
};


function makeError(message, pos){
    if (pos){
        message += " Position at Line: " + pos.line + ", Column:" + pos.column
    }
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

        var value = node.getValue();
        if (value !== ""){
            print(deep.join("."));

            if (r_multiline.test(value)){
                print(" << EOM\n");
                print(value);
                print("\nEOM");
            } else {
                print(" = ");
                print(value);
            }
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

    if (hdf.name){
        print_node(hdf);
    } else {
    //TODO 根节点被特殊处理了，考虑统一
        for(var i in hdf){
            if (hdf.hasOwnProperty(i)){
                print_node(hdf[i]);
            }
        }
    }
    return buffer;
};
