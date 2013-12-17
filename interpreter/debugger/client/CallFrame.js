define(function(){
    /**
     * @constructor
     * @param {Script} script
     * @param {DebuggerAgent.CallFrame} payload
     */
    function CallFrame(script, payload)
    {
        this._script = script;
        this._payload = payload;
        this._locations = [];
    }

    CallFrame.prototype = {
        /**
         * @return {Script}
         */
        get script()
        {
            return this._script;
        },

        /**
         * @return {string}
         */
        get type()
        {
            return this._payload.type;
        },

        /**
         * @return {string}
         */
        get id()
        {
            return this._payload.callFrameId;
        },

        /**
         * @return {Array.<DebuggerAgent.Scope>}
         */
        get scopeChain()
        {
            return this._payload.scopeChain;
        },

        /**
         * @return {RuntimeAgent.RemoteObject}
         */
        get this()
        {
            return this._payload.this;
        },

        /**
         * @return {string}
         */
        get functionName()
        {
            return this._payload.functionName;
        },

        /**
         * @return {Location}
         */
        get location()
        {
            var rawLocation = /** @type {Location} */ (this._payload.location);
            return rawLocation;
        },

        /**
         * @param {string} code
         * @param {string} objectGroup
         * @param {boolean} includeCommandLineAPI
         * @param {boolean} doNotPauseOnExceptionsAndMuteConsole
         * @param {boolean} returnByValue
         * @param {boolean} generatePreview
         * @param {function(?RuntimeAgent.RemoteObject, boolean=)=} callback
         */
        evaluate: function(code, objectGroup, includeCommandLineAPI, doNotPauseOnExceptionsAndMuteConsole, returnByValue, generatePreview, callback)
        {
            /**
             * @this {CallFrame}
             * @param {?Protocol.Error} error
             * @param {RuntimeAgent.RemoteObject} result
             * @param {boolean=} wasThrown
             */
            function didEvaluateOnCallFrame(error, result, wasThrown)
            {
                if (error) {
                    console.error(error);
                    callback(null, false);
                    return;
                }
                callback(result, wasThrown);
            }
            CSInspector.debugAgent.evaluateOnCallFrame(this._payload.callFrameId, code, objectGroup, includeCommandLineAPI, doNotPauseOnExceptionsAndMuteConsole, returnByValue, generatePreview, didEvaluateOnCallFrame.bind(this));
        },

        /**
         * @param {function(?Protocol.Error=)=} callback
         */
        restart: function(callback)
        {
            /**
             * @this {CallFrame}
             * @param {?Protocol.Error} error
             * @param {Array.<DebuggerAgent.CallFrame>=} callFrames
             * @param {Object=} details
             */
            function protocolCallback(error, callFrames, details)
            {
                if (!error)
                    CSInspector.debugModel.callStackModified(callFrames, details);
                if (callback)
                    callback(error);
            }
            DebuggerAgent.restartFrame(this._payload.callFrameId, protocolCallback);
        },

        /**
         * @param {function(Array.<DebuggerAgent.Location>)} callback
         */
        getStepIntoLocations: function(callback)
        {
            if (this._stepInLocations) {
                callback(this._stepInLocations.slice(0));
                return;
            }
            /**
             * @param {?string} error
             * @param {Array.<DebuggerAgent.Location>=} stepInPositions
             */
            function getStepInPositionsCallback(error, stepInPositions) {
                if (error) {
                    return;
                }
                this._stepInLocations = stepInPositions;
                callback(this._stepInLocations.slice(0));
            }
            DebuggerAgent.getStepInPositions(this.id, getStepInPositionsCallback.bind(this));
        },

        /**
         * @param {function(UILocation):(boolean|undefined)} updateDelegate
         */
        createLiveLocation: function(updateDelegate)
        {
            var location = this._script.createLiveLocation(this.location, updateDelegate);
            this._locations.push(location);
            return location;
        },

        dispose: function(updateDelegate)
        {
            for (var i = 0; i < this._locations.length; ++i)
                this._locations[i].dispose();
            this._locations = [];
        }
    }

    return CallFrame;
});
