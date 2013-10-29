var fs = require('fs');
var path = require('path');

var Parser = require("../parse/clearsilver");
var Compiler = require("../compile");

var opts = require('nomnom')
    .option('libpre', {
    default: 'CSTPL_',
    help: '生成的库前缀'
})
    .option('name', {
    help: '生成的库名字,生成{name}.c文件'
}).parse();

var source_file_path = path.normalize('test/test.cs');
var source = fs.readFileSync(source_file_path, "utf8");

var options = {
    name: opts.libpre + (opts.name || "test"),
    shiftwidth: 4
};
var ast = Parser.parse(source);
var result = Compiler.generate(ast, options);

/*
result = {
    codes:"",//生成的代码文件
    header:""
}
*/

//console.log("-------------generate result--------------");
//console.log(result.codes);

var targetCfile = path.resolve(__dirname, "test.c");
var targetHeaderFile = path.resolve(__dirname, options.name + ".h");

fs.writeFile(targetCfile, result.codes);
fs.writeFile(targetHeaderFile, result.header);
