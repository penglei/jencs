//Workspace主要是文件数据与界面的controller
//架构:
//                                                         Project  -]
//                                                            |      |
//UISourceCodeProvider --> [Simple]WorkspaceProvider  --> Workspace  |->  events interface
//                         [Simple]ProjectDelegate            |      |
//                                                       UISourceCode]
//
define(function(require, exports){

    var EventObjectEmitter = require("events").EventObjectEmitter;
    var UISourceCode = require("UISourceCode");

    /**
     * @constructor
     * @extends {EventObjectEmitter}
     */
    function Workspace()
    {
        /** @type {!Object.<string, Project>} */
        this._projects = {};
    }

    Workspace.Events = {
        UISourceCodeAdded: "uiSourceCodeAdded",
        UISourceCodeContentCommitted: "UISourceCodeContentCommitted",
        ProjectWillReset: "ProjectWillReset"
    }

    Workspace.ProjectTypes = {
        Network: "network"/*,
        FileSystem: "filesystem"*///我们其实只有network类型
    }

    Workspace.prototype = {
        /**
         * @param {string} projectId
         * @param {string} path
         * @return {?UISourceCode}
         */
        uiSourceCode: function(projectId, path)
        {
            var project = this._projects[projectId];
            return project ? project.uiSourceCode(path) : null;
        },

        /**
         * @param {string} type
         * @return {Array.<UISourceCode>}
         */
        uiSourceCodesForProjectType: function(type)
        {
            var result = [];
            for (var projectName in this._projects) {
                var project = this._projects[projectName];
                if (project.type() === type)
                    result = result.concat(project.uiSourceCodes());
            }
            return result;
        },

        /**
         * @param {ProjectDelegate} projectDelegate
         * @return {Project}
         */
        addProject: function(projectDelegate)
        {
            var projectId = projectDelegate.id();
            this._projects[projectId] = new Project(this, projectDelegate);
            return this._projects[projectId];
        },

        /**
         * @param {string} projectId
         */
        removeProject: function(projectId)
        {
            var project = this._projects[projectId];
            if (!project)
                return;
            project.dispose();
            delete this._projects[projectId];
        },

        /**
         * @param {string} projectId
         * @return {Project}
         */
        project: function(projectId)
        {
            return this._projects[projectId];
        },

        /**
         * @return {Array.<Project>}
         */
        projects: function()
        {
            return Object.values(this._projects);
        },

        /**
         * @param {string} type
         * @return {Array.<Project>}
         */
        projectsForType: function(type)
        {
            function filterByType(project)
            {
                return project.type() === type;
            }
            return this.projects().filter(filterByType);
        },

        /**
         * @return {Array.<UISourceCode>}
         */
        uiSourceCodes: function()
        {
            var result = [];
            for (var projectId in this._projects) {
                var project = this._projects[projectId];
                result = result.concat(project.uiSourceCodes());
            }
            return result;
        },

        /**
         * @param {string} url
         * @return {UISourceCode}
         */
        uiSourceCodeForURL: function(url)
        {
            //
        },

        /**
         * @param {string} fileSystemPath
         * @param {string} filePath
         * @return {string}
         */
        urlForPath: function(fileSystemPath, filePath)
        {
            //FileSystemMapping.urlForPath();
        },

        __proto__: EventObjectEmitter.prototype
    }

    exports.Workspace = Workspace;
});
