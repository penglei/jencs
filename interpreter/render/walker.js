function Walker(handler){
    this.stack = [];
    this.handler = handler;
}

Walker.prototype.visit = function(node, descendCb) {
    this.stack.push(node);
    var ret = this.handler(node, function descend(){//提供给visitor handler选择时机回调
        if (descendCb) descendCb.call(node);
    });
    //如果handler已经访问过descend，一定要返回true，这样才不能重新访问descend
    //没有显式终止对树的访问，那就继续访问
    if (!ret && descendCb){
        descendCb.call(node);
    }
    this.stack.pop();
    return ret;
};
/**
 * 寻找访问过的祖先节点
 */
Walker.prototype.parents = function(type){
    var stack = this.stack;
    for (var i = stack.length - 1; i >= 0; i--) {
        var x = stack[i];
        if (x instanceof type) return x;
    }
};

exports.Walker = Walker;

