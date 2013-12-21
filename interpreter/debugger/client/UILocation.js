define(function(){
/**
 * @constructor
 * @param {UISourceCode} uiSourceCode
 * @param {number} lineNumber
 * @param {number} columnNumber
 */
function UILocation(uiSourceCode, lineNumber, columnNumber)
{
    this.uiSourceCode = uiSourceCode;
    this.lineNumber = lineNumber;
    this.columnNumber = columnNumber;
}

UILocation.prototype = {
    /**
     * @return {RawLocation}
     */
    uiLocationToRawLocation: function()
    {
        return this.uiSourceCode.uiLocationToRawLocation(this.lineNumber, this.columnNumber);
    },

    /**
     * @return {?string}
     */
    url: function()
    {
        return this.uiSourceCode.contentURL();
    },

    /**
     * @return {string}
     */
    linkText: function()
    {
        var linkText = this.uiSourceCode.displayName();
        if (typeof this.lineNumber === "number")
            linkText += ":" + (this.lineNumber + 1);
        return linkText;
    }
}

return UILocation;

});
