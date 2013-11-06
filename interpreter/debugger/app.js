var app = require('http').createServer(handler),
    io = require('socket.io').listen(app),
    fs = require('fs');

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

var readyed = false;
io.sockets.on('connection', function(socket) {
    if (readyed == false){
        readyFun();
        readyed = true;
    }
    socket.emit('news', {
        hello: 'world'
    });

    socket.on('next', function(data) {
        var resultInfo = csengineNext();

        socket.emit("runtonext", resultInfo || {
            linenum: "unknown",
            filename:"unknown"
        });
    });
});

var readyFun, csengineNext;
exports.onNext = function(cb){
    csengineNext = cb;
};

exports.bootstrap = function(ready){
    readyFun = ready;
    app.listen(80);
};

