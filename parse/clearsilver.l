%{
    var r_macroSymbol = /[\w\d\-_\+\/\*%!@\$]/,
        r_ltrim = /^\s+/,
        r_rtrim = /\s+$/;

    function trim(str){
        return (str + "").replace(r_ltrim, "").replace(r_rtrim, "");
    }

%}

%x ST_CS_START ST_CS ST_EXTEND_CS ST_END_BRACKET ST_END_VARIABEL ST_VARIABLE_PROP ST_CS_MACRO_SYMBOL ST_CS_INCLUDE

%options flex

DNUM	([0-9]*\.[0-9]+)|([0-9]+\.[0-9]*)
LNUM	[0-9]+
HNUM	"0x"[0-9a-fA-F]+
LABEL	[a-zA-Z_\x7f-\xff][a-zA-Z0-9_\x7f-\xff]*
TOKENS [\#,.\[\]()+-/\*=%!<>?$]
ANY_CHAR [^]
MACRO_CHAR = [\w\d\-_\+\/\*%!@\$]
CS_TAG_START "<?cs"
CS_TAG_END "?>"
COMMANDS ("var"|"evar"|"lvar"|"set"|"name"|"include"|"if"|"elif"|"elseif"|"alt"|"each"|"loop"|"with"|"def"|"call"|"escape"|"/escape")

%%

<INITIAL>{CS_TAG_START} {
    this.begin("ST_CS_START");
    //return "TAG_START";//加入TAG_START token让语法分析会变得麻烦，LALR(1)只能向前查看一个token
}

<ST_CS_START>\s+\#(?!debugger\s*){ANY_CHAR}*?{CS_TAG_END} {
    //都是注释
    this.popState();
}

<ST_CS_START>\s+"#debugger"\s*{ANY_CHAR}*?(?={CS_TAG_END}) {
    this.popState();
    this.begin("ST_EXTEND_CS");
    return "T_DEBUGGER";
}

<ST_CS_START>[^#] {
    this.unput(yytext);
    this.popState();
    this.begin("ST_CS");
}

<ST_CS>\s+{COMMANDS}\s*\:\s* {
    var index = yytext.indexOf(":");
    if (index >= 0){
        yytext = trim(yytext.substring(0, index));
    }
    if (yytext == "def" || yytext == "call"){
        this.begin("ST_CS_MACRO_SYMBOL");
    } else if (yytext == "/escape"){
        //非常怪，居然有两种写法
        return "T_END_ESCAPE";
    } else if (yytext == "include"){
        this.begin("ST_CS_INCLUDE");
        return;//include 只影响词法分析
    }

    return "T_" + yytext.toUpperCase();
}

<ST_CS_INCLUDE>(['"]).*\1 {
    //jison不能很好第支持include，这需要修改它（使用一个修改过的jison)
    this.popState();
    yytext = yytext.substring(1, yytext.length - 1);

    if (typeof this.include == "function") {
        this.include(yytext);
    }
    return "T_INCLUDE";
}

<ST_CS_MACRO_SYMBOL>{MACRO_CHAR}+(?:\.{MACRO_CHAR}+)*\s*(?=\() {
    yytext = trim(yytext);
    this.popState();
    return "T_MACRO_NAME"
}

<ST_CS>"else" {
    print(yytext);
    return "T_ELSE";
}

<ST_CS>"/if" {
    return "T_END_IF";
}

<ST_CS>"/each" {
    return "T_END_EACH";
}

<ST_CS>"/loop" {
    return "T_END_LOOP";
}

<ST_CS>"/alt" {
    return "T_END_ALT";
}

<ST_CS>"/with" {
    return "T_END_WITH";
}

<ST_CS>"/def" {
    return "T_END_MACRO_DEF"
}
<ST_CS>"/escape" {
    return "T_END_ESCAPE";
}

<ST_CS>{LABEL}(?:\.{LABEL})+(?=\s*\() {
    //支持 string.length这样的function
    return "T_FUN_NAME";
}

<ST_CS>{LABEL} {
    print(yytext);
    this.begin("ST_END_VARIABEL");
    return "T_VARIABLE";
}

/*{支持数字数性，如a.1.b.2[xxx]*/
<ST_END_VARIABEL>{ANY_CHAR} {
    this.popState();
    if (yytext == "."){
        this.begin("ST_VARIABLE_PROP");
        print(yytext);
        return yytext;
    }
    this.unput(yytext);
}

<ST_VARIABLE_PROP>{LABEL} {
    this.popState();//弹掉状态,表示处理完属性
    this.begin("ST_END_VARIABEL");//当前属性是新的variable
    return "T_VARIABLE"
}

<ST_VARIABLE_PROP>{LNUM} {
    this.popState();//弹掉状态,表示处理完属性
    this.begin("ST_END_VARIABEL");//当前属性是新的variable
    return "PROP_NUMBER"
}

<ST_VARIABLE_PROP>[^0-9a-zA-Z_\x7f-\xff] {
    //非label和数字就要返回到正常状态,并退回吃掉的字符
    this.popState();
    this.unput(yytext);
}
/*}支持数字属性*/

<ST_CS>{HNUM} {
    return "NUMBER_HEX"
}

/*{数字分析*/
<ST_CS>{DNUM}|{LNUM} {
    print(yytext);
    return "NUMBER";
}

//把#号当成运算符，留在语法分析时进一步处理
//简单的词法分析是无法完成一元负和减法运算的区分的

/*}数字分析*/

<ST_CS>\B['"] {
    var quote = yytext;
    yytext = "";//扔掉引号
    if (this.enableEscapedString){//原cs引擎不支持\\,\",\'，所以留个开关
        while(true){
            var ch = this._input[0];
            if (ch == "\\"){
                this.input();
                yytext = yytext.substring(0, yytext.length - 1);//跳过 "\\"，取后面的转义字符
                this.input();
            } else if (ch == quote){
                this.input();//字符串结束
                yytext = yytext.substring(0, yytext.length - 1);//吃进来的字符串要丢掉
                break;
            } else if (ch !== undefined) {
                this.input();
            } else {
                //到文件末尾，留给语法分析出错
                break;
            }
        }
    } else {
        while(true){
            var ch = this._input[0];
            if (ch == quote){
                this.input();//字符串结束
                yytext = yytext.substring(0, yytext.length - 1);//吃进来的字符串要丢掉
                break;
            } else if (ch !== undefined){
                this.input();//吃进更多的字符
            } else {
                break;
            }
        }
    }
    print(yytext);
    return "STRING";
}


<ST_CS>"==" {
    print(yytext);
    return "T_IS_EQUAL";
}

<ST_CS>"!=" {
    print(yytext);
    return "T_IS_NOT_EQUAL";
}

<ST_CS>">=" {
    print(yytext);
    return "T_IS_GREATER_OR_EQUAL";
}

<ST_CS>"<=" {
    print(yytext);
    return "T_IS_SMALLER_OR_EQUAL";
}

<ST_CS>"||" {
    print(yytext);
    return "T_BOOLEAN_OR"
}

<ST_CS>"&&" {
    print(yytext);
    return "T_BOOLEAN_AND"
}

<ST_CS>{TOKENS} {
    print(yytext);
    if (yytext == ']'){
        this.begin("ST_END_BRACKET");
    }
    return yytext;
}

<ST_END_BRACKET>{ANY_CHAR} {
    //特殊处理 ".", number can property in hdf
    this.popState();
    print(yytext);
    if (yytext == "."){
        this.begin("ST_VARIABLE_PROP");
        return yytext;
    }
    this.unput(yytext);
}

<ST_CS>\s+ {
    //ignore whitespace
}


<ST_EXTEND_CS>{CS_TAG_END} {
    this.popState();
    return "TAG_END";
}

<ST_CS>{CS_TAG_END} {
    this.popState();
    return "TAG_END";
}

<INITIAL>(?!{CS_TAG_START}){ANY_CHAR}+?(?={CS_TAG_START}|$) {
    return "CONTENT";
}
/*
//用上面的正则更方便地找出所有普通文本
<INITIAL>{ANY_CHAR} {
    while(true){
        var ch = this._input[0],
            ch2 = this._input[1];
        if (ch == "<") {
            if (ch2 == "?"){
                break;//说明是cs标签开始
            } else if (ch2 !== undefined ){
                this.input();
            } else {
                break;
            }
        } else if (ch !== undefined){
            this.input();
        } else {
            break;
        }
    }
    print(yytext)
    return "CONTENT";
}
*/

<<EOF>> return "EOF";

%%

/**
 * Local variables:
 * tab-width: 4
 * c-basic-offset: 4
 * indent-tabs-mode: t
 * End:
 */
