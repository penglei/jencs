/*
 * Copyright (C) 2012 Google Inc. All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above
 * copyright notice, this list of conditions and the following disclaimer
 * in the documentation and/or other materials provided with the
 * distribution.
 *     * Neither the name of Google Inc. nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

define(function(require){

var Workspace = require("Workspace").Workspace;
var ParsedURL = require("ParsedURL");
var DebugModel = require("DebugModel");
var Resource = require("Resource").Resource;
var ResourceTypes = require("Resource").ResourceTypes;

/**
 * @constructor
 * @param {SimpleWorkspaceProvider} networkWorkspaceProvider
 * @param {Workspace} workspace
 */
function NetworkUISourceCodeProvider(networkWorkspaceProvider, workspace)
{
    this._networkWorkspaceProvider = networkWorkspaceProvider;
    this._workspace = workspace;

    //CSInspector.debugModel.addEventListener(DebugModel.Events.FileSourcesAdded, this._fileResourcesAdded, this)
    //WebInspector.resourceTreeModel.addEventListener(WebInspector.ResourceTreeModel.EventTypes.ResourceAdded, this._resourceAdded, this);
    CSInspector.debugModel.addEventListener(DebugModel.Events.ParsedScriptSource, this._parsedScriptSource, this);

    this._processedURLs = {};
}

NetworkUISourceCodeProvider.prototype = {
    _populate: function()
    {
        function populateFrame(frame)
        {
            for (var i = 0; i < frame.childFrames.length; ++i)
                populateFrame.call(this, frame.childFrames[i]);

            var resources = frame.resources();
            for (var i = 0; i < resources.length; ++i)
                this._resourceAdded({data:resources[i]});
        }

        populateFrame.call(this, ResourceTreeModel.mainFrame);
    },

    /**
     * @param {Event} event
     */
    _resourceAdded: function(event)
    {
        var resource = /** @type {Resource} */ (event.data);

        this._addFile(resource.url, resource);
    },

    /**
     * @param {Event} event
     */
    _parsedScriptSource: function(event)
    {
        var script = /** @type {Script} */ (event.data);
        // Filter out embedder injected content scripts.
        /*x
        if (script.isContentScript && !script.hasSourceURL) {
            var parsedURL = new ParsedURL(script.sourceURL);
            if (!parsedURL.host)
                return;
        }*/
        this._addFile(script.sourceURL, script, script.isContentScript);
    },

    /*
    _fileResourcesAdded: function(event) {
        var fileResources = event.data;
        for(var i = 0, l = fileResources.length; i < l; i++) {
            var fileResourceItem = fileResources[i];
            var resourceProvider = new Resource(fileResourceItem.url, fileResourceItem.mimeType, ResourceTypes.Script, fileResourceItem.content);
            this._addFile(fileResourceItem.url, resourceProvider, true);
        }
    },
    */

    /**
     * @param {string} url
     * @param {ContentProvider} contentProvider
     */
    _addFile: function(url, contentProvider, isContentScript)
    {
        if (this._processedURLs[url])
            return;

        //进入下面这条语句可以看出ContentProvider只要提供四个接口:contentType, contentURL, requestContent, searchInContent
        //contentProvider = new ContentProviderOverridingMimeType(contentProvider, mimeType);
        this._processedURLs[url] = true;
        this._networkWorkspaceProvider.addFileForURL(url, contentProvider, false, isContentScript);
    },

    _reset: function()
    {
        this._processedURLs = {};
        this._networkWorkspaceProvider.reset();
        this._populate();
    }
}

return NetworkUISourceCodeProvider;
});
