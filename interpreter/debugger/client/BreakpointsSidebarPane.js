define(function (require, exports) {

/*
 * Copyright (C) 2008 Apple Inc. All Rights Reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY APPLE INC. ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL APPLE INC. OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
 * OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

//require("breakpointsList.css");

var SidebarPane = require("SidebarPane").SidebarPane;
var BreakpointManager = require("BreakpointManager");

/**
 * @constructor
 * @param {BreakpointManager} breakpointManager
 * @extends {SidebarPane}
 */
function BreakpointsSidebarPane(breakpointManager, showSourceLineDelegate)
{
    SidebarPane.call(this, UIString("Breakpoints"));

    this._breakpointManager = breakpointManager;
    this._showSourceLineDelegate = showSourceLineDelegate;

    this.listElement = document.createElement("ol");
    this.listElement.className = "breakpoint-list";

    this.emptyElement = document.createElement("div");
    this.emptyElement.className = "info";
    this.emptyElement.textContent = UIString("No Breakpoints");

    this.bodyElement.appendChild(this.emptyElement);

    this._items = new Map();

    var breakpointLocations = this._breakpointManager.allBreakpointLocations();
    for (var i = 0; i < breakpointLocations.length; ++i)
        this._addBreakpoint(breakpointLocations[i].breakpoint, breakpointLocations[i].uiLocation);

    this._breakpointManager.addEventListener(BreakpointManager.Events.BreakpointAdded, this._breakpointAdded, this);
    this._breakpointManager.addEventListener(BreakpointManager.Events.BreakpointRemoved, this._breakpointRemoved, this);

    //this.emptyElement.addEventListener("contextmenu", this._emptyElementContextMenu.bind(this), true);
}

BreakpointsSidebarPane.prototype = {
    _emptyElementContextMenu: function(event)
    {
        var contextMenu = new ContextMenu(event);
        var breakpointActive = CSInspector.debugModel.breakpointsActive();
        var breakpointActiveTitle = breakpointActive ?
            UIString(useLowerCaseMenuTitles() ? "Deactivate breakpoints" : "Deactivate Breakpoints") :
            UIString(useLowerCaseMenuTitles() ? "Activate breakpoints" : "Activate Breakpoints");
        contextMenu.appendItem(breakpointActiveTitle, CSInspector.debuggerModel.setBreakpointsActive.bind(CSInspector.debugModel, !breakpointActive));
        contextMenu.show();
    },

    /**
     * @param {Event} event
     */
    _breakpointAdded: function(event)
    {
        this._breakpointRemoved(event);

        var breakpoint = /** @type {BreakpointManager.Breakpoint} */ (event.data.breakpoint);
        var uiLocation = /** @type {UILocation} */ (event.data.uiLocation);
        this._addBreakpoint(breakpoint, uiLocation);
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     * @param {UILocation} uiLocation
     */
    _addBreakpoint: function(breakpoint, uiLocation)
    {
        var element = document.createElement("li");
        element.addStyleClass("cursor-pointer");
        //x element.addEventListener("contextmenu", this._breakpointContextMenu.bind(this, breakpoint), true);
        element.addEventListener("click", this._breakpointClicked.bind(this, uiLocation), false);

        var checkbox = document.createElement("input");
        checkbox.className = "checkbox-elem";
        checkbox.type = "checkbox";
        checkbox.checked = breakpoint.enabled();
        checkbox.addEventListener("click", this._breakpointCheckboxClicked.bind(this, breakpoint), false);
        element.appendChild(checkbox);

        var labelElement = document.createTextNode(uiLocation.linkText());
        element.appendChild(labelElement);

        var snippetElement = document.createElement("div");
        snippetElement.className = "source-text monospace";
        element.appendChild(snippetElement);

        /**
         * @param {?string} content
         * @param {boolean} contentEncoded
         * @param {string} mimeType
         */
        function didRequestContent(content, contentEncoded, mimeType)
        {
            var lineEndings = content.lineEndings();
            if (uiLocation.lineNumber < lineEndings.length)
                snippetElement.textContent = content.substring(lineEndings[uiLocation.lineNumber - 1], lineEndings[uiLocation.lineNumber]);
        }
        uiLocation.uiSourceCode.requestContent(didRequestContent.bind(this));

        element._data = uiLocation;
        var currentElement = this.listElement.firstChild;
        while (currentElement) {
            if (currentElement._data && this._compareBreakpoints(currentElement._data, element._data) > 0)
                break;
            currentElement = currentElement.nextSibling;
        }
        this._addListElement(element, currentElement);

        var breakpointItem = {};
        breakpointItem.element = element;
        breakpointItem.checkbox = checkbox;
        this._items.put(breakpoint, breakpointItem);

        this.expand();
    },

    /**
     * @param {Event} event
     */
    _breakpointRemoved: function(event)
    {
        var breakpoint = /** @type {BreakpointManager.Breakpoint} */ (event.data.breakpoint);
        var uiLocation = /** @type {UILocation} */ (event.data.uiLocation);
        var breakpointItem = this._items.get(breakpoint);
        if (!breakpointItem)
            return;
        this._items.remove(breakpoint);
        this._removeListElement(breakpointItem.element);
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     */
    highlightBreakpoint: function(breakpoint)
    {
        var breakpointItem = this._items.get(breakpoint);
        if (!breakpointItem)
            return;
        breakpointItem.element.addStyleClass("breakpoint-hit");
        this._highlightedBreakpointItem = breakpointItem;
    },

    clearBreakpointHighlight: function()
    {
        if (this._highlightedBreakpointItem) {
            this._highlightedBreakpointItem.element.removeStyleClass("breakpoint-hit");
            delete this._highlightedBreakpointItem;
        }
    },

    _breakpointClicked: function(uiLocation, event)
    {
        this._showSourceLineDelegate(uiLocation.uiSourceCode, uiLocation.lineNumber);
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     */
    _breakpointCheckboxClicked: function(breakpoint, event)
    {
        // Breakpoint element has it's own click handler.
        event.consume();
        breakpoint.setEnabled(event.target.checked);
    },

    /**
     * @param {BreakpointManager.Breakpoint} breakpoint
     */
    _breakpointContextMenu: function(breakpoint, event)
    {
        var breakpoints = this._items.values();
        var contextMenu = new ContextMenu(event);
        contextMenu.appendItem(UIString(useLowerCaseMenuTitles() ? "Remove breakpoint" : "Remove Breakpoint"), breakpoint.remove.bind(breakpoint));
        if (breakpoints.length > 1) {
            var removeAllTitle = UIString(useLowerCaseMenuTitles() ? "Remove all breakpoints" : "Remove All Breakpoints");
            contextMenu.appendItem(removeAllTitle, this._breakpointManager.removeAllBreakpoints.bind(this._breakpointManager));
        }

        contextMenu.appendSeparator();
        var breakpointActive = CSInspector.debuggerModel.breakpointsActive();
        var breakpointActiveTitle = breakpointActive ?
            UIString(useLowerCaseMenuTitles() ? "Deactivate breakpoints" : "Deactivate Breakpoints") :
            UIString(useLowerCaseMenuTitles() ? "Activate breakpoints" : "Activate Breakpoints");
        contextMenu.appendItem(breakpointActiveTitle, CSInspector.debugModel.setBreakpointsActive.bind(CSInspector.debugModel, !breakpointActive));

        function enabledBreakpointCount(breakpoints)
        {
            var count = 0;
            for (var i = 0; i < breakpoints.length; ++i) {
                if (breakpoints[i].checkbox.checked)
                    count++;
            }
            return count;
        }
        if (breakpoints.length > 1) {
            var enableBreakpointCount = enabledBreakpointCount(breakpoints);
            var enableTitle = UIString(useLowerCaseMenuTitles() ? "Enable all breakpoints" : "Enable All Breakpoints");
            var disableTitle = UIString(useLowerCaseMenuTitles() ? "Disable all breakpoints" : "Disable All Breakpoints");

            contextMenu.appendSeparator();

            contextMenu.appendItem(enableTitle, this._breakpointManager.toggleAllBreakpoints.bind(this._breakpointManager, true), !(enableBreakpointCount != breakpoints.length));
            contextMenu.appendItem(disableTitle, this._breakpointManager.toggleAllBreakpoints.bind(this._breakpointManager, false), !(enableBreakpointCount > 1));
        }

        contextMenu.show();
    },

    _addListElement: function(element, beforeElement)
    {
        if (beforeElement)
            this.listElement.insertBefore(element, beforeElement);
        else {
            if (!this.listElement.firstChild) {
                this.bodyElement.removeChild(this.emptyElement);
                this.bodyElement.appendChild(this.listElement);
            }
            this.listElement.appendChild(element);
        }
    },

    _removeListElement: function(element)
    {
        this.listElement.removeChild(element);
        if (!this.listElement.firstChild) {
            this.bodyElement.removeChild(this.listElement);
            this.bodyElement.appendChild(this.emptyElement);
        }
    },

    _compare: function(x, y)
    {
        if (x !== y)
            return x < y ? -1 : 1;
        return 0;
    },

    _compareBreakpoints: function(b1, b2)
    {
        return this._compare(b1.uiSourceCode.originURL(), b2.uiSourceCode.originURL()) || this._compare(b1.lineNumber, b2.lineNumber);
    },

    reset: function()
    {
        this.listElement.removeChildren();
        if (this.listElement.parentElement) {
            this.bodyElement.removeChild(this.listElement);
            this.bodyElement.appendChild(this.emptyElement);
        }
        this._items.clear();
    },

    __proto__: SidebarPane.prototype
}


return BreakpointsSidebarPane;
});
