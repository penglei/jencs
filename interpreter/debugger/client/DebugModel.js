define(function(require){
    var Util = require("util");
    var EventEmitter = require("events").EventEmitter;

    function DebugModel(backend){
        this._backend = backend;
        this._listeners = {};
        backend.registerCommand(new Dispatcher(this));

        this._backend.on("errorExit", this._connectErrorHandler.bind(this));
    }

    Util.inherits(DebugModel, EventEmitter);

    DebugModel.prototype._connectErrorHandler = function(){
        this.emit(DebugModel.EventTypes.DebugFinished, true);
        console.error("remote debugger closed unexpectedly.");
    };

    DebugModel.EventTypes = {
        SessionInit: "SessionInit",
        RenderSnippet: "RenderSnippet",
        DebuggerPaused: "DebuggerPaused",
        DebugFinished: "DebugFinished",

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
        "SessionInit": function(sources, resumeEvaluate){
            this._model.emit(DebugModel.EventTypes.SessionInit, sources);
            DebugAgent.sessionReady(resumeEvaluate);
        },
        "DebugPaused": function(evaluateLine, scopeChain, watchExpressions){
            this._model.emit(DebugModel.EventTypes.DebuggerPaused, evaluateLine, scopeChain, watchExpressions);
        },
        "DebugFinished": function(exit){
            this._model.emit(DebugModel.EventTypes.DebugFinished, exit);
        }
    };

    Dispatcher.prototype.SessionInit.parameters = ["sources", "resumeEvaluate"];
    Dispatcher.prototype.DebugPaused.parameters = ["executeLine", "scopeChain", "watchExpressions"];
    Dispatcher.prototype.DebugFinished.parameters = ["exitOnFinished"];

    return DebugModel;
});
