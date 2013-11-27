define(function(require){
    var EventEmitter = require("events").EventEmitter;

    function DebugModel(backend){
        EventEmitter.call(this);
        this._backend = backend;
        this._listeners = {};
        backend.registerCommand(new Dispatcher(this));

        this._backend.on("errorExit", this._connectErrorHandler.bind(this));
    }

    DebugModel.prototype = {
        __proto__: EventEmitter.prototype
    };

    DebugModel.prototype._connectErrorHandler = function(){
        this.emit(DebugModel.Events.DebugFinished, true);
        console.error("remote debugger closed unexpectedly.");
    };

    DebugModel.Events = {
        SessionInit: "sessionInit",
        RenderSnippet: "renderSnippet",
        DebuggerPaused: "debuggerPaused",
        DebugFinished: "debugFinished",
        FileSourcesAdded: "SourceCodeAdded"

        /*
        DebuggerWasEnabled: "DebuggerWasEnabled",
        DebuggerWasDisabled: "DebuggerWasDisabled",
        DebuggerResumed: "DebuggerResumed",
        ParsedScriptSource: "ParsedScriptSource",
        FailedToParseScriptSource: "FailedToParseScriptSource",
        BreakpointResolved: "BreakpointResolved",
        CallFrameSelected: "CallFrameSelected",
        ConsoleCommandEvaluatedInSelectedCallFrame: "ConsoleCommandEvaluatedInSelectedCallFrame",
        BreakpointsActiveStateChanged: "BreakpointsActiveStateChanged"
        */
    };

    function Dispatcher(model){
        this._model = model;
    }

    Dispatcher.prototype = {
        "SessionInit": function(resources, resumeEvaluate){
            this._model.dispatchEventToListeners(DebugModel.Events.FileSourcesAdded, resources);
            CSInspector.debugAgent.sessionReady(resumeEvaluate);
        },
        "DebugPaused": function(evaluateLine, callFrames, watchExpressions){
            var data = {
                evaluateLine: evaluateLine,
                callFrames: callFrames,
                watchExpressions: watchExpressions
            };
            //this._model.emit(DebugModel.Events.DebuggerPaused, evaluateLine, callFrames, watchExpressions);
            this._model.dispatchEventToListeners(DebugModel.Events.DebuggerPaused, data);
        },
        "DebugFinished": function(exit){
            this._model.emit(DebugModel.Events.DebugFinished, exit);
        }
    };

    Dispatcher.prototype.SessionInit.parameters = ["resources", "resumeEvaluate"];
    Dispatcher.prototype.DebugPaused.parameters = ["executeLine", "scopeChain", "watchExpressions"];
    Dispatcher.prototype.DebugFinished.parameters = ["exitOnFinished"];

    return DebugModel;
});
