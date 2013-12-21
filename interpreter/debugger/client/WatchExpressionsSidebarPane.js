/*
 * Copyright (C) IBM Corp. 2009  All rights reserved.
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
 *     * Neither the name of IBM Corp. nor the names of its
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
/**
 *被监视的表达式每次刷新都是刷新所有的
 *每次刷新都会带上所有监视的表达式
 *每次刷新前都会先请求释放先前的watch-group(对于cs来说不需要，没有那么多复杂的情况)
 *表达式返回值的属性需要单独请求获取，因此刷新结果被展开的表达式要分两步完成
 *删除一个监视表达式是通过刷新实现的，只不过刷新时少了一个表达式而已
 */

define(function(require){

var SidebarPane = require("SidebarPane").SidebarPane;
var ObjectPropertiesSection = require("ObjectPropertiesSection").ObjectPropertiesSection;
var ObjectPropertyTreeElement = require("ObjectPropertiesSection").ObjectPropertyTreeElement;
var RemoteObject = require("RemoteObject").RemoteObject;
/**
 * @constructor
 * @extends {SidebarPane}
 */
function WatchExpressionsSidebarPane()
{
    SidebarPane.call(this, UIString("Watch Expressions"));

    this.section = new WatchExpressionsSection();
    this.bodyElement.appendChild(this.section.element);

    var refreshButton = document.createElement("button");
    refreshButton.className = "pane-title-button refresh";
    refreshButton.addEventListener("click", this._refreshButtonClicked.bind(this), false);
    refreshButton.title = UIString("Refresh");
    this.titleElement.appendChild(refreshButton);

    var addButton = document.createElement("button");
    addButton.className = "pane-title-button add";
    addButton.addEventListener("click", this._addButtonClicked.bind(this), false);
    this.titleElement.appendChild(addButton);
    addButton.title = UIString("Add watch expression");

    this._requiresUpdate = true;
}

WatchExpressionsSidebarPane.prototype = {
    wasShown: function()
    {
        this._refreshExpressionsIfNeeded();
    },

    reset: function()
    {
        this.refreshExpressions();
    },

    refreshExpressions: function()
    {
        this._requiresUpdate = true;
        this._refreshExpressionsIfNeeded();
    },

    addExpression: function(expression)
    {
        this.section.addExpression(expression);
        this.expand();
    },

    _refreshExpressionsIfNeeded: function()
    {
        if (this._requiresUpdate && this.isShowing()) {
            this.section.update();
            delete this._requiresUpdate;
        } else
            this._requiresUpdate = true;
    },

    _addButtonClicked: function(event)
    {
        event.consume();
        this.expand();
        this.section.addNewExpressionAndEdit();
    },

    _refreshButtonClicked: function(event)
    {
        event.consume();
        this.refreshExpressions();
    },

    __proto__: SidebarPane.prototype
}

/**
 * @constructor
 * @extends {ObjectPropertiesSection}
 */
function WatchExpressionsSection()
{
    this._watchObjectGroupId = "watch-group";

    ObjectPropertiesSection.call(this, RemoteObject.fromPrimitiveValue(""));

    this.treeElementConstructor = WatchedPropertyTreeElement;
    this._expandedExpressions = {};
    this._expandedProperties = {};

    this.emptyElement = document.createElement("div");
    this.emptyElement.className = "info";
    this.emptyElement.textContent = UIString("No Watch Expressions");

    this.watchExpressions = CSInspector.settings.watchExpressions.get();

    this.headerElement.className = "hidden";
    this.editable = true;
    this.expanded = true;
    this.propertiesElement.addStyleClass("watch-expressions");

    this.element.addEventListener("mousemove", this._mouseMove.bind(this), true);
    this.element.addEventListener("mouseout", this._mouseOut.bind(this), true);
    this.element.addEventListener("dblclick", this._sectionDoubleClick.bind(this), false);
    //x this.emptyElement.addEventListener("contextmenu", this._emptyElementContextMenu.bind(this), false);
}

// Patch the expression used as an initial value for a new watch.
// DevTools' value "\n" breaks the debugger protocol.
WatchExpressionsSection.NewWatchExpression = "''";
//WatchExpressionsSection.NewWatchExpression = "\xA0";

WatchExpressionsSection.prototype = {
    update: function(e)
    {
        if (e)
            e.consume();

        function appendResult(expression, watchIndex, result, wasThrown)
        {
            if (!result)
                return;

            var property = new RemoteObjectProperty(expression, result);
            property.watchIndex = watchIndex;
            property.wasThrown = wasThrown;

            // To clarify what's going on here:
            // In the outer function, we calculate the number of properties
            // that we're going to be updating, and set that in the
            // propertyCount variable.
            // In this function, we test to see when we are processing the
            // last property, and then call the superclass's updateProperties()
            // method to get all the properties refreshed at once.
            properties.push(property);

            if (properties.length == propertyCount) {
                this.updateProperties(properties, [], WatchExpressionTreeElement, WatchExpressionsSection.CompareProperties);

                // check to see if we just added a new watch expression,
                // which will always be the last property
                if (this._newExpressionAdded) {
                    delete this._newExpressionAdded;

                    var treeElement = this.findAddedTreeElement();
                    if (treeElement)
                        treeElement.startEditing();
                }

                // Force displaying delete button for hovered element.
                if (this._lastMouseMovePageY)
                    this._updateHoveredElement(this._lastMouseMovePageY);
            }
        }

        // TODO: pass exact injected script id.
        //x RuntimeAgent.releaseObjectGroup(this._watchObjectGroupId)
        var properties = [];

        // Count the properties, so we known when to call this.updateProperties()
        // in appendResult()
        var propertyCount = 0;
        for (var i = 0; i < this.watchExpressions.length; ++i) {
            if (!this.watchExpressions[i])
                continue;
            ++propertyCount;
        }

        // Now process all the expressions, since we have the actual count,
        // which is checked in the appendResult inner function.
        for (var i = 0; i < this.watchExpressions.length; ++i) {
            var expression = this.watchExpressions[i];
            if (!expression)
                continue;

            //WebInspector.runtimeModel.evaluate(expression, this._watchObjectGroupId, false, true, false, false, appendResult.bind(this, expression, i));

            CSInspector.debugAgent.evaluate(expression, this._watchObjectGroupId, false, true, false, false, appendResult.bind(this, expression, i));
        }

        if (!propertyCount) {
            if (!this.emptyElement.parentNode)
                this.element.appendChild(this.emptyElement);
        } else {
            if (this.emptyElement.parentNode)
                this.element.removeChild(this.emptyElement);
        }

        // note this is setting the expansion of the tree, not the section;
        // with no expressions, and expanded tree, we get some extra vertical
        // white space
        this.expanded = (propertyCount != 0);
    },

    addExpression: function(expression)
    {
        this.watchExpressions.push(expression);
        this.saveExpressions();
        this.update();
    },

    addNewExpressionAndEdit: function()
    {
        this._newExpressionAdded = true;
        this.watchExpressions.push(WatchExpressionsSection.NewWatchExpression);
        this.update();
    },

    _sectionDoubleClick: function(event)
    {
        if (event.target !== this.element && event.target !== this.propertiesElement && event.target !== this.emptyElement)
            return;
        event.consume();
        this.addNewExpressionAndEdit();
    },

    updateExpression: function(element, value)
    {
        if (value === null) {
            var index = element.property.watchIndex;
            this.watchExpressions.splice(index, 1);
        }
        else
            this.watchExpressions[element.property.watchIndex] = value;
        this.saveExpressions();
        this.update();
    },

    _deleteAllExpressions: function()
    {
        this.watchExpressions = [];
        this.saveExpressions();
        this.update();
    },

    findAddedTreeElement: function()
    {
        var children = this.propertiesTreeOutline.children;
        for (var i = 0; i < children.length; ++i) {
            if (children[i].property.name === WatchExpressionsSection.NewWatchExpression)
                return children[i];
        }
    },

    saveExpressions: function()
    {
        var toSave = [];
        for (var i = 0; i < this.watchExpressions.length; i++)
            if (this.watchExpressions[i])
                toSave.push(this.watchExpressions[i]);

        CSInspector.settings.watchExpressions.set(toSave);
        return toSave.length;
    },

    _mouseMove: function(e)
    {
        if (this.propertiesElement.firstChild)
            this._updateHoveredElement(e.pageY);
    },

    _mouseOut: function()
    {
        if (this._hoveredElement) {
            this._hoveredElement.removeStyleClass("hovered");
            delete this._hoveredElement;
        }
        delete this._lastMouseMovePageY;
    },

    _updateHoveredElement: function(pageY)
    {
        var candidateElement = this.propertiesElement.firstChild;
        while (true) {
            var next = candidateElement.nextSibling;
            while (next && !next.clientHeight)
                next = next.nextSibling;
            if (!next || next.totalOffsetTop() > pageY)
                break;
            candidateElement = next;
        }

        if (this._hoveredElement !== candidateElement) {
            if (this._hoveredElement)
                this._hoveredElement.removeStyleClass("hovered");
            if (candidateElement)
                candidateElement.addStyleClass("hovered");
            this._hoveredElement = candidateElement;
        }

        this._lastMouseMovePageY = pageY;
    },

    _emptyElementContextMenu: function(event)
    {
        var contextMenu = new ContextMenu(event);
        contextMenu.appendItem(UIString(0 ? "Add watch expression" : "Add Watch Expression"), this.addNewExpressionAndEdit.bind(this));
        contextMenu.show();
    },

    __proto__: ObjectPropertiesSection.prototype
}

WatchExpressionsSection.CompareProperties = function(propertyA, propertyB)
{
    if (propertyA.watchIndex == propertyB.watchIndex)
        return 0;
    else if (propertyA.watchIndex < propertyB.watchIndex)
        return -1;
    else
        return 1;
}

/**
 * @constructor
 * @extends {ObjectPropertyTreeElement}
 * @param {RemoteObjectProperty} property
 */
function WatchExpressionTreeElement(property)
{
    ObjectPropertyTreeElement.call(this, property);
}

WatchExpressionTreeElement.prototype = {
    onexpand: function()
    {
        ObjectPropertyTreeElement.prototype.onexpand.call(this);
        this.treeOutline.section._expandedExpressions[this._expression()] = true;
    },

    oncollapse: function()
    {
        ObjectPropertyTreeElement.prototype.oncollapse.call(this);
        delete this.treeOutline.section._expandedExpressions[this._expression()];
    },

    onattach: function()
    {
        ObjectPropertyTreeElement.prototype.onattach.call(this);
        if (this.treeOutline.section._expandedExpressions[this._expression()])
            this.expanded = true;
    },

    _expression: function()
    {
        return this.property.name;
    },

    update: function()
    {
        ObjectPropertyTreeElement.prototype.update.call(this);

        if (this.property.wasThrown) {
            this.valueElement.textContent = UIString("<not available>");
            this.listItemElement.addStyleClass("dimmed");
        } else
            this.listItemElement.removeStyleClass("dimmed");

        var deleteButton = document.createElement("input");
        deleteButton.type = "button";
        deleteButton.title = UIString("Delete watch expression.");
        deleteButton.addStyleClass("enabled-button");
        deleteButton.addStyleClass("delete-button");
        deleteButton.addEventListener("click", this._deleteButtonClicked.bind(this), false);
        this.listItemElement.addEventListener("contextmenu", this._contextMenu.bind(this), false);
        this.listItemElement.insertBefore(deleteButton, this.listItemElement.firstChild);
    },

    /**
     * @param {ContextMenu} contextMenu
     * @override
     */
    populateContextMenu: function(contextMenu)
    {
        if (!this.isEditing()) {
            contextMenu.appendItem(UIString(CSInspector.useLowerCaseMenuTitles() ? "Add watch expression" : "Add Watch Expression"), this.treeOutline.section.addNewExpressionAndEdit.bind(this.treeOutline.section));
            contextMenu.appendItem(UIString(CSInspector.useLowerCaseMenuTitles() ? "Delete watch expression" : "Delete Watch Expression"), this._deleteButtonClicked.bind(this));
        }
        if (this.treeOutline.section.watchExpressions.length > 1)
            contextMenu.appendItem(UIString(CSInspector.useLowerCaseMenuTitles() ? "Delete all watch expressions" : "Delete All Watch Expressions"), this._deleteAllButtonClicked.bind(this));
    },

    _contextMenu: function(event)
    {
        var contextMenu = new ContextMenu(event);
        this.populateContextMenu(contextMenu);
        contextMenu.show();
    },

    _deleteAllButtonClicked: function()
    {
        this.treeOutline.section._deleteAllExpressions();
    },

    _deleteButtonClicked: function()
    {
        this.treeOutline.section.updateExpression(this, null);
    },

    renderPromptAsBlock: function()
    {
        return true;
    },

    /**
     * @param {Event=} event
     */
    elementAndValueToEdit: function(event)
    {
        return [this.nameElement, this.property.name.trim()];
    },

    editingCancelled: function(element, context)
    {
        if (!context.elementToEdit.textContent)
            this.treeOutline.section.updateExpression(this, null);

        ObjectPropertyTreeElement.prototype.editingCancelled.call(this, element, context);
    },

    applyExpression: function(expression, updateInterface)
    {
        expression = expression.trim();

        if (!expression)
            expression = null;

        this.property.name = expression;
        this.treeOutline.section.updateExpression(this, expression);
    },

    __proto__: ObjectPropertyTreeElement.prototype
}


/**
 * @constructor
 * @extends {ObjectPropertyTreeElement}
 * @param {RemoteObjectProperty} property
 */
function WatchedPropertyTreeElement(property)
{
    ObjectPropertyTreeElement.call(this, property);
}

WatchedPropertyTreeElement.prototype = {
    onattach: function()
    {
        ObjectPropertyTreeElement.prototype.onattach.call(this);
        if (this.hasChildren && this.propertyPath() in this.treeOutline.section._expandedProperties)
            this.expand();
    },

    onexpand: function()
    {
        ObjectPropertyTreeElement.prototype.onexpand.call(this);
        this.treeOutline.section._expandedProperties[this.propertyPath()] = true;
    },

    oncollapse: function()
    {
        ObjectPropertyTreeElement.prototype.oncollapse.call(this);
        delete this.treeOutline.section._expandedProperties[this.propertyPath()];
    },

    __proto__: ObjectPropertyTreeElement.prototype
}

return WatchExpressionsSidebarPane;
});
