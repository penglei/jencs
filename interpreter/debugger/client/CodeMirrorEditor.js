define(function(require, exports) {
    require('cm/codemirror.js');
    var $ = require('jquery');

    var SourcePanel = require("SourcePanel");

    var DataSourcesDeffer = $.Deferred();
    function onDataSourcesReady(data) {
        DataSourcesDeffer.resolve(data);
    }

    function CodeMirrorReady(){
        var dtd = $.Deferred();

        require(['cm/clearsilver.js'], function(){
            editor = new CodeMirror($("#code").get(0), {
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
            editor.setSize("auto", editor.getScrollInfo().clientHeight);
            dtd.resolve(editor);
        });
        var timeout = setTimeout(function(){
            dtd.reject();
        }, 30 * 1000);
        return dtd.promise();
    }

    $.when(DataSourcesDeffer, CodeMirrorReady()).done(function(data, CMEditor){
        SourcePanel.initSourcesView(CMEditor, data);
    }).fail(function(){
        alert("数据加载失败");
    });

    exports.onDataSources = onDataSourcesReady;
});
