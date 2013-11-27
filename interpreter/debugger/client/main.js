define(function(require) {

    //global constroller
    this.CSInspector = {};

    var Backend = require("Backend");
    var DebugModel = require("DebugModel");//接受数据
    var DebugAgent = require("DebugAgent");//发送请求
    var Workspace = require("Workspace").Workspace;

    var BreakpointsManager = require("BreakpointsManager");
    var NetworkUISourceCodeProvider = require("NetworkUISourceCodeProvider");
    var SimpleWorkspaceProvider = require("SimpleWorkspaceProvider");
    var Settings = require("Settings").Settings;

    var MainView = require("MainView");
    var ScriptsPanel = require("ScriptsPanel");
    var ConsolePanel = require("ConsolePanel");

    var backend = new Backend(io.connect('http://localhost', {
        'reconnect': false
    }));

    CSInspector.settings = new Settings();
    CSInspector.debugAgent = new DebugAgent(backend);
    CSInspector.debugModel = new DebugModel(backend);
    CSInspector.workspace = new Workspace();
    CSInspector.breakpointsManager = new BreakpointsManager();

    CSInspector.debugModel.on(DebugModel.Events.DebugPaused, onPausedScripts);
    CSInspector.debugModel.on(DebugModel.Events.DebugFinished, onFinish);

    function onPausedScripts(){
        inspectorView.showPanel("scripts");
    }

    function onFinish(exit){
        if (exit){
            backend.close();
        }
    }

    //初始化文件传输. file://也是network类型的(这里只有Network类型)
    new NetworkUISourceCodeProvider(new SimpleWorkspaceProvider(CSInspector.workspace, Workspace.ProjectTypes.Network), CSInspector.workspace);


    var inspectorView = new MainView();
    CSInspector.inspectorView = inspectorView;
    inspectorView.show(document.getElementById("main"));

    var scriptsPanel = new ScriptsPanel();
    inspectorView.addPanel(scriptsPanel);

    window.addEventListener("resize", inspectorView.doResize.bind(inspectorView), true);

    inspectorView.showPanel("scripts");
});
