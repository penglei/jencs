var http = require('http');
var SocketIO = require('socket.io');
var fs = require('fs');

var io, app;

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
            //socket.emit("end");
            console.log("client has disconnect");
            socket.disconnect(true);
        } else {
            var resultInfo = csEngineStepOver();
            socket.emit("next", resultInfo || {
                linenum: "unknown",
                filename:"unknown"
            });
        }
    });
    socket.on("disconnect", function(){
        console.log("socket disconnect");
    });
}

var endFlag = false;

function OnExecuterEnd(){
    app.close();
    endFlag = true;
    process.nextTick(function(){
        process.exit();
    });
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

    app = http.createServer(handler);
    io = SocketIO.listen(app, {
        //"log": false
    });
    io.sockets.on('connection', OnConnection);

    app.listen(10080);
};

