#!/usr/bin/env node

var fs = require('fs');
var path = require('path');

var CSInterpreter = require('../interpreter');

var Engine = CSInterpreter.Engine;

var dataHdfFile;
var entryCsFile;

process.argv.forEach(function (val, index, array) {
    if (/\.cs$/.test(val)){
        entryCsFile = val;
    } else if (/\.hdf$/.test(val)){
        dataHdfFile = val;
    }
});


var _csRoot = path.resolve(__dirname, './cs/');
var csIncludeRoot = path.resolve(_csRoot, 'module/');

var mainCsSource = fs.readFileSync(entryCsFile, "utf8");
var dataSource = fs.readFileSync(dataHdfFile, "utf8");

var TestCSEngine = new Engine();

TestCSEngine.setLexerInclude(function(filename){
    return fs.readFileSync(path.resolve(csIncludeRoot, filename), "utf8");
});
TestCSEngine.initEntrySource(mainCsSource);
TestCSEngine.setEndListener(function(){
    process.stdout.write(this.result);
});

TestCSEngine.run(dataSource);
