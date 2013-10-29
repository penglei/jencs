var fs = require('fs');
var path = require('path');

var ClearSilverParser = require("../parse/clearsilver");
var AST = require("../parse/ast");
var HdfParser = require("hdf2json");

//---------------------------------------------
var csRoot = path.resolve(__dirname, '../resource/cs/');
var csIncludeRoot = path.resolve(csRoot, 'module/');

//install buffer change
ClearSilverParser.OnInclude = function(fname){
    var filePath = path.resolve(csIncludeRoot, fname);
    console.log(filePath);
    var source_include = fs.readFileSync(filePath, "utf-8");
    var ast2 = new ClearSilverParser.Parser().parse(source_include);
};


var dataHdfFile = path.resolve(__dirname, "unit/test.hdf");
var entryCsFile = path.resolve(__dirname, "unit/test.cs");
// var entryCsFile = path.resolve(csRoot, "wupmain_v8.cs");

var source = fs.readFileSync(entryCsFile, "utf8");

var data_source = fs.readFileSync(dataHdfFile, "utf8");

var jsonData = HdfParser.parse(data_source);
var ast = ClearSilverParser.parse(source);


var TemplateRender = require("./render");

var result = [];
TemplateRender.render(ast, jsonData, function(snippets){
    result.push(snippets);
});

console.log(result.join(""));

