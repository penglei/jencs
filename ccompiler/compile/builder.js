var fs = require("fs"),
    path = require("path"),
    Util = require("./util");

function LanguageGenerater(opts) {
    this._indentLevel = 0;
    this._expandtab = true; //tab to space
    this._shiftwidth = opts.shiftwidth || 4;
    this._needIndent = false;

    this._obLevel = 0;
    this._buffers = [[]]; //支持out buffer
};

LanguageGenerater.prototype = {
    constructor: LanguageGenerater,
    get: function() {
        var codes = this._buffers[0].join("");
        return {
            "codes": this._headCodes.replace(/\{LIBNAME\}/g, this.libname).replace(/\{CODE\}/g, codes),
            "header": this._headInclude.replace(/\{LIBNAME\}/g, this.libname)
        };
    },
    ob_start: function() {
        this._obLevel++;
        this._buffers.push([]);
        return this._obLevel;
    },
    ob_get: function(level) {
        level = level || this._obLevel;
        var buffer = this._buffers[level];
        this._buffers[level] = [];
        return buffer;
    },
    ob_end: function() {
        var level = this._obLevel;
        if (this.level) { //如果已经是最底层buffer，不能把buffer扔掉的（因为它就是最终的输出）
            this._buffers[level - 1] = this._buffers[level - 1].concat(this._buffers[level]);
            this._buffers.pop();
        }
        this._obLevel--;
        return this;
    },
    indent: function() {
        this._indentLevel++;
        return this;
    },
    undent: function() {
        this._indentLevel--;
        if (this._indentLevel < 0) this._indentLevel = 0;
        return this;
    },
    _getIndent: function() {
        return this._calcIndent(this._indentLevel);
    },
    _calcIndent: function(level){
        var size =  level * this._shiftwidth,
            blank = "",
            type = " ";
        if (!this._expandtab) {
            size = level;
            type = "\t";
        }
        for (var i = 1; i <= size; i++) {
            blank += type;
        }
        return blank;
    },
    //general method
    _more_args: function _more_args(args) {
        args = Util.argsToArr(args);
        var hasPerformIndent = false; //对于任何语句，只有第一句输出需要缩进

        var curBuffer = this._buffers[this._obLevel];
        for (var i = 0, len = args.length; i < len; i++) {
            if (args[i] !== undefined) {
                if (!hasPerformIndent) {
                    if (this._needIndent == true){
                        curBuffer.push(new String(this._getIndent()));
                        curBuffer[curBuffer.length - 1].indentLevel = this._indentLevel;//支持一块代码的缩进(ob)
                        this._needIndent = false;//已经缩进过就不要缩进了，必须等到下次newline才能再缩进
                    }
                    hasPerformIndent = true;
                }
                curBuffer.push(args[i]);
            }
        }
    },
    /**
     *把一块代码放进缓存，不受任何更改
     */
    pushCodes: function(blockBuffer) {
        var cil = this._indentLevel, cnil, i;
        for(var j = 0; j < blockBuffer.length; j++){
            if (blockBuffer[j].indentLevel !== undefined){//XXX 好像没什么用.
                cnil = blockBuffer[j].indentLevel;
                if (cil > this._indentLevel){
                    i = cil - _indentLevel;
                } else if (cil < this._indentLevel){
                    i = this._indentLevel - i;
                }
            }
            this._buffers[this._obLevel].push(blockBuffer[j]);
        }
    },
    echo: function() {
        this._more_args(arguments);
        return this;
    },
    //具体的功能
    semicolon: function semicolon() {
        this._more_args(arguments);
        this.echo(";");
        return this;
    },
    space: function space() {
        this.echo(" ");
        return this;
    },
    comma: function comma() {
        this.echo(",");
        this.space();
        return this;
    },
    _with_: function(typeLeft, args, typeRight) {
        this.echo(typeLeft);
        var _args = Util.argsToArr(args);
        if (Util.isFunction(_args[0])) {
            _args[0].call(_args[1]); //在回调函数里使用this
        } else {
            this._more_args(args);
        }
        this.echo(typeRight);
    },
    with_dbquotes: function with_dbquotes() {
        this._with_('"', arguments, '"');
        return this;
    },
    with_parens: function with_parens() {
        this._with_('(', arguments, ')');
        return this;
    },
    with_braces: function with_braces(arg) {
        this._with_('{', arguments, '}');
        return this;
    },
    with_brackets: function with_brackets() {
        this._with_('[', arguments, ']');
        return this;
    },
    newline: function newline() {
        this._needIndent = false;//阻止空行只有缩进
        this.echo("\n");
        this._needIndent = true;
        return this;
    },
    //语句结束
    endline: function () {
        this.semicolon().newline();
    },
    line:function(){
        this.newline().indent();
    },
    make_string: function make_string(str) {
        str = (str + "").replace(/[\\\b\f\n\r\t\x22\x27\u2028\u2029\0]/g, function(s) {
            switch (s) {
                case "\\":
                    return "\\\\";
                case "\b":
                    return "\\b";
                case "\f":
                    return "\\f";
                case "\n":
                    return "\\n";
                case "\r":
                    return "\\r";
                case "\u2028":
                    return "\\u2028";
                case "\u2029":
                    return "\\u2029";
                case '"':
                    return '"';
                case "'":
                    return "'";
                case "\0":
                    return "\\0";
            }
            return s;
        });
        return '"' + str.replace(/\x22/g, '\\"') + '"';
    }
};

function CLanguage(opts) {
    LanguageGenerater.apply(this, [opts]);
    this.libname = opts.name;
    this.setSymbolReadCreateFlag(false);

}

var CPrototype = Util.inherit(CLanguage, LanguageGenerater);

CPrototype._headCodes = fs.readFileSync(path.resolve(__dirname, "head.c"), "utf-8");
CPrototype._headInclude = fs.readFileSync(path.resolve(__dirname, "head.h"), "utf-8");

//类型
CPrototype.T = {
    "String": 1,
    "Number": 2,
    "Interger": 3,
    "Float": 4,
    "Value": 5
};

//{ 声明
CPrototype.new_hdf_node = function(valSymbol, hdfName, last) {
    this.echo(valSymbol.name, " = NEW_HDFNODE()");
    if (!last) this.comma();
    return this;
};
CPrototype.new_val_var = function(valSymbol, name, last) {
    if (valSymbol._init_create){
        this.echo(valSymbol.name, " = INIT_VALUE").with_parens(function(){
            this.echo(valSymbol.csval_type);
            this.comma();
            this.echo(valSymbol.csval_val);
        }, this);
    } else {
        this.echo(valSymbol.name, " = NEW_VALUE()");
    }
    if (!last) this.comma();
    return this;
};

CPrototype.start_symbol_def = function(type){
    return this.echo(type, " ");
};
//}

CPrototype.setSymbolReadCreateFlag = function(f) {
    this.readCreateFlag = f;
    if (f) {
        this._fetchHdfNodeFunName = "FetchOrNewHDFNode";
        this._fetchHdfByCsValue = "FetchOrNewHDFNodeByCSValue";
    } else {
        this._fetchHdfNodeFunName = "FetchHDFNode";
        this._fetchHdfByCsValue = "FetchHDFNodeByCSValue";
    }
};


//根据确定的字符串查询一个hdf节点数据
//targetCsymbol必然有一个parentSymbol，所以参数中不需要了
CPrototype.fetch_hdf = function(targetCsymbol, nodeName, createFlag) {
    if (createFlag) this.setSymbolReadCreateFlag(true);
    else this.setSymbolReadCreateFlag(false);

    this.echo(this._fetchHdfNodeFunName);
    //if (fetched) return false;
    this.with_parens(function() {
        this.echo("&", targetCsymbol._parent.name);
        this.comma();
        this.with_dbquotes(nodeName);
        this.comma();
        this.echo("&", targetCsymbol.name);
    }, this).endline();
    targetCsymbol.inited = true;
    return this;
};

//根根某个节点的值查询一个hdf节点
CPrototype.fetch_hdf_byCSValue = function (parentSymbol, valueSymbol, targetCsymbol, createFlag) {
    if (createFlag) this.setSymbolReadCreateFlag(true);
    else this.setSymbolReadCreateFlag(false);

    this.echo(this._fetchHdfByCsValue);
    this.with_parens(function() {
        this.echo("&", parentSymbol.name);
        this.comma();
        this.echo("&", valueSymbol.name);
        this.comma();
        this.echo("&", targetCsymbol.name); //第三个参数为目标对象
    }, this).endline();
    targetCsymbol.inited = true;
    return this;
};

//取某个hdf节点上的数据
CPrototype.hdf_value = function(cNodeSymbol, valSymbol) {
    //如果cNodeSymbol对应的节点一直没有更新（没有被set指令影响，就不用再fetchValue）
    //或者，valSymbol还没有被赋值
    valSymbol.inited = true;
    this.echo("GetHDFValue"); //str属性用来保存值的
    return this.with_parens(function() {
        this.echo("&" + cNodeSymbol.name).comma();
        this.echo("&" + valSymbol.name);
    }, this).endline();
};

CPrototype.set_hdf_value = function(hdfCSymbol, value, valueType) {
    this.echo("SetHDFValue").with_parens(function() {
        this.echo("&" + hdfCSymbol.name).comma();
        if (valueType == "Literals") {
            this.echo(this.make_string(value));
        } else if (valueType == "CSValue") {
            this.echo(value.name + ".value.str"); //value is symbol
        }
    }, this);
    this.endline();
};

//{{
CPrototype.do_add = function(leftValSymbol, rightValSymbol, resultSymbol) {
    //默认的add是字符串连接操作
    this.echo("add_operate");
    this.with_parens(function() {
        this.echo("&", leftValSymbol.name);
        this.comma();
        this.echo("&", rightValSymbol.name);
        this.comma();
        this.echo("&", resultSymbol.name);
    }, this);
    this.endline();
};

CPrototype.add_left_number = function(leftVal, rightValSymbol, resultSymbol) {
    /*
    if ({rightValSymbol.name}.type & CS_T_HDFVAL || {rightValSymbol.name}.type & CS_T_STRING){
        {resultSymbol.namej}.value.lval = leftVal + atoi({rightValSymbol.name}.value.str);
    } else {//Integer
        {resultSymbol.namej}.value.lval = leftVal + {rightValSymbol.name}.value.lval;
    }
    {resultSymbol.name}.type = CS_T_INTEGER;
    */
    this.echo("if ").with_parens(function(){
        this.echo(rightValSymbol.name, ".type & CS_T_HDFVAL");
        this.echo(" || ");
        this.echo(rightValSymbol.name, ".type & CS_T_STRING");
    }, this).with_braces(function() {
        this.newline().indent();
        this.echo(resultSymbol.name, ".value.lval = ", leftVal, " + cs_parseInt(", rightValSymbol.name, ".value.str)").endline();
        this.undent();
    }, this).echo(" else ").with_braces(function() {
        this.newline().indent();
        this.echo(resultSymbol.name, ".value.lval = ", leftVal, " + ", rightValSymbol.name, ".value.lval").endline();
        this.undent();
    }, this).newline();
    this.echo(resultSymbol.name, ".type = CS_T_INTEGER").endline();
    return this;
};

CPrototype.add_left_string = function(leftVal, rightValSymbol, resultSymbol) {
    //{resultSymbol.name}.value.str = cs_str_concat(this.make_string(leftVal), {resultSymbol.name}.value.str);
    //{resultSymbol.name}.type = CS_T_STRING;
    this.echo(resultSymbol.name, ".value.str = cs_str_concat(", this.make_string(leftVal), ", ", rightValSymbol.name, ".value.str)").endline();
    this.echo(resultSymbol.name, ".type = CS_T_STRING").endline();
};

CPrototype.add_right_number = function(leftValSymbol, rightVal, resultSymbol) {
    /*
    if ({leftValSymbol.name}.type & CS_T_HDFVAL || {leftValSymbol.name}.type & CS_T_STRING){
      {resultSymbol.name}.value.lval = atoi({leftValSymbol.name}.value.str) + rightVal;
    } else {
      {resultSymbol.name}.value.lval = {leftValSymbol.name}.value.lval + rightVal;
    }
    {resultSymbol.name}.type = CS_T_INTEGER;
    */
    this.echo("if ").with_parens(function(){
        this.echo(leftValSymbol.name, ".type & CS_T_HDFVAL");
        this.echo(" || ");
        this.echo(leftValSymbol.name, ".type & CS_T_STRING");
    }, this).with_braces(function() {
        this.newline().indent();
        this.echo(resultSymbol.name, ".value.lval = cs_parseInt(", leftValSymbol.name, ".value.str) + ", rightVal).endline();
        this.undent();
    }, this).echo(" else ").with_braces(function() {
        this.newline().indent();
        this.echo(resultSymbol.name, ".value.lval = ", leftValSymbol.name, ".value.lval + ", rightVal).endline();
        this.undent();
    }, this).newline();
    this.echo(resultSymbol.name, ".type = CS_T_INTEGER").endline();
    return this;
}
CPrototype.add_right_string = function(leftValSymbol, rightVal, resultSymbol) {
    //{resultSymbol.name}.value.str = cs_str_concat({leftValSymbol.name}.value.str, this.make_string(rightVal));
    //{resultSymbol.name}.type = CS_T_STRING;
    this.echo(resultSymbol.name, ".value.str = cs_str_concat(", leftValSymbol.name, ".value.str, ", this.make_string(rightVal), ")").endline();
    this.echo(resultSymbol.name, ".type = CS_T_STRING").endline();
};

//}}

CPrototype.jdugeWith = function(cSymbol) {
    this.echo("if ").with_parens(function() {
        //this.newline().indent();
        this.echo(cSymbol.name, ".hdf")
    }, this).echo(" ");
    return this;
};

// 输出节点为的值
CPrototype.out_node = function print_out(cSymbol) {
    this.echo("print_node");
    this.with_parens("&" + cSymbol.name).endline();
    return this;
};

//输出计算结果值
CPrototype.out_value = function(valSymbol) {
    this.echo("print_value");
    this.with_parens("&" + valSymbol.name).endline();
    return this;
}

//输出一个字符串
CPrototype.out_string = function(str) {
    this.echo("print_string");
    this.with_parens(this.make_string(str));
    this.endline();
    return this;
};

CPrototype.createDefinition = function(blockScope){
    //输出当前块中所有的符号定义
    var defVaris = 0, self = this;

    if (blockScope.hdfVariables.size()) {
        defVaris++;
        this.echo("HDFNode ");
        blockScope.hdfVariables.each(function(variableSymbol, name, last) {
            self.new_hdf_node(variableSymbol, name, last);
        });
        this.endline();
    }

    if (blockScope.valueVariables.size()) {
        defVaris++;
        this.echo("CSValue ");
        blockScope.valueVariables.each(function(valSymbol, name, last) {
            self.new_val_var(valSymbol, name, last);
        });
        this.endline();
    }

    Util.eachProp(blockScope.otherVariables, function(defType, variables) {
        if (variables.size()) {
            defVaris++;
            this.start_symbol_def(defType);
            variables.each(function(s, name, last) {
                //error... console.log(this);
                self.echo(s.name);
                if (s.initValue !== undefined){
                    self.echo(" = ", s.initValue);
                }
                if (!last) self.comma();
            }, this);
            this.endline();
        }
    }, this);

    if (defVaris) this.newline();
};

exports.create = function(opts) {
    opts = opts || {};
    return new CLanguage(opts);
};
