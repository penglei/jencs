define(function(require){

var ContentProviderBasedProjectDelegate = require("ContentProviderBasedProjectDelegate");
var ParsedURL = require("ParsedURL");
var Workspace = require("Workspace").Workspace;
/**
 * @constructor
 * @extends {ContentProviderBasedProjectDelegate}
 * @param {string} name
 * @param {string} type
 */
function SimpleProjectDelegate(name, type)
{
    ContentProviderBasedProjectDelegate.call(this, type);
    this._name = name;
    this._lastUniqueSuffix = 0;
}

SimpleProjectDelegate.projectId = function(name, type)
{
    var typePrefix = type !== Workspace.ProjectTypes.Network ? (type + ":") : "";
    return typePrefix + name;
}

SimpleProjectDelegate.prototype = {
    /**
     * @return {string}
     */
    id: function()
    {
        return SimpleProjectDelegate.projectId(this._name, this.type());
    },

    /**
     * @return {string}
     */
    displayName: function()
    {
        if (typeof this._displayName !== "undefined")
            return this._displayName;
        if (!this._name) {
            this._displayName = this.type() !== Workspace.ProjectTypes.Snippets ? UIString("(no domain)") : "";
            return this._displayName;
        }
        var parsedURL = new ParsedURL(this._name);
        if (parsedURL.isValid) {
            this._displayName = parsedURL.host + (parsedURL.port ? (":" + parsedURL.port) : "");
            if (!this._displayName)
                this._displayName = this._name;
        }
        else
            this._displayName = this._name;
        return this._displayName;
    },

    /**
     * @param {string} parentPath
     * @param {string} name
     * @param {string} url
     * @param {ContentProvider} contentProvider
     * @param {boolean} isEditable
     * @param {boolean=} isContentScript
     * @return {string}
     */
    addFile: function(parentPath, name, forceUniquePath, url, contentProvider, isEditable, isContentScript)
    {
        if (forceUniquePath)
            name = this._ensureUniqueName(parentPath, name);
        return this.addContentProvider(parentPath, name, url, contentProvider, isEditable, isContentScript);
    },

    /**
     * @param {string} parentPath
     * @param {string} name
     * @return {string}
     */
    _ensureUniqueName: function(parentPath, name)
     {
        var path = parentPath ? parentPath + "/" + name : name;
        var uniquePath = path;
        var suffix = "";
        var contentProviders = this.contentProviders();
        while (contentProviders[uniquePath]) {
            suffix = " (" + (++this._lastUniqueSuffix) + ")";
            uniquePath = path + suffix;
        }
        return name + suffix;
    },

    __proto__: ContentProviderBasedProjectDelegate.prototype
}

return SimpleProjectDelegate;
});
