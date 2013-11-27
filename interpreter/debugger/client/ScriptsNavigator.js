define(function(require){
    var EventObjectEmitter = require("events").EventObjectEmitter;
    var TabbedPane = require("TabbedPane");
    var NavigatorView = require("NavigatorView");

    /**
     * @extends {EventObjectEmitter}
     * @constructor
     */
    function ScriptsNavigator()
    {
        EventObjectEmitter.call(this);

        this._tabbedPane = new TabbedPane();
        this._tabbedPane.shrinkableTabs = true;
        this._tabbedPane.element.addStyleClass("navigator-tabbed-pane");

        this._scriptsView = new NavigatorView();
        this._scriptsView.addEventListener(NavigatorView.Events.ItemSelected, this._scriptSelected, this);
        this._scriptsView.addEventListener(NavigatorView.Events.ItemSearchStarted, this._itemSearchStarted, this);

        this._tabbedPane.appendTab(ScriptsNavigator.ScriptsTab, UIString("Sources"), this._scriptsView);
    }

    ScriptsNavigator.Events = {
        ScriptSelected: "ScriptSelected",
        ItemSearchStarted: "ItemSearchStarted",
    }

    ScriptsNavigator.ScriptsTab = "scripts";

    ScriptsNavigator.prototype = {
        /*
         * @return {View}
         */
        get view()
        {
            return this._tabbedPane;
        },

        /**
         * @param {UISourceCode} uiSourceCode
         */
        _navigatorViewForUISourceCode: function(uiSourceCode)
        {
            return this._scriptsView;
        },

        /**
         * @param {UISourceCode} uiSourceCode
         */
        addUISourceCode: function(uiSourceCode)
        {
            this._navigatorViewForUISourceCode(uiSourceCode).addUISourceCode(uiSourceCode);
        },

        /**
         * @param {UISourceCode} uiSourceCode
         */
        removeUISourceCode: function(uiSourceCode)
        {
            this._navigatorViewForUISourceCode(uiSourceCode).removeUISourceCode(uiSourceCode);
        },

        /**
         * @param {UISourceCode} uiSourceCode
         * @param {boolean=} select
         */
        revealUISourceCode: function(uiSourceCode, select)
        {
            this._navigatorViewForUISourceCode(uiSourceCode).revealUISourceCode(uiSourceCode, select);
        },

        /**
         * @param {UISourceCode} uiSourceCode
         * @param {function(boolean)=} callback
         */
        rename: function(uiSourceCode, callback)
        {
            this._navigatorViewForUISourceCode(uiSourceCode).rename(uiSourceCode, callback);
        },

        /**
         * @param {Event} event
         */
        _scriptSelected: function(event)
        {
            this.dispatchEventToListeners(ScriptsNavigator.Events.ScriptSelected, event.data);
        },

        /**
         * @param {Event} event
         */
        _itemSearchStarted: function(event)
        {
            this.dispatchEventToListeners(ScriptsNavigator.Events.ItemSearchStarted, event.data);
        },

        __proto__: EventObjectEmitter.prototype
    }
    return ScriptsNavigator;
});
