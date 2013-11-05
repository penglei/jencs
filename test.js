var fs = require('fs');
var path = require('path');

var CSInterpreter = require('./interpreter');
var HDF = require('./interpreter/hdf');

var Engine = CSInterpreter.Engine;
var HNode = CSInterpreter.HNode;
var CSValue = CSInterpreter.CSValue;
var AST = CSInterpreter.AST;

//设置内容\n\r\t过滤器
function ContentWhiteFilter(valueStr, astNode){
    return astNode instanceof AST.AST_Content ? valueStr.replace(/[\r\n\t]/g, '') : valueStr;
}

if (1){
    var _csRoot = path.resolve(__dirname, './resource/cs/');
    var csIncludeRoot = path.resolve(_csRoot, 'module/');
    var entryCsFile = path.resolve(_csRoot, "wupmain.cs");
    var dataHdfFile = path.resolve(__dirname, "./resource/wupdata.hdf");
} else {
    var csIncludeRoot = path.resolve(__dirname, './unit/interpreter/');
    var entryCsFile = path.resolve(__dirname, "./unit/interpreter/test.cs");
    var dataHdfFile = path.resolve(__dirname, "./unit/interpreter/test.hdf");
}


var mainCsSource = fs.readFileSync(entryCsFile, "utf8");
var dataSource = fs.readFileSync(dataHdfFile, "utf8");


/*最簡單的使用方式，無法添加其他接口
CSInterpreter.render(mainCsSource, dataSource, {
    "entryName": entryCsFile
});
*/

var TestCSEngine = new Engine();
//必须先设置inlcude的回调，否者分析源码时会找不到
TestCSEngine.setLexerInclude(function(filename){
    //console.log(filename);
    return fs.readFileSync(path.resolve(csIncludeRoot, filename), "utf8");
});
//TestCSEngine.addOutputFilter(ContentWhiteFilter);
/*
TestCSEngine.addOutputFilter(function SimpleFormatFilter(str, astNode){
    if (astNode instanceof AST.AST_Content){
        str = str.replace(/^[ \t\r\n]+$/, "");
    }
    return str;
});
*/
TestCSEngine.setConfig({
    //"entryName":entryCsFile
});

TestCSEngine.initEntrySource(mainCsSource);
var hdfData = CSInterpreter.parseHDFString(dataSource);
var result = TestCSEngine.execute(hdfData);

process.stdout.write(result);

var hdfStrResult = HDF.dumpHdf(hdfData);
//process.stdout.write(hdfStrResult);
