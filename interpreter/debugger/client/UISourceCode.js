define(function (require, exports) {

var EventObjectEmitter = require("events").EventObjectEmitter;

/**
 * @constructor
 * @extends {Object}
 * @param {string} parentPath
 * @param {string} name
 * @param {string} url
 * @param {string} content
 * @param {boolean} isEditable
 */
function UISourceCode(project, parentPath, name, url, contentType)
{
    EventObjectEmitter.call(this);
    this._project = project;
    this._parentPath = parentPath;
    this._name = name;
    this._url = url;

    /** @type {!Array.<function(?string,boolean,string)>} */
    this._requestContentCallbacks = [];
    /** @type {!Set.<!LiveLocation>} */
    this._liveLocations = new Set();
    /** @type {!Array.<PresentationConsoleMessage>} */
    this._consoleMessages = [];

    /** @type {!String} */
    this._content;

    this._contentType = contentType;
}

UISourceCode.Events = {
    TitleChanged: "TitleChanged"
}

UISourceCode.prototype = {
    /**
     * @return {string}
     */
    get url()
    {
        return this._url;
    },

    /**
     * @return {string}
     */
    name: function()
    {
        return this._name;
    },

    /**
     * @return {string}
     */
    parentPath: function()
    {
        return this._parentPath;
    },

    /**
     * @return {string}
     */
    path: function()
    {
        return this._parentPath ? this._parentPath + "/" + this._name : this._name;
    },

    /**
     * @return {string}
     */
    fullDisplayName: function()
    {
        return this._project.displayName() + "/" + (this._parentPath ? this._parentPath + "/" : "") + this.displayName(true);
    },

    /**
     * @param {boolean=} skipTrim
     * @return {string}
     */
    displayName: function(skipTrim)
    {
        var displayName = this.name() || UIString("[main]");
        return skipTrim ? displayName : displayName.trimEnd(100);
    },

    /**
     * @return {string}
     */
    uri: function()
    {
        var path = this.path();
        if (!this._project.id())
            return path;
        if (!path)
            return this._project.id();
        return this._project.id() + "/" + path;
    },

    /**
     * @return {string}
     */
    originURL: function()
    {
        return this._url;
    },

    /**
     * @return {boolean}
     */
    canRename: function()
    {
        //return this._project.canRename();
        return false;
    },

    /**
     * @param {string} newName
     * @param {function(boolean)} callback
     */
    rename: function(newName, callback)
    {
        this._project.rename(this, newName, innerCallback.bind(this));

        /**
         * @param {boolean} success
         * @param {string=} newName
         */
        function innerCallback(success, newName)
        {
            if (success)
                this._updateName(newName);
            callback(success);
        }
    },

    /**
     * @param {string} name
     */
    _updateName: function(name)
    {
        var oldURI = this.uri();
        this._name = name;
        // FIXME: why?
        this._url = name;
        this.dispatchEventToListeners(UISourceCode.Events.TitleChanged, oldURI);
    },

    /**
     * @return {string}
     */
    contentURL: function()
    {
        return this.originURL();
    },

    /**
     * @return {ResourceType}
     */
    contentType: function()
    {
        return this._contentType;
    },

    /**
     * @return {string}
     */
    mimeType: function()
    {
        return this._mimeType;
    },

    /**
     * @return {Project}
     */
    project: function()
    {
        return this._project;
    },

    /**
     * @return {?string}
     */
    content: function()
    {
        return this._content;
    },

    /**
     * @return {boolean}
     */
    isDirty: function()
    {
        //return typeof this._workingCopy !== "undefined" || typeof this._workingCopyGetter !== "undefined";
        return false;
    },

    /**
     * @return {boolean}
     */
    hasUnsavedCommittedChanges: function()
    {
        return false;/*
        var mayHavePersistingExtensions = extensionServer.hasSubscribers(extensionAPI.Events.ResourceContentCommitted);
        if (this._savedWithFileManager || this.project().canSetFileContent() || mayHavePersistingExtensions)
            return false;
        return !!this._hasCommittedChanges;
        */
    },

    /**
     * @param {function(?string,boolean,string)} callback
     */
    requestContent: function(callback)
    {
        if (this._content || this._contentLoaded) {
            callback(this._content, false, this._mimeType);
            return;
        }
        this._requestContentCallbacks.push(callback);
        if (this._requestContentCallbacks.length === 1)
            this._project.requestFileContent(this, this._fireContentAvailable.bind(this));
    },

    /**
     * @param {?string} content
     * @param {boolean} contentEncoded
     * @param {string} mimeType
     */
    _fireContentAvailable: function(content, contentEncoded, mimeType)
    {
        this._contentLoaded = true;
        this._mimeType = mimeType;
        this._content = content;

        var callbacks = this._requestContentCallbacks.slice();
        this._requestContentCallbacks = [];
        for (var i = 0; i < callbacks.length; ++i)
            callbacks[i](content, contentEncoded, mimeType);
    },

    /**
     * @param {!LiveLocation} liveLocation
     */
    addLiveLocation: function(liveLocation)
    {
        this._liveLocations.add(liveLocation);
    },

    /**
     * @param {!LiveLocation} liveLocation
     */
    removeLiveLocation: function(liveLocation)
    {
        this._liveLocations.remove(liveLocation);
    },

    updateLiveLocations: function()
    {
        var items = this._liveLocations.items();
        for (var i = 0; i < items.length; ++i)
            items[i].update();
    },

    /**
     * @param {UILocation} uiLocation
     */
    overrideLocation: function(uiLocation)
    {
        var location = this._formatterMapping.originalToFormatted(uiLocation.lineNumber, uiLocation.columnNumber);
        uiLocation.lineNumber = location[0];
        uiLocation.columnNumber = location[1];
        return uiLocation;
    },

    /**
     * @param {number} lineNumber
     * @param {number} columnNumber
     * @return {WebInspector.RawLocation}
     */
    uiLocationToRawLocation: function(lineNumber, columnNumber)
    {
        /*
        if (!this._sourceMapping)
            return null;
        var location = this._formatterMapping.formattedToOriginal(lineNumber, columnNumber);
        return this._sourceMapping.uiLocationToRawLocation(this, location[0], location[1]);
        */
        if (!this.script) return;
        return CSInspector.debugModel.createRawLocation(this.script, lineNumber, columnNumber);
    },

    /**
     * @return {boolean}
     */
    formatted: function()
    {
        return !!this._formatted;
    },
    __proto__: EventObjectEmitter.prototype
}

return UISourceCode;

});

