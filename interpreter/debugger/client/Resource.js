define(function(require){
var EventObjectEmitter = require("events").EventObjectEmitter;
/**
 * @constructor
 * @param {string} name
 * @param {string} title
 * @param {string} categoryTitle
 * @param {string} color
 * @param {boolean} isTextType
 */
function ResourceType(name, title, categoryTitle, color, isTextType){
{
    this._name = name;
    this._title = title;
    this._categoryTitle = categoryTitle;
    this._color = color;
    this._isTextType = isTextType;
}

ResourceType.prototype = {
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
    title: function()
    {
        return this._title;
    },

    /**
     * @return {string}
     */
    categoryTitle: function()
    {
        return this._categoryTitle;
    },

    /**
     * @return {string}
     */
    color: function()
    {
        return this._color;
    },

    /**
     * @return {boolean}
     */
    isTextType: function()
    {
        return this._isTextType;
    },

    /**
     * @return {string}
     */
    toString: function()
    {
        return this._name;
    },

    /**
     * @return {string}
     */
    canonicalMimeType: function()
    {
        if (this === WebInspector.resourceTypes.Document)
            return "text/html";
        if (this === WebInspector.resourceTypes.Script)
            return "text/javascript";
        if (this === WebInspector.resourceTypes.Stylesheet)
            return "text/css";
        return "";
    }
}

ResourceType.Script = new ResourceType("script", "Script", "Scripts", "rgb(255,121,0)", true);

function Resource(url, mimeType, type)
{
    this.url = url;
    this._mimeType = mimeType;
    this._type = type;

    /** @type {?string} */ this._content = "";
    /** @type {boolean} */ this._contentEncoded = "utf-8";
    this._pendingContentCallbacks = [];
}

Resource.prototype = {

    /**
     * @return {string}
     */
    get url()
    {
        return this._url;
    },

    set url(x)
    {
        this._url = x;
        this._parsedURL = new WebInspector.ParsedURL(x);
    },

    get parsedURL()
    {
        return this._parsedURL;
    },

    /**
     * @return {string}
     */
    get displayName()
    {
        return this._parsedURL.displayName;
    },

    /**
     * @return {string}
     */
    get mimeType()
    {
        return this._request ? this._request.mimeType : this._mimeType;
    },

    /**
     * @return {WebInspector.ResourceType}
     */
    get type()
    {
        return this._type;
    },

    /**
     * @return {Array.<WebInspector.ConsoleMessage>}
     */
    get messages()
    {
        return this._messages || [];
    },

    /**
     * @param {WebInspector.ConsoleMessage} msg
     */
    addMessage: function(msg)
    {
        if (!msg.isErrorOrWarning() || !msg.message)
            return;

        if (!this._messages)
            this._messages = [];
        this._messages.push(msg);
        this.dispatchEventToListeners(WebInspector.Resource.Events.MessageAdded, msg);
    },

    /**
     * @return {number}
     */
    get errors()
    {
        return this._errors || 0;
    },

    set errors(x)
    {
        this._errors = x;
    },

    /**
     * @return {number}
     */
    get warnings()
    {
        return this._warnings || 0;
    },

    set warnings(x)
    {
        this._warnings = x;
    },

    /**
     * @return {?string}
     */
    get content()
    {
        return this._content;
    },

    /**
     * @return {boolean}
     */
    get contentEncoded()
    {
        return this._contentEncoded;
    },

    /**
     * @return {string}
     */
    contentURL: function()
    {
        return this._url;
    },

    /**
     * @return {WebInspector.ResourceType}
     */
    contentType: function()
    {
        return this.type;
    },

    /**
     * @param {function(?string, boolean, string)} callback
     */
    requestContent: function(callback)
    {
        if (typeof this._content !== "undefined") {
            callback(this._content, !!this._contentEncoded, this.canonicalMimeType());
            return;
        }

        this._pendingContentCallbacks.push(callback);
        if (!this._request || this._request.finished)
            this._innerRequestContent();
    },


    _innerRequestContent: function()
    {
        if (this._contentRequested)
            return;
        this._contentRequested = true;

        /**
         * @param {?Protocol.Error} error
         * @param {?string} content
         * @param {boolean} contentEncoded
         */
        function contentLoaded(error, content, contentEncoded)
        {
            if (error || content === null) {
                loadFallbackContent.call(this, error);
                return;
            }
            replyWithContent.call(this, content, contentEncoded);
        }

        /**
         * @param {?string} content
         * @param {boolean} contentEncoded
         */
        function replyWithContent(content, contentEncoded)
        {
            this._content = content;
            this._contentEncoded = contentEncoded;
            var callbacks = this._pendingContentCallbacks.slice();
            for (var i = 0; i < callbacks.length; ++i)
                callbacks[i](this._content, this._contentEncoded, this.canonicalMimeType());
            this._pendingContentCallbacks.length = 0;
            delete this._contentRequested;
        }

        /**
         * @param {?Protocol.Error} error
         * @param {string} content
         * @param {boolean} contentEncoded
         */
        function resourceContentLoaded(error, content, contentEncoded)
        {
            contentLoaded.call(this, error, content, contentEncoded);
        }

        /**
         * @param {?Protocol.Error} error
         */
        function loadFallbackContent(error)
        {
            var scripts = WebInspector.debuggerModel.scriptsForSourceURL(this.url);
            if (!scripts.length) {
                console.error("Resource content request failed: " + error);
                replyWithContent.call(this, null, false);
                return;
            }

            var contentProvider;
            if (this.type === WebInspector.resourceTypes.Document)
                contentProvider = new WebInspector.ConcatenatedScriptsContentProvider(scripts);
            else if (this.type === WebInspector.resourceTypes.Script)
                contentProvider = scripts[0];

            if (!contentProvider) {
                console.error("Resource content request failed: " + error);
                replyWithContent.call(this, null, false);
                return;
            }

            contentProvider.requestContent(fallbackContentLoaded.bind(this));
        }

        /**
         * @param {?string} content
         * @param {boolean} contentEncoded
         * @param {string} mimeType
         */
        function fallbackContentLoaded(content, contentEncoded, mimeType)
        {
            replyWithContent.call(this, content, contentEncoded);
        }

        if (this.request) {
            /**
             * @param {?string} content
             * @param {boolean} contentEncoded
             * @param {string} mimeType
             */
            function requestContentLoaded(content, contentEncoded, mimeType)
            {
                contentLoaded.call(this, null, content, contentEncoded);
            }

            this.request.requestContent(requestContentLoaded.bind(this));
            return;
        }
        PageAgent.getResourceContent(this.frameId, this.url, resourceContentLoaded.bind(this));
    },

    __proto__: EventObjectEmitter.prototype
}

return Resource;
});
