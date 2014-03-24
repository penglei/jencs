var AST = require("../parse/ast");
var ClearSilverParser = require("../parse/clearsilver").Parser;
var HDF = require("./hdf");

var Scope = require("./scope");
var Executer = require("./executer").Executer;

var DebugService = require("./debugger").DebugService;

require("./block");
require("./statement");
require("./expr");
require("./external");

function Empty(){}

function insertArray(arr, item){
    for(var i = 0; arr[i]; i++){
        if (arr[i] == item) return;
    }
    arr.push(item);
}

function parseError(str, hash, engine){
    var filepath = this.yy.fileid == 1 ? this.yy.filename : engine._includeParentBase + '/' + this.yy.filename;
    str = 'Parse error on line : ' + (hash.line + 1) + ', in file --> ' + filepath + '\n' + this.lexer.showPosition();
    if (hash.expected) {
         str += '\nExpecting ' + hash.expected.join(', ') + ', got \'' + hash.token + '\'';
    } else {//XXX only lex error go here?
         str += '\nUnrecognized text.\n'
    }
    throw new Error(str);
}


function Engine(csString){
    this.result = "";
    this._debugMode = false;
    this._debugConfig = {};

    this.executer = new Executer(this);

    this.csparser = new ClearSilverParser();

    this.csparser.yy.filename = this._entryPathname = "[main]";
    this.csparser.yy.parseError = function(){
        var args = Array.prototype.slice.call(arguments);
        args.push(self);
        parseError.apply(this, args);
    };

    this._entryPathIsDefault = true;
    this._includeParentBase = "$(root)";

    if (typeof csString == 'string') this.initEntrySource(csString);


    this.subAsts = {};

    var self = this;
    //安装词法分析器的include回调
    this.csparser.lexer.include = function(name){
        self._lexInclude(name);
    };
    //安装语法分析解析对include的解析
    this.csparser.yy.getSubAst = function(name){
        return self.subAsts[name];
    };
    this._externFilters = [];

    this.executer.on("end", this._onEnd.bind(this));

    this._sources = [];

    this._onRenderListeners = [];
}

//include在语法分析阶段就完成要方便得多
Engine.prototype._lexInclude = function(includeName) {
    var self = this;
    var source = this._lexIncludeSource(includeName);
    if (source && !this.subAsts[includeName]){
        var fileid = this._saveSource(includeName, source);
        //同一个文件只需要解析一次，语法树只允许读，每个地方不需要重新生成
        //TODO 检查循环依赖
        var csSubParser = new ClearSilverParser();
        csSubParser.yy.parseError = function(){
            var args = Array.prototype.slice.call(arguments);
            args.push(self);
            parseError.apply(this, args);
        };
        csSubParser.lexer.include = function(name){
            self._lexInclude(name);
        };
        csSubParser.yy.getSubAst = function(name){
            return self.subAsts[name];
        };
        //给yy上面安装属性必须在parse调用前
        csSubParser.yy.filename = includeName;
        csSubParser.yy.fileid = fileid;

        var subAst = csSubParser.parse(source);
        this.subAsts[includeName] = subAst;
    } else {
        //TODO notice
    }
};

Engine.prototype._getDebuggerInstance = function(){
    if (!this._debugr) {
        this._debugr = new DebugService(this, this._debugConfig);
    }
    return this._debugr;
};

Engine.prototype._renderListener = function(snippet){
    for(var i = 0; this._onRenderListeners[i]; i++){
        this._onRenderListeners[i](snippet);
    }
    this.result += snippet;
};

Engine.prototype._onEnd = function(){
    if (this._endListener) this._endListener.call(this, this.result);
};

Engine.prototype._saveSource = function(name, source){
    var id = this._sources.length + 1;
    var sourceInfo = {
        "isEntryDefaultPath": false,
        "rootPath": "",
        "name": name,
        "source": source,
        "id": id
    };

    if (id != 1) {
        //说明这是子模板
        sourceInfo.rootPath = this._includeParentBase;
    } else {
        if (this._entryPathIsDefault){
            sourceInfo.isEntryDefaultPath = true;
        }
    }

    this._sources.push(sourceInfo);
    return id;
};

Engine.prototype.onRender = function(cb){
    if (typeof cb == "function") insertArray(this._onRenderListeners, cb);
};

Engine.prototype.initEntrySource = function(csString, pathname){
    if (pathname !== undefined) {
        this._entryPathIsDefault = false;
        this.csparser.yy.filename = this._entryPathname = pathname + " [main]";
    }
    var fileid = this._saveSource(this._entryPathname, csString);

    this.csparser.yy.fileid = fileid;
    this.astInstance = this.csparser.parse(csString);
    Scope.initScopeLayer(this.astInstance);//XXX 虽然会修改ast，但它是没有什么副作用的.但最好还是用一份clone的ast来运行最好
};

Engine.prototype.getSources = function(name){
    if (name) {
        for(var i = 0; i < this._sources.length; i++){
            if (this._sources[i].name == name) {
                return [this._sources[i]];
            }
        }
    }
    return this._sources;
};

Engine.prototype.getSourceObjectById = function (id) {
    var astTree, source;
    for(var i = 0; i < this._sources.length; i++){
        if (this._sources[i].id == id) {
            source = this._sources[i];
            break;
        }
    }

    if (id == 1) astTree = this.astInstance;
    else astTree = this.subAsts[source.name];

    return {
        "astTree": astTree,
        "source": source
    }
};

//設定一個Include處理器，用於返回include的源碼
Engine.prototype.setLexerInclude = function(cb) {
    this._lexIncludeSource = cb;
};

Engine.prototype.setConfig = function(opts){
    opts = opts || {};

    if (opts.debug) {
        this._debugMode = true;
    }

    if (opts.debugBreakFirst){
        this._debugConfig.breakFirst = true;
    }

    this._debugConfig.port = opts.port;

    if (opts.includeBase){
        this._includeParentBase = opts.includeBase;
    }

    return this;
};

Engine.prototype.run = function(hdfData){
    if (typeof hdfData == "string"){
        hdfData = HDF.parse(hdfData);
    } else {
        hdfData = {};
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
        insertArray(this._externFilters, filter);
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
