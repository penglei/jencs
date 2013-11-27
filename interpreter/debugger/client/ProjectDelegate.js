define(function(){

/**
 * @interface
 * @extends {EventTarget}
 */
function ProjectDelegate() { }

ProjectDelegate.Events = {
    FileAdded: "FileAdded",
    FileRemoved: "FileRemoved",
    Reset: "Reset",
}

ProjectDelegate.prototype = {
    /**
     * @return {string}
     */
    id: function() { },

    /**
     * @return {string}
     */
    type: function() { },

    /**
     * @return {string}
     */
    displayName: function() { }, 

    /**
     * @param {string} path
     * @param {function(?Date, ?number)} callback
     */
    requestMetadata: function(path, callback) { },

    /**
     * @param {string} path
     * @param {function(?string,boolean,string)} callback
     */
    requestFileContent: function(path, callback) { },

    /**
     * @return {boolean}
     */
    canSetFileContent: function() { },

    /**
     * @param {string} path
     * @param {string} newContent
     * @param {function(?string)} callback
     */
    setFileContent: function(path, newContent, callback) { },

    /**
     * @return {boolean}
     */
    canRename: function() { },

    /**
     * @param {string} path
     * @param {string} newName
     * @param {function(boolean, string=)} callback
     */
    rename: function(path, newName, callback) { },

    /**
     * @param {string} path
     */
    refresh: function(path) { },

    /**
     * @param {string} path
     */
    excludeFolder: function(path) { },

    /**
     * @param {string} path
     * @param {?string} name
     * @param {function(?string)} callback
     */
    createFile: function(path, name, callback) { },

    /**
     * @param {string} path
     */
    deleteFile: function(path) { },

    remove: function() { },

    /**
     * @param {string} path
     * @param {string} query
     * @param {boolean} caseSensitive
     * @param {boolean} isRegex
     * @param {function(Array.<ContentProvider.SearchMatch>)} callback
     */
    searchInFileContent: function(path, query, caseSensitive, isRegex, callback) { },

    /**
     * @param {string} query
     * @param {boolean} caseSensitive
     * @param {boolean} isRegex
     * @param {Progress} progress
     * @param {function(StringMap)} callback
     */
    searchInContent: function(query, caseSensitive, isRegex, progress, callback) { },

    /**
     * @param {Progress} progress
     * @param {function()} callback
     */
    indexContent: function(progress, callback) { }
}

return ProjectDelegate;
});
