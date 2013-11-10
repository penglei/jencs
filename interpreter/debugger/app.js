var http = require('http');
var SocketIO = require('socket.io');
var fs = require('fs');

var app;

function handler(req, res) {
    fs.readFile(__dirname + '/client/index.html', function(err, data) {
        if (err) {
            res.writeHead(500);
            return res.end('Error loading index.html');
        }

        res.writeHead(200);
        res.end(data);
    });
}

function OnConnection(socket){
    if (readyed == false){
        readyFun();
        readyed = true;
    }

    socket.on('stepover', function(data) {
        if (endFlag){
            socket.emit("end");
        } else {
            var resultInfo = csEngineStepOver();
            socket.emit("next", resultInfo || {
                linenum: "unknown",
                filename:"unknown"
            });
        }
    });
}

var endFlag = false;

function OnExecuterEnd(){
    endFlag = true;
}


var readyed = false;
var readyFun, csEngineStepOver;

exports.onStepOver = function(cb){
    csEngineStepOver = cb;
};

exports.onResume = function(cb){
};

exports.onStepInto = function(cb){
};

exports.onStepOut = function(cb){
};

exports.bootstrap = function(executer, ready){
    readyFun = ready;

    executer.on("end", OnExecuterEnd);

    app = http.createServer(handler),
    io = SocketIO.listen(app),
    io.sockets.on('connection', OnConnection);

    app.listen(10080);
};

