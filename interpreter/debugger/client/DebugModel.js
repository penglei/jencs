define(function(require){
    var EventEmitter = require("events").EventEmitter;
    var DebuggerPausedDetails = require("DebuggerPausedDetails");
    var Script = require("Script");
    var RemoteObject = require("RemoteObject").RemoteObject;

    /**
     * @constructor
     * @implements {RawLocation}
     * @param {string} scriptId
     * @param {number} lineNumber
     * @param {number} columnNumber
     */
    function Location(scriptId, lineNumber, columnNumber)
    {
        this.scriptId = scriptId;
        this.lineNumber = lineNumber;
        this.columnNumber = columnNumber;
    }

    function DebuggerModel(backend){
        EventEmitter.call(this);
        this._backend = backend;
        this._listeners = {};
        backend.registerCommand(new Dispatcher(this));
        /**
         * @type {Object.<string, Script>}
         */
        this._scripts = {};
        /** @type {!Object.<!string, !Array.<!Script>>} */
        this._scriptsBySourceURL = {};

        this._debuggerPausedDetails = null;
        this._breakpointsActive = true;

        this._backend.on("errorExit", this._connectErrorHandler.bind(this));
    }

    DebuggerModel.prototype = {

        /**
         * @return {boolean}
         */
        debuggerEnabled: function()
        {
            return !!this._debuggerEnabled;
        },

        enableDebugger: function()
        {
            if (this._debuggerEnabled)
                return;

            function callback(error, result)
            {
                this._canSetScriptSource = result;
            }
            DebuggerAgent.canSetScriptSource(callback.bind(this));
            DebuggerAgent.enable(this._debuggerWasEnabled.bind(this));
        },

        disableDebugger: function()
        {
            if (!this._debuggerEnabled)
                return;

            DebuggerAgent.disable(this._debuggerWasDisabled.bind(this));
        },

        /**
         * @return {boolean}
         */
        canSetScriptSource: function()
        {
            return this._canSetScriptSource;
        },

        _debuggerWasEnabled: function()
        {
            this._debuggerEnabled = true;
            this._pauseOnExceptionStateChanged();
            this.dispatchEventToListeners(DebuggerModel.Events.DebuggerWasEnabled);
        },

        _pauseOnExceptionStateChanged: function()
        {
            DebuggerAgent.setPauseOnExceptions(CSInspector.settings.pauseOnExceptionStateString.get());
        },

        _debuggerWasDisabled: function()
        {
            this._debuggerEnabled = false;
            this.dispatchEventToListeners(DebuggerModel.Events.DebuggerWasDisabled);
        },

        /**
         * @param {DebuggerModel.Location} rawLocation
         */
        continueToLocation: function(rawLocation)
        {
            DebuggerAgent.continueToLocation(rawLocation);
        },

        /**
         * @param {DebuggerModel.Location} rawLocation
         */
        stepIntoSelection: function(rawLocation)
        {
            /**
             * @param {DebuggerModel.Location} requestedLocation
             * @param {?string} error
             */
            function callback(requestedLocation, error)
            {
               if (error)
                   return;
               this._pendingStepIntoLocation = requestedLocation;
            };
            DebuggerAgent.continueToLocation(rawLocation, true, callback.bind(this, rawLocation));
        },

        /**
         * @param {DebuggerModel.Location} rawLocation
         * @param {string} condition
         * @param {function(?DebuggerAgent.BreakpointId, Array.<DebuggerModel.Location>):void=} callback
         */
        setBreakpointByScriptLocation: function(rawLocation, condition, callback)
        {
            var script = this.scriptForId(rawLocation.scriptId);
            if (script.sourceURL)
                this.setBreakpointByURL(script.sourceURL, rawLocation.lineNumber, rawLocation.columnNumber, condition, callback);
            else
                this.setBreakpointBySourceId(rawLocation, condition, callback);
        },

        /**
         * @param {string} url
         * @param {number} lineNumber
         * @param {number=} columnNumber
         * @param {string=} condition
         * @param {function(?DebuggerAgent.BreakpointId, Array.<DebuggerModel.Location>)=} callback
         */
        setBreakpointByURL: function(url, lineNumber, columnNumber, condition, callback)
        {
            // Adjust column if needed.
            var minColumnNumber = 0;
            var scripts = this._scriptsBySourceURL[url] || [];
            for (var i = 0, l = scripts.length; i < l; ++i) {
                var script = scripts[i];
                if (lineNumber === script.lineOffset)
                    minColumnNumber = minColumnNumber ? Math.min(minColumnNumber, script.columnOffset) : script.columnOffset;
            }
            columnNumber = Math.max(columnNumber, minColumnNumber);

            /**
             * @this {DebuggerModel}
             * @param {?Protocol.Error} error
             * @param {DebuggerAgent.BreakpointId} breakpointId
             * @param {Array.<DebuggerAgent.Location>} locations
             */
            function didSetBreakpoint(error, breakpointId, locations)
            {
                if (callback) {
                    var rawLocations = /** @type {Array.<DebuggerModel.Location>} */ (locations);
                    callback(error ? null : breakpointId, rawLocations);
                }
            }
            //CSInspector.debugAgent.setBreakpointByUrl(url, lineNumber, columnNumber, didSetBreakpoint.bind(this));
            var rawLocations = new Location(script.scriptId, lineNumber, columnNumber);
            CSInspector.debugAgent.setBreakpointBySourceId(rawLocations, "", didSetBreakpoint.bind(this));
        },

        /**
         * @param {DebuggerModel.Location} rawLocation
         * @param {string} condition
         * @param {function(?DebuggerAgent.BreakpointId, Array.<DebuggerModel.Location>)=} callback
         */
        setBreakpointBySourceId: function(rawLocation, condition, callback)
        {
            /**
             * @this {DebuggerModel}
             * @param {?Protocol.Error} error
             * @param {DebuggerAgent.BreakpointId} breakpointId
             * @param {DebuggerAgent.Location} actualLocation
             */
            function didSetBreakpoint(error, breakpointId, actualLocation)
            {
                if (callback) {
                    var rawLocation = /** @type {DebuggerModel.Location} */ (actualLocation);
                    callback(error ? null : breakpointId, [rawLocation]);
                }
            }
            DebuggerAgent.setBreakpoint(rawLocation, condition, didSetBreakpoint.bind(this));
        },

        /**
         * @param {DebuggerAgent.BreakpointId} breakpointId
         * @param {function(?Protocol.Error)=} callback
         */
        removeBreakpoint: function(breakpointId, callback)
        {
            CSInspector.debugAgent.removeBreakpoint(breakpointId, callback);
        },

        /**
         * @param {DebuggerAgent.BreakpointId} breakpointId
         * @param {DebuggerAgent.Location} location
         */
        _breakpointResolved: function(breakpointId, location)
        {
            this.dispatchEventToListeners(DebuggerModel.Events.BreakpointResolved, {breakpointId: breakpointId, location: location});
        },

        _globalObjectCleared: function()
        {
            this._setDebuggerPausedDetails(null);
            this._reset();
            this.dispatchEventToListeners(DebuggerModel.Events.GlobalObjectCleared);
        },

        _reset: function()
        {
            this._scripts = {};
            this._scriptsBySourceURL = {};
        },

        /**
         * @return {Object.<string, Script>}
         */
        get scripts()
        {
            return this._scripts;
        },

        /**
         * @param {DebuggerAgent.ScriptId} scriptId
         * @return {Script}
         */
        scriptForId: function(scriptId)
        {
            return this._scripts[scriptId] || null;
        },

        /**
         * @return {!Array.<!Script>}
         */
        scriptsForSourceURL: function(sourceURL)
        {
            if (!sourceURL)
                return [];
            return this._scriptsBySourceURL[sourceURL] || [];
        },

        /**
         * @param {DebuggerAgent.ScriptId} scriptId
         * @param {string} newSource
         * @param {function(?Protocol.Error, DebuggerAgent.SetScriptSourceError=)} callback
         */
        setScriptSource: function(scriptId, newSource, callback)
        {
            this._scripts[scriptId].editSource(newSource, this._didEditScriptSource.bind(this, scriptId, newSource, callback));
        },

        /**
         * @return {Array.<DebuggerAgent.CallFrame>}
         */
        get callFrames()
        {
            return this._debuggerPausedDetails ? this._debuggerPausedDetails.callFrames : null;
        },

        /**
         * @return {?DebuggerPausedDetails}
         */
        debuggerPausedDetails: function()
        {
            return this._debuggerPausedDetails;
        },

        /**
         * @param {Array.<CallFrame>} callFrames
         * @param {string} reason
         * @param {Object|undefined} auxData
         * @param {Array.<string>} breakpointIds
         */
        _pausedScript: function(callFrames, reason, auxData, breakpointIds)
        {
            if (this._pendingStepIntoLocation) {
                var requestedLocation = this._pendingStepIntoLocation;
                delete this._pendingStepIntoLocation;

                if (callFrames.length > 0) {
                    var topLocation = callFrames[0].location;
                    if (topLocation.lineNumber == requestedLocation.lineNumber && topLocation.columnNumber == requestedLocation.columnNumber && topLocation.scriptId == requestedLocation.scriptId) {
                        DebuggerAgent.stepInto();
                        return;
                    }
                }
            }

            this._setDebuggerPausedDetails(new DebuggerPausedDetails(this, callFrames, reason, auxData, breakpointIds));
        },

        /**
         * @param {?DebuggerPausedDetails} debuggerPausedDetails
         */
        _setDebuggerPausedDetails: function(debuggerPausedDetails)
        {
            if (this._debuggerPausedDetails)
                this._debuggerPausedDetails.dispose();
            this._debuggerPausedDetails = debuggerPausedDetails;
            if (this._debuggerPausedDetails)
                this.dispatchEventToListeners(DebuggerModel.Events.DebuggerPaused, this._debuggerPausedDetails);
            if (debuggerPausedDetails) {
                this.setSelectedCallFrame(debuggerPausedDetails.callFrames[0]);
                //DebuggerAgent.setOverlayMessage(UIString("Paused in debugger"));
            } else {
                this.setSelectedCallFrame(null);
                //DebuggerAgent.setOverlayMessage();
            }
        },

        /**
         * @param {DebuggerAgent.ScriptId} scriptId
         * @param {string} sourceURL
         * @param {number} startLine
         * @param {number} startColumn
         * @param {number} endLine
         * @param {number} endColumn
         * @param {boolean} isContentScript
         * @param {string=} sourceMapURL
         * @param {boolean=} hasSourceURL
         */
        _parsedScriptSource: function(scriptId, sourceURL, startLine, startColumn, endLine, endColumn, isContentScript, sourceMapURL, hasSourceURL)
        {
            var script = new Script(scriptId, sourceURL, startLine, startColumn, endLine, endColumn, isContentScript, sourceMapURL, hasSourceURL);
            this._registerScript(script);
            this.dispatchEventToListeners(DebuggerModel.Events.ParsedScriptSource, script);
        },

        /**
         * all source load when startup
         */
        _parseScriptSources: function(scriptResources){
            for(var i = 0, l = scriptResources.length; i < l; i++) {
                var fileResourceItem = scriptResources[i];

                var script = new Script(fileResourceItem.id, fileResourceItem.url, 0,0,0,0, true);
                script.setSource(fileResourceItem.content);
                this._registerScript(script);
                this.dispatchEventToListeners(DebuggerModel.Events.ParsedScriptSource, script);
            }
        },

        /**
         * @param {Script} script
         */
        _registerScript: function(script)
        {
            this._scripts[script.scriptId] = script;
            if (script.isAnonymousScript())
                return;

            var scripts = this._scriptsBySourceURL[script.sourceURL];
            if (!scripts) {
                scripts = [];
                this._scriptsBySourceURL[script.sourceURL] = scripts;
            }
            scripts.push(script);
        },

        /**
         * @param {Script} script
         * @param {number} lineNumber
         * @param {number} columnNumber
         * @return {DebuggerModel.Location}
         */
        createRawLocation: function(script, lineNumber, columnNumber)
        {
            if (script.sourceURL)
                return this.createRawLocationByURL(script.sourceURL, lineNumber, columnNumber)
            return new Location(script.scriptId, lineNumber, columnNumber);
        },

        /**
         * @param {string} sourceURL
         * @param {number} lineNumber
         * @param {number} columnNumber
         * @return {DebuggerModel.Location}
         */
        createRawLocationByURL: function(sourceURL, lineNumber, columnNumber)
        {
            var closestScript = null;
            var scripts = this._scriptsBySourceURL[sourceURL] || [];
            for (var i = 0, l = scripts.length; i < l; ++i) {
                var script = scripts[i];
                if (!closestScript)
                    closestScript = script;
                if (script.lineOffset > lineNumber || (script.lineOffset === lineNumber && script.columnOffset > columnNumber))
                    continue;
                if (script.endLine < lineNumber || (script.endLine === lineNumber && script.endColumn <= columnNumber))
                    continue;
                closestScript = script;
                break;
            }
            return closestScript ? new Location(closestScript.scriptId, lineNumber, columnNumber) : null;
        },

        /**
         * @return {boolean}
         */
        isPaused: function()
        {
            return !!this.debuggerPausedDetails();
        },

        /**
         * @param {?CallFrame} callFrame
         */
        setSelectedCallFrame: function(callFrame)
        {
            this._selectedCallFrame = callFrame;
            if (!this._selectedCallFrame)
                return;

            this.dispatchEventToListeners(DebuggerModel.Events.CallFrameSelected, callFrame);
        },

        /**
         * @return {?CallFrame}
         */
        selectedCallFrame: function()
        {
            return this._selectedCallFrame;
        },

        /**
         * @param {string} code
         * @param {string} objectGroup
         * @param {boolean} includeCommandLineAPI
         * @param {boolean} doNotPauseOnExceptionsAndMuteConsole
         * @param {boolean} returnByValue
         * @param {boolean} generatePreview
         * @param {function(?RemoteObject, boolean, RemoteObject=)} callback
         */
        evaluateOnSelectedCallFrame: function(code, objectGroup, includeCommandLineAPI, doNotPauseOnExceptionsAndMuteConsole, returnByValue, generatePreview, callback)
        {
            /**
             * @param {?RemoteObject} result
             * @param {boolean=} wasThrown
             */
            function didEvaluate(result, wasThrown)
            {
                if (returnByValue)
                    callback(null, !!wasThrown, wasThrown ? null : result);
                else
                    callback(RemoteObject.fromPayload(result), !!wasThrown);

                if (objectGroup === "console")
                    this.dispatchEventToListeners(DebuggerModel.Events.ConsoleCommandEvaluatedInSelectedCallFrame);
            }

            this.selectedCallFrame().evaluate(code, objectGroup, includeCommandLineAPI, doNotPauseOnExceptionsAndMuteConsole, returnByValue, generatePreview, didEvaluate.bind(this));
        },

        /**
         * @param {function(Object)} callback
         */
        getSelectedCallFrameVariables: function(callback)
        {
            var result = { this: true };

            var selectedCallFrame = this._selectedCallFrame;
            if (!selectedCallFrame)
                callback(result);

            var pendingRequests = 0;

            function propertiesCollected(properties)
            {
                for (var i = 0; properties && i < properties.length; ++i)
                    result[properties[i].name] = true;
                if (--pendingRequests == 0)
                    callback(result);
            }

            for (var i = 0; i < selectedCallFrame.scopeChain.length; ++i) {
                var scope = selectedCallFrame.scopeChain[i];
                var object = RemoteObject.fromPayload(scope.object);
                pendingRequests++;
                object.getAllProperties(false, propertiesCollected);
            }
        },

        /**
         * @param {boolean} active
         */
        setBreakpointsActive: function(active)
        {
            if (this._breakpointsActive === active)
                return;
            this._breakpointsActive = active;
            CSInspector.debugAgent.setBreakpointsActive(active);
            this.dispatchEventToListeners(DebuggerModel.Events.BreakpointsActiveStateChanged, active);
        },

        /**
         * @return {boolean}
         */
        breakpointsActive: function()
        {
            return this._breakpointsActive;
        },

        /**
         * @param {DebuggerModel.Location} rawLocation
         * @param {function(UILocation):(boolean|undefined)} updateDelegate
         * @return {Script.Location}
         */
        createLiveLocation: function(rawLocation, updateDelegate)
        {
            var script = this._scripts[rawLocation.scriptId];
            return script.createLiveLocation(rawLocation, updateDelegate);
        },

        __proto__: EventEmitter.prototype
    };

    DebuggerModel.prototype._connectErrorHandler = function(){
        this.emit(DebuggerModel.Events.DebugFinished, true);
        console.error("remote debugger closed unexpectedly.");
    };

    DebuggerModel.Events = {
        SessionInit: "sessionInit",
        RenderSnippet: "renderSnippet",
        DebuggerPaused: "debuggerPaused",
        DebugFinished: "debugFinished",
        ParsedScriptSource: "ParsedScriptSource",
        CallFrameSelected: "CallFrameSelected",

        /*
        DebuggerWasEnabled: "DebuggerWasEnabled",
        DebuggerWasDisabled: "DebuggerWasDisabled",
        DebuggerResumed: "DebuggerResumed",
        */
        /*
        FailedToParseScriptSource: "FailedToParseScriptSource",
        BreakpointResolved: "BreakpointResolved",
        ConsoleCommandEvaluatedInSelectedCallFrame: "ConsoleCommandEvaluatedInSelectedCallFrame",
        */
        BreakpointsActiveStateChanged: "BreakpointsActiveStateChanged"
    };

    function Dispatcher(model){
        this._model = model;
    }

    Dispatcher.prototype = {
        "SessionInit": function(resources, resumeEvaluate){
            this._model._parseScriptSources(resources);
            CSInspector.debugAgent.sessionReady(resumeEvaluate);
        },
        "DebugPaused": function(callFrames, watchExpressions, breakpointIds){
            this._model._pausedScript(callFrames, 0, null, breakpointIds || [])
        },
        "DebugFinished": function(exit){
            this._model.emit(DebuggerModel.Events.DebugFinished, exit);
        }
    };

    Dispatcher.prototype.SessionInit.parameters = ["resources", "resumeEvaluate"];
    Dispatcher.prototype.DebugPaused.parameters = ["callFrames", "watchExpressions"];
    Dispatcher.prototype.DebugFinished.parameters = ["exitOnFinished"];

    return DebuggerModel;
});
