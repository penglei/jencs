var AST = require("../parse/ast");
var ClearSilverParser = require("../parse/clearsilver").Parser;
var HDF = require("./hdf");

var Scope = require("./scope");
var Executer = require("./executer").Executer;

//放在这里而不是executer里面，是因为executer里的def_execute会被它们用到，这就会形成循环依赖
require("./block");
require("./statement");
require("./expr");
require("./external");

function Engine(csString){
    this.result = "";
    this._debugMode = false;

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

    //context主要负责内部数据, Executer主要负责执行控制，实现debugger
    var executer = new Executer();
    var context = new Scope.Context();

    if (this._debugMode) executer.enableDebugger();

    context.initHDFData(hdfData || {});
    context.setExternalFilters(this._externFilters);

    var self = this;
    context.setRenderListener(function(snippets){
        self.result += snippets;
    });

    executer.on("end", function(){
        self._onEnd();
    });

    executer.run(this.astInstance, context);
};

Engine.prototype.setEndListener = function(listener){
    if (typeof listener == "function"){
        this._onEnd = listener;
    } else {
        this._onEnd = function(){};
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
