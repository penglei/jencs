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
function UISourceCode(project, id, parentPath, name, url, content, contentType)
{
    EventObjectEmitter.call(this);
    this._project = project;
    this.id = id;
    this._parentPath = parentPath;
    this._name = name;
    this._url = url;
    /** @type {!Array.<function(?string,boolean,string)>} */
    this._requestContentCallbacks = [];
    this._content = content || "";

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
        return this.id + ":" + this._url;
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
     * @return {WebInspector.ResourceType}
     */
    contentType: function()
    {
        return this._contentType;
    },

    /**
     * @return {string}
     */
    originURL: function()
    {
        return this._url;
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

    __proto__: EventObjectEmitter.prototype
}

return UISourceCode;

});

