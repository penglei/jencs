var EventEmitter = require("events").EventEmitter,
    inherits = require("util").inherits,
    path = require("path");

function Session(csengine, socket, config) {
    this.config = config;
    this.engine = csengine;
    this.executer = csengine.executer;
    this.socket = socket;

    this._sourcesDescriptor = {};

    socket.on("disconnect", this.disconnect.bind(this));
    socket.on("message", this._onMessage.bind(this));

    this.engine.onRender(this.emitSnippet.bind(this));
    this._messageDispatcher = new MessageDispatcher(this.executer, this);

    this.emitInitStatus();
}

inherits(Session, EventEmitter);

Session.prototype._onMessage = function(message){
    var messageObject = JSON.parse(message);
    var method = messageObject.method;

    //console.log(messageObject);

    if (method){
        if (this._messageDispatcher[method]){
            this._messageDispatcher[method].call(this._messageDispatcher, messageObject)
        }
    } else {
        var type = messageObject.type;
        if (type == "ready") {
            if (messageObject.resume) {
                if (this.executer.allowPaused()) {
                    //如果第一行是可以执行中断的(debug指令)，直接debugbreak
                    this.debugBreak(messageObject);
                } else {
                    this.executer.resume();
                    this.debugBreak(messageObject);
                }
            } else {
                this.debugBreak(messageObject);
            }
        }
    }
};

Session.prototype.sendEvent = function(method, data){
    var message = {
        method: method,
        params: data || {}
    };
    this.sendMessage(message);
};

Session.prototype.sendMessage = function(message){
  var payload = typeof message == 'string' ? message : JSON.stringify(message);
  this.socket.send(payload);
};

Session.prototype.emitInitStatus = function(){
    var _sources = this.engine.getSources(), resources = [];
    for (var i = 0, l = _sources.length; i < l; i ++) {
        resources.push(this._fileResourcesDescriptor(_sources[i]));
    }

    var data = {
        "resources": resources,
        "resumeEvaluate": this.config.breakFirst ? true : false
    };
    this.sendEvent("SessionInit", data);
};

Session.prototype._fileResourcesDescriptor = function(v){
    if (this._sourcesDescriptor[v.id]) return this._sourcesDescriptor[v.id];
    var url = path.join(v.rootPath, v.name);//url要保证唯一的

    if (v.isEntryDefaultPath){//默认无名的入口文件不做任何处理
    } else {
        url = 'file://' + url;
    }

    var _source = {
        "id": v.id,
        "mimeType":"text/clearsilver",
        "content": v.source,
        "url":url
    };

    this._sourcesDescriptor[v.id] = _source;
    return _source;
};


Session.prototype.emitSnippet = function(str){
    this.socket.emit("RenderSnippet", str);
};

Session.prototype.debugBreak = function(requstParam){
    var socket = this.socket;
    if (this.executer.isRunning()) {
        //说明还在执行
        var params = {
            "watchExpressions": null, //TODO 获得所有需要查看的表达值
            "callFrames":this.executer.getCallFrames()
        };
        this.sendEvent("DebugPaused", params);
    } else {
        var params = {
            "exitOnFinished": this.config.exit ? true : false
        };
        this.sendEvent("DebugFinished", params);
    }

    if (requstParam && requstParam.id){
        this.sendMessage({
            "id": requstParam.id,
            "result": "success"
        });
    }
};

Session.prototype.breakpoint = function(requstParam){
    //TODO
    var id = this.executer.requestBreakpoint(requstParam.rawLocation);
    this.sendMessage({
        "id": requstParam.id,
        "result": {
            "breakpointId": 1,
            "locations": requstParam.rawLocation
        }
    });
};

Session.prototype.disconnect = function() {
    console.log("socket disconnected.");
    //process.exit();
};


function MessageDispatcher(executer, session){
    this._session = session;
    this._executer = executer;
}

MessageDispatcher.prototype = {
    "stepInto": function(msg){
        this._executer.forward(true);
        this._session.debugBreak(msg);
    },
    "stepOver": function(msg){
        this._executer.forward();
        this._session.debugBreak(msg);
    },
    "resume": function(msg){
        this._executer.resume();
        this._session.debugBreak(msg);
    },
    "stepOut": function (msg) {
        this._executer.resume(2);
        this._session.debugBreak(msg);
    },
    "toggleBreakpointsActive": function (msg) {
        this._executer.setBreakpointsActive(msg.active);
    },
    "setBreakpointBySourceId": function(msg) {
        this._session.breakpoint(msg);
    }
};

exports.Session = Session;
