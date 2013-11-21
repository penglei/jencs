define(function(require, exports) {

    var rkey = /\{(\w+)\}/g;
    function replaceKey(_, key) {
        if (this[key] !== undefined && this[key] !== null) {
            return this[key];
        }
        return _;
    }
    exports.format = function(str, obj) {
        if (str === undefined && str === null) return "";
        if (!obj) return str;
        str = str + "";
        return str.replace(rkey, replaceKey.bind(obj));
    };

    var inherits;
    if (Object.create){
        inherits = function (ctor, superCtor) {
            ctor.super_ = superCtor;
            ctor.prototype = Object.create(superCtor.prototype, {
                constructor: {
                    value: ctor,
                    enumerable: false,
                    writable: true,
                    configurable: true
                }
            });
        }
    } else {
        inherits = function (ctor, superCtor){
            ctor.super_ = superCtor;
            ctor.prototype = new Object;
            for(var i in superCtor.prototype){
                if (prototype.hasOwnProperty(i)) ctor.prototype[i] = superCtor.prototype[i];
            }
        }
    }
    exports.inherits = inherits;

    exports.argsToArr = function(args){
        return Array.prototype.slice.call(args, 0);
    };
});
