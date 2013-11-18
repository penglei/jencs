define(function(require) {
    var CodeEditor = require("CodeMirrorEditor");
    var SourcePanel = require("SourcePanel");

    var $ = require('jquery');

    var conn = io.connect('http://localhost', {
        'reconnect': false
    });

    var end_closed = false;
    var seq = 0; //每个请求给一个续号(续号需要实现有效期)
    $("#resume").click(function(evt) {
        if (!end_closed) {
            conn.emit("resume", {
                "seq": seq,
                "type": "resume"
            });
        }
    });

    $("#step-over").click(function(evt) {
        if (!end_closed) {
            conn.emit("next", {
                "seq": seq,
                "type": "stepover"
            });
        }
    });

    $("#step-into").click(function(evt) {
        if (!end_closed) {
            conn.emit("next", {
                "seq": seq,
                "type": "stepinto"
            });
        }
    });

    conn.on('Debug.break', function(data) {
        seq++;
        SourcePanel.ActiveSourceLine(data.id, data.first_line);
    });

    conn.on('finished', function(){
        conn.disconnect();
        SourcePanel.UnActiveSourceLine();
    });

    conn.on("connect", function() {
        console.log("debugger connected >>>");
    });

    conn.on('disconnect', function() {
        end_closed = true;
        console.log('<<< deubbger finished');
    });

    conn.on('init', function(data){
        CodeEditor.onDataSources(data);
    });

    conn.on('Render.Snippet', function (str) {
        console.log(str);
    });
});
