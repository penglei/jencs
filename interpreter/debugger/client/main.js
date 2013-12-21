define(function(require) {

    //global constroller
    CSInspector = {
        useLowerCaseMenuTitles:function(){}
    };

    var Backend = require("Backend");
    var DebugModel = require("DebugModel");//接受数据
    var DebugAgent = require("DebugAgent");//发送请求
    var Workspace = require("Workspace").Workspace;

    var BreakpointManager = require("BreakpointManager");
    var NetworkUISourceCodeProvider = require("NetworkUISourceCodeProvider");
    var SimpleWorkspaceProvider = require("SimpleWorkspaceProvider");
    var Settings = require("Settings").Settings;

    var InspectorView = require("InspectorView");
    var ScriptsPanel = require("ScriptsPanel");
    var ConsolePanel = require("ConsolePanel");

    var backend = new Backend(io.connect('http://localhost', {
        'reconnect': false
    }));

backend.on("connected", function(){
    CSInspector.settings = new Settings();
    CSInspector.debugAgent = new DebugAgent(backend);
    CSInspector.debugModel = new DebugModel(backend);
    CSInspector.workspace = new Workspace();

    CSInspector.breakpointManager = new BreakpointManager(CSInspector.settings.breakpoints, CSInspector.debugModel, CSInspector.workspace);

    CSInspector.debugModel.on(DebugModel.Events.DebugPaused, function(){
        CSInspector.inspectorView.showPanel("scripts");
    });

    CSInspector.debugModel.on(DebugModel.Events.DebugFinished, function(exit){
        if (exit) backend.close();
    });

    CSInspector.inspectedPageURL = "";//mainFramePayload.frame.url;
    //初始化文件传输. file://也是network类型的(这里只有Network类型)
    new NetworkUISourceCodeProvider(
            new SimpleWorkspaceProvider(
                CSInspector.workspace,
                Workspace.ProjectTypes.Network),
            CSInspector.workspace);



    var inspectorView = new InspectorView();
    CSInspector.inspectorView = inspectorView;
    inspectorView.show(document.getElementById("main"));

    var scriptsPanel = new ScriptsPanel();
    inspectorView.addPanel(scriptsPanel);
    inspectorView.showPanel("scripts");

    window.addEventListener("resize", inspectorView.doResize.bind(inspectorView), true);

});

});

//resourceReady -> mainFramePayload -> ready
