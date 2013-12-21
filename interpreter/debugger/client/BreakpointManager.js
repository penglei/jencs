define(function(require, exports){
/*
 * Copyright (C) 2011 Google Inc. All rights reserved.
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

var EventObjectEmitter = require("events").EventObjectEmitter;
var DebugModel = require("DebugModel");
var Workspace = require("Workspace").Workspace;
var ResourceTypes = require("Resource").ResourceTypes;
var UISourceCode = require("UISourceCode");
var UILocation = require("UILocation");

/**
 * @constructor
 * @extends {Object}
 * @param {Setting} breakpointStorage
 * @param {DebuggerModel} debuggerModel
 * @param {Workspace} workspace
 */
function BreakpointManager(breakpointStorage, debuggerModel, workspace)
{
    this._storage = new BreakpointManager.Storage(this, breakpointStorage);
    this._debuggerModel = debuggerModel;
    this._workspace = workspace;

    this._breakpoints = new Map();
    this._breakpointForDebuggerId = {};
    this._breakpointsForUISourceCode = new Map();
    this._sourceFilesWithRestoredBreakpoints = {};

    this._debuggerModel.addEventListener(DebugModel.Events.BreakpointResolved, this._breakpointResolved, this);
    this._workspace.addEventListener(Workspace.Events.ProjectWillReset, this._projectWillReset, this);
    this._workspace.addEventListener(Workspace.Events.UISourceCodeAdded, this._uiSourceCodeAdded, this);
}

BreakpointManager.Events = {
    BreakpointAdded: "breakpoint-added",
    BreakpointRemoved: "breakpoint-removed"
}

BreakpointManager.sourceFileId = function(uiSourceCode)
{
    if (!uiSourceCode.url)
        return "";
    var deobfuscatedPrefix = uiSourceCode.formatted() ? "deobfuscated:" : "";
    return deobfuscatedPrefix + uiSourceCode.uri();
}

BreakpointManager.prototype = {
    /**
     * @param {UISourceCode} uiSourceCode
     */
    _restoreBreakpoints: function(uiSourceCode)
    {
        var sourceFileId = BreakpointManager.sourceFileId(uiSourceCode);
        if (!sourceFileId || this._sourceFilesWithRestoredBreakpoints[sourceFileId])
            return;
        this._sourceFilesWithRestoredBreakpoints[sourceFileId] = true;

        // Erase provisional breakpoints prior to restoring them.
        for (var debuggerId in this._breakpointForDebuggerId) {
            var breakpoint = this._breakpointForDebuggerId[debuggerId];
            if (breakpoint._sourceFileId !== sourceFileId)
                continue;
            breakpoint.remove(true);
        }
        this._storage._restoreBreakpoints(uiSourceCode);
    },

    /**
     * @param {Event} event
     */
    _uiSourceCodeAdded: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.data);
        this._restoreBreakpoints(uiSourceCode);
        if (uiSourceCode.contentType() === ResourceTypes.Script || uiSourceCode.contentType() === resourceTypes.Document) {
            uiSourceCode.addEventListener(UISourceCode.Events.SourceMappingChanged, this._uiSourceCodeMappingChanged, this);
            uiSourceCode.addEventListener(UISourceCode.Events.FormattedChanged, this._uiSourceCodeFormatted, this);
        }
    },

    /**
     * @param {Event} event
     */
    _uiSourceCodeFormatted: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._restoreBreakpoints(uiSourceCode);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    _resetBreakpoints: function(uiSourceCode)
    {
        var sourceFileId = BreakpointManager.sourceFileId(uiSourceCode);
        var breakpoints = this._breakpoints.keys();
        for (var i = 0; i < breakpoints.length; ++i) {
            var breakpoint = breakpoints[i];
            if (breakpoint._sourceFileId !== sourceFileId)
                return;
            if (breakpoint.enabled()) {
                breakpoint._removeFromDebugger();
                breakpoint._setInDebugger();
            }
        }
    },

    /**
     * @param {Event} event
     */
    _uiSourceCodeMappingChanged: function(event)
    {
        var identityHasChanged = /** @type {boolean} */ (event.data.identityHasChanged);
        if (!identityHasChanged)
            return;
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._resetBreakpoints(uiSourceCode);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @param {number} lineNumber
     * @param {string} condition
     * @param {boolean} enabled
     * @return {BreakpointManager.Breakpoint}
     */
    setBreakpoint: function(uiSourceCode, lineNumber, condition, enabled)
    {
        this._debuggerModel.setBreakpointsActive(true);
        return this._innerSetBreakpoint(uiSourceCode, lineNumber, condition, enabled);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @param {number} lineNumber
     * @param {string} condition
     * @param {boolean} enabled
     * @return {BreakpointManager.Breakpoint}
     */
    _innerSetBreakpoint: function(uiSourceCode, lineNumber, condition, enabled)
    {
        var breakpoint = this.findBreakpoint(uiSourceCode, lineNumber);
        if (breakpoint) {
            breakpoint._updateBreakpoint(condition, enabled);
            return breakpoint;
        }
        breakpoint = new BreakpointManager.Breakpoint(this, uiSourceCode, lineNumber, condition, enabled);
        this._breakpoints.put(breakpoint);
        return breakpoint;
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @param {number} lineNumber
     * @return {?BreakpointManager.Breakpoint}
     */
    findBreakpoint: function(uiSourceCode, lineNumber)
    {
        var breakpoints = this._breakpointsForUISourceCode.get(uiSourceCode);
        var lineBreakpoints = breakpoints ? breakpoints[lineNumber] : null;
        return lineBreakpoints ? lineBreakpoints[0] : null;
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {Array.<BreakpointManager.Breakpoint>}
     */
    breakpointsForUISourceCode: function(uiSourceCode)
    {
        var result = [];
        var breakpoints = /** @type {Array.<BreakpointManager.Breakpoint>} */(this._breakpoints.keys());
        for (var i = 0; i < breakpoints.length; ++i) {
            var breakpoint = breakpoints[i];
            var uiLocation = breakpoint._primaryUILocation;
            if (uiLocation.uiSourceCode === uiSourceCode)
                result.push(breakpoint);
        }
        return result;
    },

    /**
     * @return {Array.<BreakpointManager.Breakpoint>}
     */
    allBreakpoints: function()
    {
        var result = [];
        var breakpoints = /** @type {Array.<BreakpointManager.Breakpoint>} */(this._breakpoints.keys());
        return breakpoints;
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {Array.<{breakpoint: BreakpointManager.Breakpoint, uiLocation: UILocation}>}
     */
    breakpointLocationsForUISourceCode: function(uiSourceCode)
    {
        var result = [];
        var breakpoints = /** @type {Array.<BreakpointManager.Breakpoint>} */(this._breakpoints.keys());
        for (var i = 0; i < breakpoints.length; ++i) {
            var breakpoint = breakpoints[i];
            var uiLocations = Object.values(breakpoint._uiLocations);
            for (var j = 0; j < uiLocations.length; ++j) {
                var uiLocation = uiLocations[j];
                if (uiLocation.uiSourceCode === uiSourceCode)
                    result.push({breakpoint: breakpoint, uiLocation: uiLocations[j]});
            }
        }
        return result;
    },

    /**
     * @return {Array.<{breakpoint: BreakpointManager.Breakpoint, uiLocation: UILocation}>}
     */
    allBreakpointLocations: function()
    {
        var result = [];
        var breakpoints = /** @type {Array.<BreakpointManager.Breakpoint>} */(this._breakpoints.keys());
        for (var i = 0; i < breakpoints.length; ++i) {
            var breakpoint = breakpoints[i];
            var uiLocations = Object.values(breakpoint._uiLocations);
            for (var j = 0; j < uiLocations.length; ++j)
                result.push({breakpoint: breakpoint, uiLocation: uiLocations[j]});
        }
        return result;
    },

    /**
     * @param {boolean} toggleState
     */
    toggleAllBreakpoints: function(toggleState)
    {
        var breakpoints = /** @type {Array.<BreakpointManager.Breakpoint>} */(this._breakpoints.keys());
        for (var i = 0; i < breakpoints.length; ++i) {
            var breakpoint = breakpoints[i];
            if (breakpoint.enabled() != toggleState)
                breakpoint.setEnabled(toggleState);
        }
    },

    removeAllBreakpoints: function()
    {
        var breakpoints = /** @type {Array.<BreakpointManager.Breakpoint>} */(this._breakpoints.keys());
        for (var i = 0; i < breakpoints.length; ++i)
            breakpoints[i].remove();
    },

    reset: function()
    {
        // Remove all breakpoints from UI and debugger, do not update storage.
        this._storage._muted = true;
        this.removeAllBreakpoints();
        delete this._storage._muted;

        // Remove all provisional breakpoints from the debugger.
        for (var debuggerId in this._breakpointForDebuggerId)
            this._debuggerModel.removeBreakpoint(debuggerId);
        this._breakpointForDebuggerId = {};
        this._sourceFilesWithRestoredBreakpoints = {};
    },

    _projectWillReset: function(event)
    {
        var project = /** @type {Project} */ (event.data);
        var uiSourceCodes = project.uiSourceCodes();
        for (var i = 0; i < uiSourceCodes.length; ++i) {
            var uiSourceCode = uiSourceCodes[i];
            var breakpoints = this._breakpointsForUISourceCode.get(uiSourceCode) || [];
            for (var lineNumber in breakpoints) {
                var lineBreakpoints = breakpoints[lineNumber];
                for (var j = 0; j < lineBreakpoints.length; ++j) {
                    var breakpoint = lineBreakpoints[j];
                    breakpoint._resetLocations();
                }
            }
            this._breakpointsForUISourceCode.remove(uiSourceCode);

            breakpoints = this.breakpointsForUISourceCode(uiSourceCode);
            for (var j = 0; j < breakpoints.length; ++j) { 
                var breakpoint = breakpoints[j];
                this._breakpoints.remove(breakpoint);
            }

            var sourceFileId = BreakpointManager.sourceFileId(uiSourceCode);
            delete this._sourceFilesWithRestoredBreakpoints[sourceFileId];
        }
    },

    _breakpointResolved: function(event)
    {
        var breakpointId = /** @type {DebuggerAgent.BreakpointId} */ (event.data.breakpointId);
        var location = /** @type {DebuggerModel.Location} */ (event.data.location);
        var breakpoint = this._breakpointForDebuggerId[breakpointId];
        if (!breakpoint)
            return;
        if (!this._breakpoints.contains(breakpoint))
            this._breakpoints.put(breakpoint);
        breakpoint._addResolvedLocation(location);
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     * @param {boolean} removeFromStorage
     */
    _removeBreakpoint: function(breakpoint, removeFromStorage)
    {
        console.assert(!breakpoint._debuggerId)
        this._breakpoints.remove(breakpoint);
        if (removeFromStorage)
            this._storage._removeBreakpoint(breakpoint);
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     * @param {UILocation} uiLocation
     */
    _uiLocationAdded: function(breakpoint, uiLocation)
    {
        var breakpoints = this._breakpointsForUISourceCode.get(uiLocation.uiSourceCode);
        if (!breakpoints) {
            breakpoints = {};
            this._breakpointsForUISourceCode.put(uiLocation.uiSourceCode, breakpoints);
        }

        var lineBreakpoints = breakpoints[uiLocation.lineNumber];
        if (!lineBreakpoints) {
            lineBreakpoints = [];
            breakpoints[uiLocation.lineNumber] = lineBreakpoints;
        }

        lineBreakpoints.push(breakpoint);
        this.dispatchEventToListeners(BreakpointManager.Events.BreakpointAdded, {breakpoint: breakpoint, uiLocation: uiLocation});
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     * @param {UILocation} uiLocation
     */
    _uiLocationRemoved: function(breakpoint, uiLocation)
    {
      var breakpoints = this._breakpointsForUISourceCode.get(uiLocation.uiSourceCode);
        if (!breakpoints)
            return;

        var lineBreakpoints = breakpoints[uiLocation.lineNumber];
        if (!lineBreakpoints)
            return;

        lineBreakpoints.remove(breakpoint);
        if (!lineBreakpoints.length)
            delete breakpoints[uiLocation.lineNumber];
        this.dispatchEventToListeners(BreakpointManager.Events.BreakpointRemoved, {breakpoint: breakpoint, uiLocation: uiLocation});
    },

    __proto__: EventObjectEmitter.prototype
}

/**
 * @constructor
 * @param {BreakpointManager} breakpointManager
 * @param {UISourceCode} uiSourceCode
 * @param {number} lineNumber
 * @param {string} condition
 * @param {boolean} enabled
 */
BreakpointManager.Breakpoint = function(breakpointManager, uiSourceCode, lineNumber, condition, enabled)
{
    this._breakpointManager = breakpointManager;
    this._primaryUILocation = new UILocation(uiSourceCode, lineNumber, 0);
    this._sourceFileId = BreakpointManager.sourceFileId(uiSourceCode);
    /** @type {Array.<Script.Location>} */
    this._liveLocations = [];
    /** @type {!Object.<string, UILocation>} */
    this._uiLocations = {};

    // Force breakpoint update.
    /** @type {string} */ this._condition;
    /** @type {boolean} */ this._enabled;
    this._updateBreakpoint(condition, enabled);
}

BreakpointManager.Breakpoint.prototype = {
    /**
     * @return {UILocation}
     */
    primaryUILocation: function()
    {
        return this._primaryUILocation;
    },

    /**
     * @param {DebuggerModel.Location} location
     */
    _addResolvedLocation: function(location)
    {
        this._liveLocations.push(this._breakpointManager._debuggerModel.createLiveLocation(location, this._locationUpdated.bind(this, location)));
    },

    /**
     * @param {DebuggerModel.Location} location
     * @param {UILocation} uiLocation
     */
    _locationUpdated: function(location, uiLocation)
    {
        var stringifiedLocation = location.scriptId + ":" + location.lineNumber + ":" + location.columnNumber;
        var oldUILocation = /** @type {UILocation} */ (this._uiLocations[stringifiedLocation]);
        if (oldUILocation)
            this._breakpointManager._uiLocationRemoved(this, oldUILocation);
        if (this._uiLocations[""]) {
            delete this._uiLocations[""];
            this._breakpointManager._uiLocationRemoved(this, this._primaryUILocation);
        }
        this._uiLocations[stringifiedLocation] = uiLocation;
        this._breakpointManager._uiLocationAdded(this, uiLocation);
    },

    /**
     * @return {boolean}
     */
    enabled: function()
    {
        return this._enabled;
    },

    /**
     * @param {boolean} enabled
     */
    setEnabled: function(enabled)
    {
        this._updateBreakpoint(this._condition, enabled);
    },

    /**
     * @return {string}
     */
    condition: function()
    {
        return this._condition;
    },

    /**
     * @param {string} condition
     */
    setCondition: function(condition)
    {
        this._updateBreakpoint(condition, this._enabled);
    },

    /**
     * @param {string} condition
     * @param {boolean} enabled
     */
    _updateBreakpoint: function(condition, enabled)
    {
        if (this._enabled === enabled && this._condition === condition)
            return;

        if (this._enabled)
            this._removeFromDebugger();

        this._enabled = enabled;
        this._condition = condition;
        this._breakpointManager._storage._updateBreakpoint(this);

        //x var scriptFile = this._primaryUILocation.uiSourceCode.scriptFile();
        if (this._enabled/*x && !(scriptFile && scriptFile.hasDivergedFromVM())*/) {
            if (this._setInDebugger())
                return;
        }

        this._fakeBreakpointAtPrimaryLocation();
    },

    /**
     * @param {boolean=} keepInStorage
     */
    remove: function(keepInStorage)
    {
        var removeFromStorage = !keepInStorage;
        this._resetLocations();
        this._removeFromDebugger();
        this._breakpointManager._removeBreakpoint(this, removeFromStorage);
    },

    _setInDebugger: function()
    {
        console.assert(!this._debuggerId);
        var rawLocation = this._primaryUILocation.uiLocationToRawLocation();
        var debuggerModelLocation = /** @type {DebuggerModel.Location} */ (rawLocation);
        if (debuggerModelLocation) {
            this._breakpointManager._debuggerModel.setBreakpointByScriptLocation(debuggerModelLocation, this._condition, this._didSetBreakpointInDebugger.bind(this));
            return true;
        }
        if (this._primaryUILocation.uiSourceCode.url) {
            this._breakpointManager._debuggerModel.setBreakpointByURL(this._primaryUILocation.uiSourceCode.url, this._primaryUILocation.lineNumber, 0, this._condition, this._didSetBreakpointInDebugger.bind(this));
            return true;
        }
        return false;
    },

    /**
    * @this {BreakpointManager.Breakpoint}
    * @param {?DebuggerAgent.BreakpointId} breakpointId
    * @param {Array.<DebuggerModel.Location>} locations
    */
    _didSetBreakpointInDebugger: function(breakpointId, locations)
    {
        if (!breakpointId) {
            this._resetLocations();
            this._breakpointManager._removeBreakpoint(this, false);
            return;
        }

        this._debuggerId = breakpointId;
        this._breakpointManager._breakpointForDebuggerId[breakpointId] = this;

        if (!locations.length) {
            this._fakeBreakpointAtPrimaryLocation();
            return;
        }

        this._resetLocations();
        for (var i = 0; i < locations.length; ++i) {
            var script = this._breakpointManager._debuggerModel.scriptForId(locations[i].scriptId);
            var uiLocation = script.rawLocationToUILocation(locations[i].lineNumber, locations[i].columnNumber);
            if (this._breakpointManager.findBreakpoint(uiLocation.uiSourceCode, uiLocation.lineNumber)) {
                // location clash
                this.remove();
                return;
            }
        }

        for (var i = 0; i < locations.length; ++i)
            this._addResolvedLocation(locations[i]);
    },

    _removeFromDebugger: function()
    {
        if (this._debuggerId) {
            this._breakpointManager._debuggerModel.removeBreakpoint(this._debuggerId);
            delete this._breakpointManager._breakpointForDebuggerId[this._debuggerId];
            delete this._debuggerId;
        }
    },

    _resetLocations: function()
    {
        for (var stringifiedLocation in this._uiLocations)
            this._breakpointManager._uiLocationRemoved(this, this._uiLocations[stringifiedLocation]);

        for (var i = 0; i < this._liveLocations.length; ++i)
            this._liveLocations[i].dispose();
        this._liveLocations = [];

        this._uiLocations = {};
    },

    /**
     * @return {string}
     */
    _breakpointStorageId: function()
    {
        if (!this._sourceFileId)
            return "";
        return this._sourceFileId + ":" + this._primaryUILocation.lineNumber;
    },

    _fakeBreakpointAtPrimaryLocation: function()
    {
        this._resetLocations();
        this._uiLocations[""] = this._primaryUILocation;
        this._breakpointManager._uiLocationAdded(this, this._primaryUILocation);
    }
}

/**
 * @constructor
 * @param {BreakpointManager} breakpointManager
 * @param {Setting} setting
 */
BreakpointManager.Storage = function(breakpointManager, setting)
{
    this._breakpointManager = breakpointManager;
    this._setting = setting;
    var breakpoints = this._setting.get();
    /** @type {Object.<string,BreakpointManager.Storage.Item>} */
    this._breakpoints = {};
    for (var i = 0; i < breakpoints.length; ++i) {
        var breakpoint = /** @type {BreakpointManager.Storage.Item} */ (breakpoints[i]);
        this._breakpoints[breakpoint.sourceFileId + ":" + breakpoint.lineNumber] = breakpoint;
    }
}

BreakpointManager.Storage.prototype = {
    /**
     * @param {UISourceCode} uiSourceCode
     */
    _restoreBreakpoints: function(uiSourceCode)
    {
        this._muted = true;
        var sourceFileId = BreakpointManager.sourceFileId(uiSourceCode);
        for (var id in this._breakpoints) {
            var breakpoint = this._breakpoints[id];
            if (breakpoint.sourceFileId === sourceFileId)
                this._breakpointManager._innerSetBreakpoint(uiSourceCode, breakpoint.lineNumber, breakpoint.condition, breakpoint.enabled);
        }
        delete this._muted;
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     */
    _updateBreakpoint: function(breakpoint)
    {
        if (this._muted || !breakpoint._breakpointStorageId())
            return;
        this._breakpoints[breakpoint._breakpointStorageId()] = new BreakpointManager.Storage.Item(breakpoint);
        this._save();
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     */
    _removeBreakpoint: function(breakpoint)
    {
        if (this._muted)
            return;
        delete this._breakpoints[breakpoint._breakpointStorageId()];
        this._save();
    },

    _save: function()
    {
        var breakpointsArray = [];
        for (var id in this._breakpoints)
            breakpointsArray.push(this._breakpoints[id]);
        this._setting.set(breakpointsArray);
    }
}

/**
 * @constructor
 * @param {BreakpointManager.Breakpoint} breakpoint
 */
BreakpointManager.Storage.Item = function(breakpoint)
{
    var primaryUILocation = breakpoint.primaryUILocation();
    this.sourceFileId = breakpoint._sourceFileId;
    this.lineNumber = primaryUILocation.lineNumber;
    this.condition = breakpoint.condition();
    this.enabled = breakpoint.enabled();
}

/** @type {BreakpointManager} */
//CSInspector.breakpointManager = null;

return BreakpointManager;
});
