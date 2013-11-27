define(function(){
    /**
     * @param {Workspace} workspace
     * @param {ProjectDelegate} projectDelegate
     * @constructor
     */
    function Project(workspace, projectDelegate)
    {
        /** @type {Object.<string, {uiSourceCode: UISourceCode, index: number}>} */
        this._uiSourceCodesMap = {};
        /** @type {Array.<UISourceCode>} */
        this._uiSourceCodesList = [];
        this._workspace = workspace;
        this._projectDelegate = projectDelegate;
        this._displayName = this._projectDelegate.displayName();
        this._projectDelegate.addEventListener(ProjectDelegate.Events.FileAdded, this._fileAdded, this);
    }

    Project.prototype = {
        /**
         * @return {string}
         */
        id: function()
        {
            return this._projectDelegate.id();
        },

        /**
         * @return {string}
         */
        type: function()
        {
            return this._projectDelegate.type();
        },

        /**
         * @return {string}
         */
        displayName: function()
        {
            return this._displayName;
        },

        /**
         * @return {boolean}
         */
        isServiceProject: function()
        {
            return false;
        },

        _fileAdded: function(event)
        {
            var fileDescriptor = /** @type {FileDescriptor} */ (event.data);
            var path = fileDescriptor.parentPath ? fileDescriptor.parentPath + "/" + fileDescriptor.name : fileDescriptor.name;
            var uiSourceCode = this.uiSourceCode(path);
            if (uiSourceCode)
                return;

            uiSourceCode = new UISourceCode(
                                    this,
                                    fileDescriptor.parentPath,
                                    fileDescriptor.name,
                                    fileDescriptor.originURL,
                                    fileDescriptor.url,
                                    fileDescriptor.contentType,
                                    fileDescriptor.isEditable
                                );
            uiSourceCode.isContentScript = fileDescriptor.isContentScript;

            this._uiSourceCodesMap[path] = {uiSourceCode: uiSourceCode, index: this._uiSourceCodesList.length};
            this._uiSourceCodesList.push(uiSourceCode);
            this._workspace.dispatchEventToListeners(Workspace.Events.UISourceCodeAdded, uiSourceCode);
        },

        _reset: function()
        {
            this._workspace.dispatchEventToListeners(Workspace.Events.ProjectWillReset, this);
            this._uiSourceCodesMap = {};
            this._uiSourceCodesList = [];
        },

        /**
         * @param {string} path
         * @return {?UISourceCode}
         */
        uiSourceCode: function(path)
        {
            var entry = this._uiSourceCodesMap[path];
            return entry ? entry.uiSourceCode : null;
        },

        /**
         * @return {Array.<UISourceCode>}
         */
        uiSourceCodes: function()
        {
            return this._uiSourceCodesList;
        },

        /**
         * @param {UISourceCode} uiSourceCode
         * @param {function(?Date, ?number)} callback
         */
        requestMetadata: function(uiSourceCode, callback)
        {
            this._projectDelegate.requestMetadata(uiSourceCode.path(), callback);
        },

        /**
         * @param {UISourceCode} uiSourceCode
         * @param {function(?string,boolean,string)} callback
         */
        requestFileContent: function(uiSourceCode, callback)
        {
            this._projectDelegate.requestFileContent(uiSourceCode.path(), callback);
        },


        /**
         * @param {UISourceCode} uiSourceCode
         * @param {string} newContent
         * @param {function(?string)} callback
         */
        setFileContent: function(uiSourceCode, newContent, callback)
        {
            this._projectDelegate.setFileContent(uiSourceCode.path(), newContent, onSetContent.bind(this));

            /**
             * @param {?string} content
             */
            function onSetContent(content)
            {
                this._workspace.dispatchEventToListeners(Workspace.Events.UISourceCodeContentCommitted, { uiSourceCode: uiSourceCode, content: newContent });
                callback(content);
            }
        },

        /**
         * @param {string} path
         */
        refresh: function(path)
        {
            this._projectDelegate.refresh(path);
        },

        remove: function()
        {
            this._projectDelegate.remove();
        },

        /**
         * @param {UISourceCode} uiSourceCode
         * @param {string} query
         * @param {boolean} caseSensitive
         * @param {boolean} isRegex
         * @param {function(Array.<ContentProvider.SearchMatch>)} callback
         */
        searchInFileContent: function(uiSourceCode, query, caseSensitive, isRegex, callback)
        {
            this._projectDelegate.searchInFileContent(uiSourceCode.path(), query, caseSensitive, isRegex, callback);
        },

        /**
         * @param {string} query
         * @param {boolean} caseSensitive
         * @param {boolean} isRegex
         * @param {Progress} progress
         * @param {function(StringMap)} callback
         */
        searchInContent: function(query, caseSensitive, isRegex, progress, callback)
        {
            this._projectDelegate.searchInContent(query, caseSensitive, isRegex, progress, callback);
        },

        /**
         * @param {Progress} progress
         * @param {function()} callback
         */
        indexContent: function(progress, callback)
        {
            this._projectDelegate.indexContent(progress, callback);
        },

        dispose: function()
        {
            this._projectDelegate.reset();
        }
    }

    return Project;
});
