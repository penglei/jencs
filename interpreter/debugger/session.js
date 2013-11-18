function Session(csengine, socket) {

    this.engine = csengine;
    this.executer = csengine.executer;
    this.socket = socket;

    socket.on("next", this.forwardCommand.bind(this));
    socket.on("resume", this.resumeCommand.bind(this));
    socket.on("disconnect", this.disconnect.bind(this));

    this.engine.onRender(this.emitSnippet.bind(this));

    this.emitInitStatus();
}

Session.prototype.emitInitStatus = function(){

    var data = {
        "sources": this.engine.getSource(),
    };
    if (this.executer.cmdHead){
        var astnode = this.executer.cmdHead.node;
        data["DebugBreak"] = {
            "type": astnode.type,
            "first_line": astnode.pos.first_line,
            "filename": astnode.pos.name,
            "id":astnode.pos.fileid
        };
    } else {
        data["finished"] = true;
    }
    //csengine.getCurrentStatus();//isrunning? Debug.break?
    this.socket.emit("init", data);
};

Session.prototype.emitSnippet = function(str){
    this.socket.emit("Render.Snippet", str);
};

Session.prototype.debugBreak = function(astnode){
    var socket = this.socket;
    if (astnode) {
        var info = {
            "type": astnode.type,
            "first_line": astnode.pos.first_line,
            "filename": astnode.pos.name,
            "id": astnode.pos.fileid
        };
        socket.emit("Debug.break", info);
    } else {
        socket.emit("finished");
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
    process.exit(); //TODO for test convinient
};

exports.Session = Session;
