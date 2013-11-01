var AST = require("../parse/ast");
var ClearSilverParser = require("../parse/clearsilver").Parser;
var HDF = require("./hdf");

var Scope = require("./scope");
var Executer = require("./executer");

//放在这里而不是executer里面，是因为executer里的def_execute会被它们用到，这就会形成循环依赖
require("./block");
require("./statement");
require("./expr");
require("./external");


function Engine(csString){
    this.csparser = new ClearSilverParser();
    if (typeof csString == 'string') this.initEntrySource(csString);

    this.csparser.yy.name = "[main]";

    this.subAsts = {};

    var self = this;
    this.csparser.lexer.include = function(name){
        self._lexInclude(name);
    };
    this.csparser.yy.getSubAst = function(name){
        return self.subAsts[name];
    };
    this._externFilters = [];
}

//include在语法分析阶段就完成要方便得多
Engine.prototype._lexInclude = function(includeName) {
    var self = this;
    var source = this._lexIncludeSource(includeName);
    if (source){
        //source必須每次解析，因為include就是簡單的代碼插入
        var csSubParser = new ClearSilverParser();
        csSubParser.lexer.include = function(name){
            self._lexInclude(name);
        };
        csSubParser.yy.getSubAst = function(name){
            return self.subAsts[name];
        };
        csSubParser.yy.name = includeName;
        var subAsts = csSubParser.parse(source);
        this.subAsts[includeName] = subAsts;
    } else {
        //TODO notice
    }
};

Engine.prototype._renderListener = function(snippets) {
    this._result += snippets;

    //console.log(snippets);
};

Engine.prototype.initEntrySource = function(csString, name){
    if (name !== undefined) this.csparser.yy.name = name;
    this.astInstance = this.csparser.parse(csString);
    Scope.initScopeLayer(this.astInstance);//XXX 虽然会修改ast，但它是没有什么副作用的.但最好还是用一份clone的ast来运行最好
};

//設定一個Include處理器，用於返回include的源碼
Engine.prototype.setLexerInclude = function(cb) {
    this._lexIncludeSource = cb;
};

Engine.prototype.setConfig = function(opts){
    opts = opts || {};

    if (opts.entryName){
        this.csparser.yy.name = opts.entryName;
    }
    if (opts.lexerIncludeFun){
        this._lexIncludeSource = opts.lexerIncludeFun;
    }
    return this;
};

Engine.prototype.execute = function(hdfData, render){
    this._result = "";

    this.context = new Scope.Context();
    this.context.initHDFData(hdfData || {});
    this.context.setExternalFilters(this._externFilters);

    var self = this;
    this.context.setRenderListener(function(){
        self._renderListener.apply(self, Array.prototype.slice.call(arguments, 0));
    });
    Executer.run(this.astInstance, this.context);
    this.context = null;
    return this._result;
};
Engine.prototype.addOutputFilter = function(filter){
    if (typeof filter == "function"){
        this._externFilters.push(filter);
    }
};

exports.Engine = Engine;
exports.CSValue = Scope.CSValue;
exports.parseHDFString = HDF.parse;
exports.HNode = HDF.HNode;
exports.AST = AST;

/**
 * 一個默認的解析器
 */
exports.render = function(csString, hdfString, opts){
    var csEngine = new Engine(csString);
    csEngine.setConfig(opts);

    var hdfData = HDF.parse(hdfString);
    return csEngine.execute(hdfData);
};
