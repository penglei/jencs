var Scope = require("./scope");

var HNode = require("./hdf").HNode;
var CSValue = Scope.CSValue;

function subcount(arg){
    if (arg instanceof HNode){
        return new CSValue(CSValue.Number, arg.subcount());
    } else {
        return new CSValue(CSValue.Number, 0);
    }
}

function name(hdfnode){
}

function string_length(arg){
}

function string_slice(arg){
}

function string_find(arg){
}

function string_crc(){
}

function first(csvalue){
}

function last(csvalue){
}

function abs(csvalue){
}

function max(foo, bar){
}

function min(foo, bar){
}

function min(foo, bar){
}

function _(foo, bar){
}

function url_escape(){
}

function html_escape(){
}

function js_escape(){
}

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

//-------qzone-----
function bitmap_value(){
}

function bitmap_value_ex(bitmapExStr, pos, len){
    //return new ;
    //return int;
}

function bit_and(){
}

function bit_or(){
}

function json_encode(){
}

function json_decode(){
}

function html_encode(){
}

function html_decode(){
}

function uri_encode(){
}

function uri_decode(){
}

function string_firstwords_replace(){
}

Scope.addExternInterface("subcount", subcount);
Scope.addExternInterface("len", subcount);
Scope.addExternInterface("bitmap_value_ex", bitmap_value_ex);
