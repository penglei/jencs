function type(name) {
    return function(obj) {
        return Object.prototype.toString.call(obj) == "[object " + name + "]";
    }
}

exports.isFunction = type("Function");
exports.isObject = type("Object");
exports.isString = type("String");
exports.isArray = type("Array");

exports.argsToArr = function argsToArr(args) {
    return Array.prototype.slice.call(args, 0);
}

exports.inherit = function inherit(constructor, base) {
    for (var i in base.prototype) {
        if (base.prototype.hasOwnProperty(i)) {
            constructor.prototype[i] = base.prototype[i];
        }
    }
    return constructor.prototype;
}

exports.eachProp = function(obj, handler, that){
    for(var i in obj){
        if (obj.hasOwnProperty(i)){
            if (exports.isFunction(handler)){
                handler.call(that, i, obj[i]);
            }
        }
    }
}

exports.each = function each(arr, handler, that) {
    if (arr && exports.isFunction(handler))
    for(var i = 0; i < arr.length; i++){
        handler.call(that, arr[i], i);
    }
}
exports.makeError = function makeError(msg) {
    throw new Error(msg);
}

var i = 0;
exports.log  = function log(str){
    console.log(str || "----run----" + i++ + "----here----");
}
