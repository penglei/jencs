define(function(require){
    var inherits = require("util").inherits;
    var EventEmitter = require("events").EventEmitter;

    function Backend(conn){

        this._conn = conn;
        this._connected = false;

        this._requests = {};
        this._requests._count = 0;

        conn.on('message', this._dispatchHandler.bind(this));
        conn.on('connect', this._connectedHandler.bind(this));
        conn.on('disconnect', this._disconnectedHandler.bind(this));
        conn.on('error', this._errorExit.bind(this));
    }

    inherits(Backend, EventEmitter);

    Backend.prototype.addRequst = function (id, request) {
        this._requests[id] = request;
        this._requests._count++;
    }
    Backend.prototype.sendMessage = function(message){
        if (!this._connected) return false;
        var payload = typeof message == 'string' ? message : JSON.stringify(message);
        this._conn.send(payload);
    };

    Backend.prototype._connectedHandler = function(){
        this._connected = true;
        console.log("debugger connected >>>");
    };

    Backend.prototype._disconnectedHandler = function(){
        this._connected = false;
        console.log('<<< debugger finished.');
        if (this._requests._count) this._errorExit();
        else this.emit("close");
    };

    Backend.prototype._errorExit = function(){
         this.emit("errorExit");
    };

    Backend.prototype.close = function(){
        this._connected = false;
        this._conn.disconnect();
    };

    /**
     * onWebSocketMessage
     */
    Backend.prototype._dispatchHandler = function(message){
        var messageObject = (typeof message === "string") ? JSON.parse(message) : message;
        if ("id" in messageObject){//confirm request
            console.log(message);
            var request = this._requests[messageObject.id];
            delete this._requests[messageObject.id];
            this._requests._count--;
            if (request.callback){
                request.callback.call(request.that);
            }
        } else {
            var methodName = messageObject.method;
            var params = messageObject.params;

            if (this._dispatcher[methodName]) {
                var parameters = this._dispatcher[methodName].parameters;
                var args = [];
                for(var i = 0; parameters[i]; i++){
                    args.push(messageObject.params[parameters[i]]);
                }
                this._dispatcher[methodName].apply(this._dispatcher, args);
            }
        }
    };

    Backend.prototype.registerCommand = function(dispatcher){
        this._dispatcher = dispatcher;
    };

   return Backend;
});
