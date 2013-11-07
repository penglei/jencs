var Scope = require("./scope");

var CSValue = require("./types").CSValue;

function getStringCSValue(obj){
    if (obj instanceof CSValue){
        obj.value = obj.getString();
        obj.type = CSValue.String;
        return obj;
    } else {
        return new CSValue(CSValue.String, obj.getValue() + "");
    }
}

function getNumberCSValue(obj){
    if (obj instanceof CSValue){
        obj.value = obj.getNumber();
        obj.type = CSValue.Number;
        return obj;
    } else {
        var v = parseInt(obj.getValue(), 10);
        return new CSValue(CSValue.String, isNaN(v) ? 0: v);
    }
}

//===================

function string_length(str) {
    str = getStringCSValue(str);
    return new CSValue(CSValue.Number, str.value.length);
}

function string_slice(obj, start, end){
    obj = getStringCSValue(obj);

    start = getNumberCSValue(start).value;
    end = getNumberCSValue(end).value;

    if (start < 0) {
        if (end > 0 || end < start) return new CSValue();

        start = obj.value.length + start;
        end = obj.value.length + end;
        if (start < end){
            return new CSValue(CSValue.String, obj.value.substring(start, end));
        }
        return new CSValue();

    } else {
        //不要修改原来的obj!
        if (start < end) return new CSValue(CSValue.String, obj.value.substring(start, end));
        return new CSValue();
    }
}

function string_find(obj, pattern){
    var str = getStringCSValue(obj);
    pattern = getStringCSValue(pattern);

    return new CSValue(CSValue.Number, str.value.indexOf(pattern.value));
}

function string_crc(obj){
    return obj;
}

function first(csvalue){
}

function last(csvalue){
}

function abs(csvalue){
    var r = getNumberCSValue(csvalue);
    r.value = Math.abs(r.value);
    return r;
}

function max(foo, bar){
    foo = getNumberCSValue(foo);
    bar = getNumberCSValue(bar);
    return foo.getNumber() > bar.getNumber() ? foo : bar;
}

function min(foo, bar){
    foo = getNumberCSValue(foo);
    bar = getNumberCSValue(bar);
    return foo.getNumber() < bar.getNumber() ? foo : bar;
}


function _(foo){
    return getStringCSValue(foo);
}

/*
function url_escape(){
}

function html_escape(){
}

function js_escape(){
}
*/

function text_html(){
}

function html_strip(){
}

function url_validate(){
}

function css_url_validate(){
}

function null_escape(){
}

//-------qz-----
function bitmap_value(){
}

var ExMap = {
    "0": "0000",
    "1": "0001",
    "2": "0010",
    "3": "0011",
    "4": "0100",
    "5": "0101",
    "6": "0110",
    "7": "0111",
    "8": "1000",
    "9": "1001",
    "a": "1010",
    "b": "1011",
    "c": "1100",
    "d": "1101",
    "e": "1110",
    "f": "1111"
};

function bitmap_value_ex(bitmapEx, pos, len){
    bitmapEx = getStringCSValue(bitmapEx);
    var bitmapCode = "";
    for(var i = 0; i < bitmapEx.value.length; i++){
        bitmapCode += ExMap[bitmapEx.value[i].toLowerCase()];
    }

    pos = getNumberCSValue(pos);
    len = getNumberCSValue(len);

    var v = parseInt(bitmapCode.substr(bitmapCode.length - pos.value, len.value), 2);
    bitmapEx.value = isNaN(v) ? 0 : v;
    bitmapEx.type = CSValue.Number;
    return bitmapEx;
}

function bit_and(){
}

function bit_or(){
}

function json_encode(obj, type){
    if (type == 1){//表示utf-8
        obj = getStringCSValue(obj);
        obj.value = Scope.jsonEncode(obj.value);
        return obj;
    } else {
        return getStringCSValue(obj);
    }
}

function json_decode(obj, type){
}

function html_encode(obj, type){
    type = getNumberCSValue(type);
    if (type.value == 1){//表示utf-8
        obj = getStringCSValue(obj);
        obj.value = Scope.htmlEncode(obj.value);
        return obj;
    } else {
        return getStringCSValue(obj);
    }
}

function html_decode(obj, type){
}

function uri_encode(obj, type){
    if (type == 1){//表示utf-8
        obj = getStringCSValue(obj);
        obj.value = Scope.urlEncode(obj.value);
        return obj;
    } else {
        return getStringCSValue(obj);
    }
}

function uri_decode(obj, type){
}

function string_firstwords_replace(url, srcKey, replaceKey){
    url = getStringCSValue(url);
    srcKey = getStringCSValue(srcKey);
    replaceKey = getStringCSValue(replaceKey);
    if (url.value.indexOf(srcKey.value) == 0){
        url.value = url.value.replace(srcKey.value, replaceKey.value);
    }
    return url;
}

function string_firstwords_filter(url, replaceStr){
    url = getStringCSValue(url);
    replayStr = getStringCSValue(replaceStr);
    url.value = url.value.replace(replaceStr.value, "");
    return url;
}

Scope.addExternInterface("string.slice", string_slice);
Scope.addExternInterface("string.find", string_find);
Scope.addExternInterface("string.length", string_length);
Scope.addExternInterface("max", max);
Scope.addExternInterface("min", min);
Scope.addExternInterface("abs", abs);
Scope.addExternInterface("bitmap_value_ex", bitmap_value_ex);
Scope.addExternInterface("json_encode", json_encode);
Scope.addExternInterface("html_encode", html_encode);
Scope.addExternInterface("uri_encode", uri_encode);
Scope.addExternInterface("string_firstwords_replace", string_firstwords_replace);
Scope.addExternInterface("string_firstwords_filter", string_firstwords_filter);
//Scope.addExternInterface("", );
