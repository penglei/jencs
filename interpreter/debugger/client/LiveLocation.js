define(function(){
/**
 * @constructor
 * @param {RawLocation} rawLocation
 * @param {function(UILocation):(boolean|undefined)} updateDelegate
 */
function LiveLocation(rawLocation, updateDelegate)
{
    this._rawLocation = rawLocation;
    this._updateDelegate = updateDelegate;
    this._uiSourceCodes = [];
}

LiveLocation.prototype = {
    update: function()
    {
        var uiLocation = this.uiLocation();
        if (uiLocation) {
            var uiSourceCode = uiLocation.uiSourceCode;
            if (this._uiSourceCodes.indexOf(uiSourceCode) === -1) {
                uiSourceCode.addLiveLocation(this);
                this._uiSourceCodes.push(uiSourceCode);
            }
            var oneTime = this._updateDelegate(uiLocation);
            if (oneTime)
                this.dispose();
        }
    },

    /**
     * @return {RawLocation}
     */
    rawLocation: function()
    {
        return this._rawLocation;
    },

    /**
     * @return {UILocation}
     */
    uiLocation: function()
    {
        // Should be overridden by subclasses.
    },

    dispose: function()
    {
        for (var i = 0; i < this._uiSourceCodes.length; ++i)
            this._uiSourceCodes[i].removeLiveLocation(this);
        this._uiSourceCodes = [];
    }
}

return LiveLocation;
});
