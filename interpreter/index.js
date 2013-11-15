var AST = require("../parse/ast");
var ClearSilverParser = require("../parse/clearsilver").Parser;
var HDF = require("./hdf");

var Scope = require("./scope");
var Executer = require("./executer").Executer;

var DebugService = require("./debugger").DebugService;

//放在这里而不是executer里面，是因为executer里的def_execute会被它们用到，这就会形成循环依赖
require("./block");
require("./statement");
require("./expr");
require("./external");

function Engine(csString){
    this.result = "";
    this._debugMode = false;

    this.executer = new Executer();

    this.csparser = new ClearSilverParser();

    this.csparser.yy.name = this._entryName = "[main]";

    if (typeof csString == 'string') this.initEntrySource(csString);


    this.subAsts = {};

    var self = this;
    this.csparser.lexer.include = function(name){
        self._lexInclude(name);
    };
    this.csparser.yy.getSubAst = function(name){
        return self.subAsts[name];
    };
    this._externFilters = [];

    this.executer.on("end", this._onEnd.bind(this));

    this._sources = {};
}

//include在语法分析阶段就完成要方便得多
Engine.prototype._lexInclude = function(includeName) {
    var self = this;
    var source = this._lexIncludeSource(includeName);
    if (source && !this.subAsts[includeName]){
        //同一个文件只需要解析一次，语法树只允许读，每个地方不需要重新生成
        //TODO 检查循环依赖
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

Engine.prototype._getDebuggerInstance = function(){
    if (!this._debugr) {
        this._debugr = new DebugService(this);
    }
    return this._debugr;
};

Engine.prototype._renderListener = function(snippets){
    this.result += snippets;
};

Engine.prototype._onEnd = function(){
    this._endListener.call(this, this.result);
};

Engine.prototype.initEntrySource = function(csString, name){
    if (name !== undefined) this.csparser.yy.name = this.entryName = name;
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
    if(opts.debug){
        this._debugMode = true;
    }
    return this;
};

Engine.prototype.run = function(hdfData){
    if (typeof hdfData == "string"){
        hdfData = HDF.parse(hdfData);
    }

    this.result = "";
    this._hdf = hdfData;

    var context = new Scope.Context();

    context.initHDFData(hdfData);
    context.setExternalFilters(this._externFilters);
    context.setRenderListener(this._renderListener.bind(this));

    this.executer.run(this.astInstance, context, this._debugMode ? this._getDebuggerInstance() : null);
};

Engine.prototype.setEndListener = function(listener){
    if (typeof listener == "function"){
        this._endListener = listener;
    } else {
        this._endListener = function(){};
    }
};

Engine.prototype.addOutputFilter = function(filter){
    if (typeof filter == "function"){
        this._externFilters.push(filter);
    }
};

Engine.prototype.dumpData = function(){
    return HDF.dumpHdf(this._hdf);
};

exports.Engine = Engine;
exports.CSValue = Scope.CSValue;
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
