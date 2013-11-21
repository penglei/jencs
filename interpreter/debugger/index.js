var http = require('http'),
    path = require('path'),
    io = require('socket.io'),
    EventEmitter = require('events').EventEmitter,
    express = require('express'),
    Session = require("./session").Session,
    inherits = require('util').inherits,
    extend = require('util')._extend;

var WEBROOT = path.join(__dirname, './client');

function indexAction(req, res){
    res.sendfile(path.join(WEBROOT, 'index.html'));
}

function handleServerListening() {
  this.emit('listening');
}

function handleServerError(err) {
  this.emit('error', err);
}

/**
 * DebugService
 */
function DebugService(csEngine, config) {
    this.config = config || {};
    this._engine = csEngine;
    this._httpServer = null;
    this._wsio = null;

    this._init();
}

inherits(DebugService, EventEmitter);

DebugService.prototype._init = function() {

    var app = express();
    var httpServer = this._httpServer = http.createServer(app);

    app.get('/', indexAction.bind(this));
    app.use(express.static(WEBROOT));

    var wsServer = this.wsServer = io.listen(httpServer);
    wsServer.configure(function() {
        wsServer.set('transports', ['websocket']);
        wsServer.set('log level', 1);
    });
    wsServer.sockets.on('connection', this._createSession.bind(this));

    //startup server
    httpServer.on('listening', handleServerListening.bind(this));
    httpServer.on('error', handleServerError.bind(this));
    httpServer.listen(10080);
};

DebugService.prototype._createSession = function(socket){
    //每次有一个客户端连接都需要新建一个session
    //exectuer的事件需要发到每一个session上面去
    var newClient =  new Session(this._engine, socket, this.config);
};


DebugService.prototype._close = function(){
    if (this.wsServer) {
        this.wsServer.close();
        this.emit('close');
    }
};

exports.DebugService = DebugService;

