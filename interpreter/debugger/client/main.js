define(function(require) {
    var $ = require('jquery');

    var Panel = require("Panel");
    var Backend = require("Backend");
    var DebugModel = require("DebugModel");//接受数据
    var DebugAgent = require("DebugAgent");//发送请求
    //var VariabelPanel = require("VariablePanel");
    var DebugToolbar = require("DebugToolbar");
    var CodeEditor = require("CodeCMEditor");

    var backend = new Backend(io.connect('http://localhost', {
        'reconnect': false
    }));

    //global variable DebugAgent
    this.DebugAgent = new DebugAgent(backend);

    var debugModel = new DebugModel(backend);

    Panel.toolbar = new DebugToolbar();
    Panel.codeEditor = new CodeEditor(debugModel);


    debugModel.on(DebugModel.EventTypes.SessionInit, Panel.setReSources.bind(Panel));//main

    debugModel.on(DebugModel.EventTypes.DebuggerPaused, Panel.debuggerPaused.bind(Panel));
    debugModel.on(DebugModel.EventTypes.DebugFinished, onFinish);

    /*
    debugModel.on(DebugModel.EventTypes.RenderSnippet, function (str) {
        console.log(str);
    });
    */

    function onFinish(exit){
        Panel.codeEditor.unActiveExecutionLine();
        Panel.toolbar.disable();
        if (exit){
            backend.close();
        }
    }

});
