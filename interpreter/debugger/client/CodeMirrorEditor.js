define(function(require) {
    var $ = require('jquery');

    require('cm/codemirror.js');
    require('cm/codemirror.css');
    require('cm/monokai.css');
    //require('cm/velocity.js');

    var code = require("code.cs")

    require(['cm/clearsilver.js'], function(){

        var editor = new CodeMirror($("#code").get(0), {
            //value: code || "",
            cursorBlinkRate: 0, //0 is disabled
            readOnly: true,
            tabMode: "indent",
            theme: "monokai",
            lineNumbers: true,
            indentUnit: 4,
            mode: "text/clearsilver"
        });
        editor.setSize("auto", "500");

        setTimeout(function(){
            editor.setValue(code || "");
        });
    });
});
