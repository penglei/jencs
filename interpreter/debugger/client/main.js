define(function(require) {
    require("CodeMirrorEditor");

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
        if (data.finished) {
            conn.socket.disconnect();
        } else {
            console.log("next:");
            console.log(data);
            seq++;
        }
    });

    conn.on("connect", function() {
        console.log("debugger connect>>>");
    });

    conn.on('disconnect', function() {
        end_closed = true;
        console.log('<< remote execute end');
    });

});
