%{
    var ast = require("./ast");

    //helper function
    function print(_){
        //console.log(_);
    }
    function debug(_){
        console.log(_);
    }
    function pos(yy, $pos, $pos2){
        var _p = {};
        for(var i in $pos){
            if ($pos.hasOwnProperty(i)){
                _p[i] = $pos[i]
            }
        }
        if ($pos2) {
            _p.last_line = $pos2.last_line;
            _p.last_column = $pos2.last_column;
        }
        _p.fileid = yy.fileid;//自定义的属性，这是文件名
        return _p;
    }
%}


%token T_BOOLEAN_OR   "|| (T_BOOLEAN_OR)"
%token T_BOOLEAN_AND   "&& (T_BOOLEAN_AND)"
%token T_IS_EQUAL     "== (T_IS_EQUAL)"
%token T_IS_NOT_EQUAL "!= (T_IS_NOT_EQUAL)"
%token T_IS_SMALLER_OR_EQUAL "<= (T_IS_SMALLER_OR_EQUAL)"
%token T_IS_GREATER_OR_EQUAL ">= (T_IS_GREATER_OR_EQUAL)"


%left T_BOOLEAN_OR
%left T_BOOLEAN_AND

%nonassoc T_IS_EQUAL T_IS_NOT_EQUAL

%nonassoc '<' T_IS_SMALLER_OR_EQUAL '>' T_IS_GREATER_OR_EQUAL
/*%left '<' T_IS_SMALLER_OR_EQUAL '>' T_IS_GREATER_OR_EQUAL*/

%left '+' '-'
%left '*' '/' '%'
%right '!' '?' '#' '$'
%right UMINUS "-/+ unary"

%ebnf

%start cs

%% /* language grammar */

cs:
      inner_statement_list EOF {
        var program = new ast.AST_Program($1);
        return program;
      }
    ;

statement:
      block
    | single_stmt
    ;

block:
      if
    | alt
    | each
    | loop
    | with
    | escape
    | macro_def
    ;

/*
加了更加精确地得到位置，需要<?cs 和?>两个token，这导致很多方法都是以TAG_START开始，引起归约-移进冲突.
因此，别的地方把用statement*代替，减少该list方法的使用.
否则在看到TAG_START时既可以归约为_list_repetition，也可以移进以形成block或者single_stmt
但那会破坏方法的可读性，因此最后还是去掉了TAG_START 这个 token
*/
inner_statement_list:
    statement*
    ;

if:
      T_IF expr TAG_END inner_statement_list elif_list else_single T_END_IF TAG_END {
        //console.log('if:' + $2.left.target.name);
        var ifRootAlternate;
        if ($5){
            //else是最后被归约出来的，要放到最末尾的AST_If中
            var elifstmt = $5;
            while (elifstmt.alternate){
                elifstmt = elifstmt.alternate;
            };
            elifstmt.alternate = $6;

            ifRootAlternate = $5
        } else {
            ifRootAlternate = $6
        }
        $$ = new ast.AST_If($4, $2, ifRootAlternate);
        $$.pos = pos(yy, @1, @3);
        $$.endPos = pos(yy, @7, @8);
      }
    ;

elif_list:
      elif_list T_ELIF_TOKENS expr TAG_END inner_statement_list {
        //console.log('elif:' + $3.left.target.name);
        //console.log($1);
        var alternate = new ast.AST_If($5, $3);
        alternate.pos = pos(yy, @2, @4);
        if ($5){
            var lastStmt = $5[$5.length - 1];
            alternate.endPos = pos(yy, lastStmt.endPos || lastStmt.pos);
        }
        if ($1){
            //遍历AST_If找到最下面的alternate，把当前归约出的elif放到末尾
            //归约的顺序跟代码书写顺序是一样的
            var curBranch = $1;
            while(curBranch.alternate) curBranch = curBranch.alternate
            curBranch.alternate = alternate;
            $$ = $1;
        } else {
            $$ = alternate;
        }
      }
    | /* empty */
    ;

T_ELIF_TOKENS:
      T_ELIF
    | T_ELSEIF
    ;

else_single:
      /* empty */
    | T_ELSE TAG_END inner_statement_list {
        $$ = new ast.AST_Block($3);
        /*
        if ($3){
            var firstStmt = $3[0];
            var lastStmt = $3[$3.length - 1];
            $$.endPos = pos(yy, firstStmt.pos);
        } else {
            $$.pos = pos(yy, @1, @2);
        }
        */
    }
    ;

alt:
      T_ALT expr TAG_END inner_statement_list T_END_ALT TAG_END{
        $$ = new ast.AST_Alt($4, $2);
        $$.pos = pos(yy, @1);
    }
    ;

each:
      T_EACH t_variable_one '=' expr TAG_END inner_statement_list T_END_EACH TAG_END{
        $$ = new ast.AST_Each($6, $2, $4);
        $$.pos = pos(yy, @1);
    }
    ;

with:
      T_WITH t_variable_one '=' base_variable TAG_END inner_statement_list T_END_WITH TAG_END{
        $$ = new ast.AST_With($6, $2, $4);
        $$.pos = pos(yy, @1);
    }
    ;

escape:
      T_ESCAPE STRING TAG_END inner_statement_list escape_end TAG_END {
        //check surpport 'html' and 'js' , 'url'
        $$ = new ast.AST_Escape($4, $2);
        $$.pos = pos(yy, @1);
      }
    ;
escape_end:
      T_END_ESCAPE
    | T_END_ESCAPE STRING
    ;

loop:
      T_LOOP loop_init_expr ',' expr loop_step TAG_END inner_statement_list T_END_LOOP TAG_END {
        $$ = new ast.AST_Loop($7, $2, $4, $5);
        $$.pos = pos(yy, @1);
    }
    ;

/*官方cs引擎有个bug，可以使用<?cs loop: a.b =1, 5, 1?>，但是在内部却没法取到a.b的值，
原作者可能没有考虑这样用，因此我们的语法将拒绝这种写法*/
loop_init_expr:
     t_variable_one '=' expr {
        $1.initValue = $3;
        $$ = $1;
      }
    ;

/*step其实是可以为表达式的*/
loop_step:
     ',' expr ->$2
    | ->new ast.AST_Number(1) //默认步进为1
    ;

macro_def:
      T_DEF T_MACRO_NAME '(' def_formal_parameters ')' TAG_END inner_statement_list T_END_MACRO_DEF TAG_END {
        $$ = new ast.AST_MacroDef($7, $2, $4);
        $$.pos = pos(yy, @1);
        $$.pos.last_line = @7.last_line;
        $$.pos.last_column = @7.last_column;
      }
    ;

def_formal_parameters:
      t_variable_one ->[$1]
    | t_variable_one more_parameter+ ->[$1].concat($2)
    | ->[]
    ;

more_parameter:
    ',' t_variable_one ->$2
  ;

/*
def_formal_parameters:
      def_formal_parameter
    |
    ;

def_formal_parameter:
      t_variable_one
    | def_formal_parameter ',' t_variable_one
    ;
*/

single_stmt:
      single_stmt_syntax TAG_END {
        $$ = $1;
        $$.pos = pos(yy, @1, @2);
    }
    | CONTENT {
        $$ = new ast.AST_Content($1)
        //var lines = $1.match(/(?:\r\n?|\n).*/g)
        $$.pos = pos(yy, @1);
    }
    ;

/*单句表达式*/
single_stmt_syntax:
      set_stmt
    | var_stmt
    | name_stmt
    | macro_call
    | include_stmt
    | cs_debugger
    ;

set_stmt:
      T_SET base_variable '=' expr -> new ast.AST_SetStmt($2, $4)
    ;

var_stmt:
      T_VAR expr -> new ast.AST_VarStmt($2)
    ;

name_stmt:
      T_NAME base_variable -> new ast.AST_NameStmt($2);
    ;

macro_call:
      T_CALL T_MACRO_NAME '(' parameter_list ')' -> new ast.AST_MacroCall($2, $4);
    ;

include_stmt:
      T_INCLUDE {
        if (yy.getSubAst){
            //这个时候子模板的方法分析肯定已经完成了..TODO 递归怎么办?
            var subast = yy.getSubAst($1);
            $$ = new ast.AST_Include($1, subast);
        } else {
            $$ = new ast.AST_Include($1)
        }
    }
    ;

cs_debugger:
      T_DEBUGGER -> new ast.AST_CSDebugger();
    ;

/**
 *表达式分为基本表达式、变量表达式
 *变量表达式分为带函数调用和不带函数调用的
 *基本表达式分为数学运算、逻辑表达式、字符串连接
 *基本表达式与变量表达式结合组成表达式
 *----表达式都是带返回值的
 *----模板语言的返回值是无类型的,可以全部看成是字符串
 *----逻辑表达式只返回0或者1
 *----数学表达式与非数字字符串表达式相加返回字符串
 *----数字表达式与逻辑表达式相加相当与加上0/1
 *----字符串与逻辑表达式相加相当于加上0/1数字
 */
expr:
      variable
    | expr_basic
    ;

variable:
     base_variable   /* 这是hdf访问语法（VariabelAccess） */
   | '#' base_variable  ->new ast.AST_UnaryForceNum($1, $2)  /*强制数字表达式*/
   | const_variable  /* Number or String */
   | function_call
   ;

expr_basic:
      '!' expr      ->new ast.AST_UnaryNot($1, $2)  /*非表达式*/
    | '?' expr      ->new ast.AST_UnaryExist($1, $2) /*是否存在，TODO，不应该有这种语法，应该限定为 ?foo.bar 这种*/
    | '$' expr      ->new ast.AST_Unary($1, $2)  /*比如，对于 foo + $3, 其中的3是一个key，而不是数字*/
    | expr T_BOOLEAN_OR expr            -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr T_BOOLEAN_AND expr           -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr T_IS_EQUAL expr              -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr T_IS_NOT_EQUAL expr          -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr '<' expr                     -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr '>' expr                     -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr T_IS_SMALLER_OR_EQUAL expr   -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr T_IS_GREATER_OR_EQUAL expr   -> new ast.AST_BinLogicExpr($2, $1, $3)
    | expr '+' expr -> new ast.AST_BinMathExpr($2, $1, $3)
    | expr '-' expr -> new ast.AST_BinMathExpr($2, $1, $3)
    | expr '*' expr -> new ast.AST_BinMathExpr($2, $1, $3)
    | expr '/' expr -> new ast.AST_BinMathExpr($2, $1, $3)
    | expr '%' expr -> new ast.AST_BinMathExpr($2, $1, $3)
    | '(' comma_expr ')' ->new ast.AST_CommaExpr($2) /*逗号表达式*/
    | '(' expr ')'  ->$2  /*控制优先级用（优先被归约）*/
    ;

comma_expr:
      expr comma_expr_more+ ->[$1].concat($2)
    ;

comma_expr_more:
    ',' expr ->$2
    ;

base_variable:
      t_variable_one ->new ast.AST_VariableAccess($1) /*注意这里与subProp构成a[b][c][d]的访问形式*/
    | t_variable_one t_properties {
        var leftTree = $2
        while(leftTree.left) leftTree = leftTree.left;
        leftTree.left = $1;//注意下图中的 "??" 位置
        $$ = new ast.AST_VariableAccess($2);

        /*
        debug("---------")
        debug($1)
        */
      }
    ;

/**
 *例如: a["xx"]["2"]["b_d"]

             []
            /  \
           []  b_d
          /  \
         []   2
        /  \
       ??  xx

*/
t_properties:
      t_property
    | t_properties t_property {
        $2.left = $1
        $$ = $2
        /*debug_prop
        debug("<<reduce<<")
        debug($1)
        debug($2)
        */
      }
    ;

t_property:
      '.' t_label {
        $$ = new ast.AST_DotProp($2);
        /*debug_prop
        $$ = $2
        */
      }
    | '[' expr ']' {
        $$ = new ast.AST_SubProp($2);
        /*debug_prop
        $$ = $2
        */
    }
    ;

t_label:
      t_variable_one    ->$1
    | PROP_NUMBER        ->new ast.AST_Symbol($1) /*这个不要综合到t_variable_one里面去*/
    ;

t_variable_one:
      T_VARIABLE    ->new ast.AST_Symbol($1)
    ;

/*'+' expr %prec UMINUS ->new ast.AST_UnaryPositive($1, $2) 正数表达式*/
/*'-' expr %prec UMINUS ->new ast.AST_UnaryNegative($1, $2) 正数表达式*/
const_variable:
      STRING        ->new ast.AST_String($1)      /*字面字符串*/
    | '#' t_math_number    ->$2  /*这其实是官方cs引擎的一个bug，它不支持 #"111" */
    | '#' '-' t_math_number ->new ast.AST_UnaryNegative($2, $3) /*负数表达式*/
    | '-' t_math_number     ->new ast.AST_UnaryNegative($1, $2)
    | '#' '+' t_math_number  ->$3
    | '+' t_math_number  ->$2
    | t_math_number
    ;

t_math_number:
      NUMBER        ->new ast.AST_Number($1)
    | NUMBER_HEX    ->new ast.AST_HexNumber($1)
    ;
/*函数名需要扩展支持 string.length这种有dot分隔的形式*/
/*这里使用简单的词法分析解决dot问题*/
function_call:
      T_FUN_NAME '(' parameter_list ')' -> new ast.AST_FunctionCall($1, $3)
    | t_variable_one '(' parameter_list ')' -> new ast.AST_FunctionCall($1, $3)
    ;

/*{使用ebnf可以简化参数的处理(不用在ast里面分析递归树*/
parameter_list:
      expr ->[$1]
    | expr more_parameter_list+ {
        $$ = [$1].concat($2); /*LR分析归约顺序决定第一个参数最后被规约*/
      }
    | ->[]
    ;

more_parameter_list:
    ',' expr ->$2
    ;
/*}*/

/*{bnf会生成递归参数树，这不便于处理（参数作为数组处理起来比较直观）
parameter_list:
      non_empty_parameter_list -> $1
    |
    ;

non_empty_parameter_list:
      expr ->new ast.AST_Parameter($1)
    | non_empty_parameter_list ',' expr ->new ast.AST_Parameter($1, $3)
    ;
}*/

%%

//console.log("start compile ...")

/*
 * Local variables:
 * tab-width: 4
 * c-basic-offset: 4
 * indent-tabs-mode: t
 * End:
 */
