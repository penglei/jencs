define(function(){
    function DebugAgent(backend){
        this._id = 1;
        this._backend = backend;
        this._closed = false;
        this._backend.on("close", this._backendCloseHandler.bind(this));
    }

    DebugAgent.prototype._backendCloseHandler = function(){
        this._closed = true;
    };

    DebugAgent.prototype.sendMessageToBackend = function(message){
        if (!this._closed) this._backend.sendMessage(message);
        else console.log("remote had closed!");
    };

    DebugAgent.prototype.resume = function(cb){
        var message = {
            "method": "resume"
        };
        this._wrap(message, cb);
        this.sendMessageToBackend(message);
    };

    DebugAgent.prototype.stepOver = function(message, cb){
        var message = {
            "method": "stepOver"
        };
        this._wrap(message, cb);
        this.sendMessageToBackend(message);
    };

    DebugAgent.prototype.stepInto = function(message, cb){
        var message = {
            "method": "stepInto"
        };
        this._wrap(message, cb);
        this.sendMessageToBackend(message);
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

        this._wrap(message);
        this.sendMessageToBackend(message);
    };


    return DebugAgent;
});
