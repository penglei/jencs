define(function(require){

    var CallFrame = require("CallFrame");
    /**
     * @constructor
     * @param {DebuggerModel} model
     * @param {Array.<DebuggerAgent.CallFrame>} callFrames
     * @param {string} reason
     * @param {Object|undefined} auxData
     * @param {Array.<string>} breakpointIds
     */
    function DebuggerPausedDetails(model, callFrames, reason, auxData, breakpointIds)
    {
        this.callFrames = [];
        for (var i = 0; i < callFrames.length; ++i) {
            var callFrame = callFrames[i];
            var script = model.scriptForId(callFrame.location.scriptId);
            if (script)
                this.callFrames.push(new CallFrame(script, callFrame));
        }
        this.reason = reason;
        this.auxData = auxData;
        this.breakpointIds = breakpointIds;
    }

    DebuggerPausedDetails.prototype = {
        dispose: function()
        {
            for (var i = 0; i < this.callFrames.length; ++i) {
                var callFrame = this.callFrames[i];
                callFrame.dispose();
            }
        }
    }

    return DebuggerPausedDetails;
});
