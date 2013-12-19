define(function(){
    function DebugAgent(backend){
        this._id = 1;
        this._backend = backend;
        this._closed = false;
        this._backend.on("close", this._backendCloseHandler.bind(this));

        this._backend.replyArgs["setBreakpointBySourceId"] = ["breakpointId", "locations"];
        this._backend.replyArgs["evaluate"] = ["result", "wasThrown"];
        this._backend.replyArgs["getObjectProperties"] = ["result", "wasThrown"];
    }

    DebugAgent.prototype = {
        getScriptSource: function(scriptId, cb){
            //TODO
        },
        setBreakpointsActive: function(active){
            var message = {
                "method": "toggleBreakpointsActive",
                "active": active
            };
            this.sendMessageToBackend(message);
        },

        setBreakpointByUrl: function(url, lineNumber, columnNumber, cb){
        },

        setBreakpointBySourceId: function(rawLocation, condition, cb){
            var message = {
                "method": "setBreakpointBySourceId",
                "rawLocation": rawLocation
            };
            this.sendMessageToBackend(message, cb);
        },
        removeBreakpoint: function(breakpointId){
            var message = {
                "method":"removeBreakpoint",
                "breakpointId": breakpointId
            };
            this.sendMessageToBackend(message);
        },
        evaluate: function() {
            if (CSInspector.debugModel.selectedCallFrame()){
                CSInspector.debugModel.evaluateOnSelectedCallFrame.apply(CSInspector.debugModel, Array.prototype.slice.call(arguments, 0));
            } else {
                //console.error("can't evalute expresss, because of no callFrame selected");
            }
        },
        evaluateOnCallFrame: function(
                                callFrameId,
                                code,
                                objectGroup,
                                includeCommandLineAPI,
                                doNotPauseOnExceptionsAndMuteConsole,
                                returnByValue,
                                generatePreview,
                                callback)
        {
            var message = {
                "method":"evaluate",
                "callFrameId": callFrameId,
                "expression": code
            };
            this.sendMessageToBackend(message, callback);
        },
        getProperties: function(objectId, ownProperties, accessorPropertiesOnly, cb){debugger
            var message = {
                "method": "getObjectProperties",
                "objectId": objectId
            };

            this.sendMessageToBackend(message, cb);
        }

    };

    DebugAgent.prototype._backendCloseHandler = function(){
        this._closed = true;
    };

    DebugAgent.prototype.sendMessageToBackend = function(message, callback){
        this._wrap(message, callback);
        if (!this._closed) this._backend.sendMessage(message);
        else console.notice("remote had closed!");
    };

    DebugAgent.prototype.resume = function(cb){
        var message = {
            "method": "resume"
        };
        this.sendMessageToBackend(message, cb);
    };

    DebugAgent.prototype.stepOut = function(cb){
        var message = {
            "method": "stepOut"
        };
        this.sendMessageToBackend(message, cb);
    };

    DebugAgent.prototype.stepOver = function(message, cb){
        var message = {
            "method": "stepOver"
        };
        this.sendMessageToBackend(message, cb);
    };

    DebugAgent.prototype.stepInto = function(message, cb){
        var message = {
            "method": "stepInto"
        };
        this.sendMessageToBackend(message, cb);
    };

    DebugAgent.prototype._wrap = function(message, cb){
        if (this._id > 100000000) this._id = 1;
        var id = message["id"] = this._id;
        var request = {
            "id": id,
            "callback": cb
        };
        this._id++;
        this._backend.addRequst(id, request);
        return request;
    };

    DebugAgent.prototype.sessionReady = function(resume){
        var message = {
            "type": "ready",
            "resume": resume ? true : false
        };

        this.sendMessageToBackend(message);
    };


    return DebugAgent;
});
