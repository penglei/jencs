var Scope = require("./scope"),
    Builder = require("./builder"),
    Generater = require("./generater");

/**
 * @param AST_Node ast 语法分析树
 * @return String 生成的目标代码
 */
exports.generate = function(ast, opts){
    var output, context;
    context = Scope.parse(ast);
    opts = opts || {};
    output = Builder.create(opts);
    Generater.compile(ast, output, context);//编译生成代码
    return output.get();
}

