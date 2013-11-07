var HNode = require("./hdf").HNode;
CSValue.String = 1;
CSValue.Number = 2;

function Empty(){}

function CSValue(type, value){
    this.type = type || CSValue.String;
    this.value = (value !== undefined ? value : "");//默认用空字符串比较方便处理
}

CSValue.prototype.getNumber = function(){
    if (this.type == CSValue.Number) return this.value;
    if (this.type == CSValue.String) {
        var v = parseInt(this.value);
        return isNaN(v) ? 0 : v;
    }
};

CSValue.prototype._r_num_ = /^\d+$/;
CSValue.prototype.getString = function () {
    return this.value !== undefined ? this.value + "" : "";
};

CSValue.prototype.isTrue = function(){
    if (this.type == CSValue.Number) return this.value != 0;
    else {
        //我们要看value是不是全由数字组成的，如果是，就转换为数字
        //并且，转换的时候只考虑值为10进制的情况
        //这是原版cs引擎的逻辑
        if (this._r_num_.test(this.value)) return parseInt(this.value);
        return this.value.length != 0;//只需要看看是不是空串
    }
};

exports.CSValue = CSValue;
exports.HNode = HNode;
