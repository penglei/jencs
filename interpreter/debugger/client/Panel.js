define(function(require, exports){
    var View = require("View");
    var inherits = require("util").inherits;
    var SidebarView = require("SidebarView");

    var KeyboardShortcut = require("KeyboardShortcut");

    function Panel(name){
        View.call(this);

        this.name = name;
        this.element.addStyleClass("panel");
        this.element.addStyleClass(name);

        this._shortcuts = /** !Object.<number, function(Event=):boolean> */ ({});
    }

    inherits(Panel, View);

    Panel.prototype.createSidebarView = function(parentElement, position, defaultWidth, defaultHeight) {
        if (this.splitView)
            return;

        if (!parentElement)
            parentElement = this.element;

        this.splitView = new SidebarView(position, this._sidebarWidthSettingName(), defaultWidth, defaultHeight);
        this.splitView.show(parentElement);
        this.splitView.addEventListener(SidebarView.Events.Resized, this.sidebarResized.bind(this));

        this.sidebarElement = this.splitView.sidebarElement;
    }

    Panel.prototype._sidebarWidthSettingName = function() {
        return this._panelName + "SidebarWidth";
    }


    // Should be implemented by ancestors.

    Panel.prototype.getStatusBarItems = function(event)
    {
    }

    /**
     * @param {Event} event
     */
    Panel.prototype.sidebarResized = function(event)
    {
    }

    Panel.prototype.statusBarResized = function()
    {
    }


    Panel.prototype.show = function()
    {
        View.prototype.show.call(this, CSInspector.inspectorView.panelsElement());
    }

    Panel.prototype.wasShown = function()
    {
        var panelStatusBar = document.getElementById("panel-status-bar")
        var drawerViewAnchor = document.getElementById("drawer-view-anchor");
        var statusBarItems = this.getStatusBarItems();
        if (statusBarItems) {
            this._statusBarItemContainer = document.createElement("div");
            for (var i = 0; i < statusBarItems.length; ++i)
                this._statusBarItemContainer.appendChild(statusBarItems[i]);
            panelStatusBar.insertBefore(this._statusBarItemContainer, drawerViewAnchor);
        }
        var statusBarText = this.statusBarText();
        if (statusBarText) {
            this._statusBarTextElement = statusBarText;
            panelStatusBar.appendChild(statusBarText);
        }

        this.focus();
    }

    Panel.prototype.willHide = function()
    {
        if (this._statusBarItemContainer)
            this._statusBarItemContainer.remove();
        delete this._statusBarItemContainer;

        if (this._statusBarTextElement)
            this._statusBarTextElement.remove();
        delete this._statusBarTextElement;
    }

    Panel.prototype.reset = function()
    {
        this.searchCanceled();
    }

    Panel.prototype.defaultFocusedElement = function()
    {
        return this.sidebarTreeElement || this.element;
    }



    /**
     * @param {KeyboardEvent} event
     */
    Panel.prototype.handleShortcut = function(event)
    {
        var shortcutKey = KeyboardShortcut.makeKeyFromEvent(event);
        var handler = this._shortcuts[shortcutKey];
        if (handler && handler(event))
            event.handled = true;
    }

    /**
     * @param {!Array.<!KeyboardShortcut.Descriptor>} keys
     * @param {function(Event=):boolean} handler
     */
    Panel.prototype.registerShortcuts = function(keys, handler)
    {
        for (var i = 0; i < keys.length; ++i)
            this._shortcuts[keys[i].key] = handler;
    }


    /**
     * @constructor
     * @param {string} name
     * @param {string} title
     * @param {string=} className
     * @param {string=} scriptName
     * @param {Panel=} panel
     */
    function PanelDescriptor(name, title, className, scriptName, panel)
    {
        this._name = name;
        this._title = title;
        this._className = className;
        this._scriptName = scriptName;
        this._panel = panel;
    }

    PanelDescriptor.prototype = {
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
         * @return {Panel}
         */
        panel: function()
        {
            /*
            if (this._panel)
                return this._panel;
            if (this._scriptName)
                loadScript(this._scriptName);
            this._panel = new WebInspector[this._className];
            */
            return this._panel;
        },

        registerShortcuts: function() {}
    }

    exports.Panel = Panel;
    exports.PanelDescriptor = PanelDescriptor;
});
