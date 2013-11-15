function Session(csengine, socket) {

    this.engine = csengine;
    this.executer = csengine.executer;
    this.socket = socket;

    socket.emit("init");

    socket.on("next", this.forwardCommand.bind(this));
    socket.on("resume", this.resumeCommand.bind(this));
    socket.on("disconnect", this.disconnect.bind(this));
}

Session.prototype.debugBreak = function(astnode){
    var socket = this.socket;
    if (astnode) {
        var info = {
            "type": astnode.type,
            "first_line": astnode.pos.first_line,
            "filename": astnode.pos.name
        };
        socket.emit("Debug.break", info);
    } else {
        socket.emit("runned", {
            "finished": true
        });
        socket.emit("finish");
    }
};

Session.prototype.forwardCommand = function(data) {
    var astnode,
        socket = this.socket,
        executer = this.executer;

    if (data.type == "stepinto") {
        astnode = executer.forward(true);
    } else {
        astnode = executer.forward();
    }
    this.debugBreak(astnode);
};

Session.prototype.resumeCommand = function(){
    var astnode = this.executer.resume();
    this.debugBreak(astnode);
};

Session.prototype.disconnect = function() {
    console.log("socket disconnect");
    //process.exit(); //TODO for test convinient
};

exports.Session = Session;
