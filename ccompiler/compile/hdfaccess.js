var ast = require("../parse/ast"),
    Scope = require("./scope"),
    Util = require("./util"),
    Interface = require("./interface");

//--------------------------//{
var Void = Interface.Void;
var def_genCode = Interface.def_genCode;
var isSymbol = Interface.isSymbol;
var isLogic = Interface.isLogic;
var Value = Interface.Value;
var CreateValue = Interface.CreateValue;
//--------------------------//}

//VariableAccess只生成访问hdf节点的代码，不会去取节点的值
def_genCode(ast.AST_VariableAccess, function(opts) {
    opts = opts || {};
    var output = this.output;

    var useInLeftOfSET = opts.useInLeftOfSET;

    if (isSymbol(this.target)) { //处理单symbol的情况，如<?cs var:foo?>
        var _symbol = this.context.hdfStartSymbol(this.target.name);
        if (_symbol instanceof Scope.HDFNodeSymbol) {
            var hdfkey = _symbol.hdfkey;
            if (useInLeftOfSET) {
                output.fetch_hdf(_symbol, hdfkey, true);
            } else {
                if (!_symbol.inited) output.fetch_hdf(_symbol, hdfkey);
            }
            this.cSymbol = _symbol;
        } else if (_symbol instanceof Value){
            this.cSymbol = _symbol;
        } else {
            //这个比较绕：在宏参数里传Void时会走这个判断
            this.cSymbol = Void;
        }
    } else if (this.target instanceof ast.AST_Prop) {

        //output.useInLeftOfSET = true;//不能这样给子树用，因为在SubProp中的右子树会继续使用VariableAccess
        var leftInfo = this.target.gen_code(opts); //SubProp或者dotProp
        if (leftInfo.isVoid) {
            this.cSymbol = Void; //Void必须被引用VariableAccess的地方显式地处理
        } else {
            this.cSymbol = this.target.cSymbol;
        }
    }
});

def_genCode(ast.AST_SubProp, function(opts) {
    var output = this.output,
        context = this.context;

    var _leftcSymbol, _cSymbol;
    var returnInfo = {};

    var useInLeftOfSET = !!(opts.useInLeftOfSET);

    if (isSymbol(this.left)) {//先取左节点(语法树的最左根节点)
        var _symbol = context.hdfStartSymbol(this.left.name);
        if (_symbol instanceof Scope.HDFNodeSymbol) {
            _leftcSymbol = _symbol;
            //_symbol.hdfkey才是对的，因为宏实参中this.left.name可能是一个alias
            var hdfkey = _symbol.hdfkey;
            if (useInLeftOfSET) {
                output.fetch_hdf(_leftcSymbol, hdfkey, true);
                //这个时候必须把该symbol子树的全部变量unint
                //如： <?cs var:foo.bar.gon?> (foo, foo_bar, foo_bar_gon all is empty)
                //     <?cs set:foo["bar" + ".gon"] = 1?> (will get temp symbol foo_node4，so foo* must be unint)
                _leftcSymbol.unsetInitedChildren(true); //当前symbol不需要重新初始化(最左叶子)
            } else {
                if (!_leftcSymbol.inited) output.fetch_hdf(_leftcSymbol, hdfkey);
            }
        } else if (_symbol instanceof Value){//在值上面取节点.
            //在Value上面进行属性操作([]或才DotProp中的 .)，导致空HDF的产生
            //也可以把当前cSymbol设为Void，然后返回，父结点再判断是否为Void，递归处理
            returnInfo.isVoid = true;
            return returnInfo;//XXX可以加更多的返回信息供VariableAccess使用
        } else {
            returnInfo.isVoid = true;
            return returnInfo;
        }
    } else {
        //同样先成左值
        var leftParseInfo = this.left.gen_code(opts); //SubProp
        if (leftParseInfo.isVoid) return leftParseInfo;
        else _leftcSymbol = this.left.cSymbol;
    }

    //分析右表达式（中括号的值）
    this.right.expr_gen_val();

    if (this.right.value.useLiterals) {//XXX 这是一个优化
        //con["a.c"] 或 con["a.c." + 1]
        //XXX 还可以优化:"a.b.c.d"就全部分开
        _cSymbol = context.hdfChildSymbol(_leftcSymbol, this.right.value.get());

        if (useInLeftOfSET) {
            output.fetch_hdf(_cSymbol, this.right.value.get(), true);
        } else {
            if (!_cSymbol.inited) output.fetch_hdf(_cSymbol, this.right.value.get());
        }
    } else {
        //Void的值也是Void !!
        var val = this.right.value.get();
        if (val == Void) { //XXX 右表达式不但可能为空，还有可能是字符串空.(但)是在节点上取空值属性节点，也为空
            returnInfo.isVoid = true;
            return returnInfo;
        } else {
            //XXX 这里还有一个优化:
            //con["a.c." + i] -> con.a.c[i]的优化
            //con[i + ".a.c"] -> con[i].a.c的优化
            _cSymbol = context.hdfTempSymbol(_leftcSymbol);

            if (useInLeftOfSET) {
                output.fetch_hdf_byCSValue(_leftcSymbol, val, _cSymbol, true);
            } else {
                if (!_cSymbol.inited) output.fetch_hdf_byCSValue(_leftcSymbol, val, _cSymbol);
            }
        }
    }

    this.cSymbol = _cSymbol;
    return returnInfo;
});

def_genCode(ast.AST_DotProp, function(opts) {
    var output = this.output,
        context = this.context;
    var _leftcSymbol;

    var returnInfo = {};

    var useInLeftOfSET = !!(opts.useInLeftOfSET);

    if (isSymbol(this.left)) {//语法树的最左根节点
        var _symbol = context.hdfStartSymbol(this.left.name);
        if (_symbol instanceof Scope.HDFNodeSymbol) {
            _leftcSymbol = _symbol;
            //_symbol.hdfkey才是对的，因为宏数中this.left.name可能是一个alias
            var hdfkey = _leftcSymbol.hdfkey;
            if (useInLeftOfSET) {
                output.fetch_hdf(_leftcSymbol, hdfkey, true);
                //节点肯定是已经存在了，但是值可能会修改，这留到SetStmt里面去做
            } else {
                if (!_leftcSymbol.inited) output.fetch_hdf(_leftcSymbol, hdfkey);
            }
        } else if (_symbol instanceof Value) { //CSValueSymbol
            returnInfo.isVoid = true;
            return returnInfo;
        } else {
            returnInfo.isVoid = true;
            return returnInfo;
        }
    } else { //foo.bar.xx的第一项"foo"
        //在target Object上面直接取
        var leftParseInfo = this.left.gen_code(opts); //先生成左值
        if (leftParseInfo.isVoid) return leftParseInfo;
        _leftcSymbol = this.left.cSymbol;
    }

    var attrName = this.right.name || this.right.value; //这时的right为AST_Symbol或AST_Number
    this.cSymbol = context.hdfChildSymbol(_leftcSymbol, attrName); //这时 . 运算符的返回值是该symbol
    if (useInLeftOfSET) {
        output.fetch_hdf(this.cSymbol, attrName, true);
    } else {
        if (!this.cSymbol.inited) output.fetch_hdf(this.cSymbol, attrName);
    }

    return returnInfo;
});
