define(function(require){

var EventObjectEmitter = require("events").EventObjectEmitter;
var SimpleProjectDelegate = require("SimpleProjectDelegate");
var ParsedURL = require("ParsedURL");

/**
 * @constructor
 * @extends {Object}
 * @param {Workspace} workspace
 * @param {string} type
 */
function SimpleWorkspaceProvider(workspace, type)
{
    this._workspace = workspace;
    this._type = type;
    this._simpleProjectDelegates = {};
}

SimpleWorkspaceProvider.prototype = {
    /**
     * @param {string} projectName
     * @return {SimpleProjectDelegate}
     */
    _projectDelegate: function(projectName)
    {
        if (this._simpleProjectDelegates[projectName])
            return this._simpleProjectDelegates[projectName];
        var simpleProjectDelegate = new SimpleProjectDelegate(projectName, this._type);
        this._simpleProjectDelegates[projectName] = simpleProjectDelegate;
        this._workspace.addProject(simpleProjectDelegate);
        return simpleProjectDelegate;
    },

    /**
     * @param {string} url
     * @param {ContentProvider} contentProvider
     * @param {boolean} isEditable
     * @param {boolean=} isContentScript
     * @return {UISourceCode}
     */
    addFileForURL: function(url, contentProvider, isEditable, isContentScript)
    {
        return this._innerAddFileForURL(url, contentProvider, isEditable, false, isContentScript);
    },

    /**
     * @param {string} url
     * @param {ContentProvider} contentProvider
     * @param {boolean} isEditable
     * @param {boolean=} isContentScript
     * @return {UISourceCode}
     */
    addUniqueFileForURL: function(url, contentProvider, isEditable, isContentScript)
    {
        return this._innerAddFileForURL(url, contentProvider, isEditable, true, isContentScript);
    },

    /**
     * @param {string} url
     * @param {ContentProvider} contentProvider
     * @param {boolean} isEditable
     * @param {boolean} forceUnique
     * @param {boolean=} isContentScript
     * @return {UISourceCode}
     */
    _innerAddFileForURL: function(url, contentProvider, isEditable, forceUnique, isContentScript)
    {
        var splitURL = ParsedURL.splitURL(url);
        var projectName = splitURL[0];
        var parentPath = splitURL.slice(1, splitURL.length - 1).join("/");
        var name = splitURL[splitURL.length - 1];
        var projectDelegate = this._projectDelegate(projectName);
        var path = projectDelegate.addFile(parentPath, name, forceUnique, url, contentProvider, isEditable, isContentScript);
        return this._workspace.uiSourceCode(projectDelegate.id(), path);
    },

    reset: function()
    {
        for (var projectName in this._simpleProjectDelegates)
            this._simpleProjectDelegates[projectName].reset();
        this._simpleProjectDelegates = {};
    },

    __proto__: EventObjectEmitter.prototype
}

return SimpleWorkspaceProvider;
});
