define(function(require, exports){
    var View = require("View");

    function InspectorView(){
        View.call(this);
        this.markAsRoot();

        this._panelsElement = this.element.createChild("div", "fill");

        this._panels = {};
        this._panelOrder = [];

        this._currentPanel = null;

        this._footerElementContainer = this.element.createChild("div", "inspector-footer status-bar hidden");
    }

    InspectorView.prototype = {

        /**
         * @param {Panel} x
         */
        setCurrentPanel: function(x)
        {
            if (this._currentPanel === x)
                return;

            // FIXME: remove search controller.
            //WebInspector.searchController.cancelSearch();

            if (this._currentPanel)
                this._currentPanel.detach();

            this._currentPanel = x;

            if (x) {
                x.show();
                this.dispatchEventToListeners(InspectorView.Events.PanelSelected);
            }
            for (var panelName in CSInspector.panels) {
                if (CSInspector.panels[panelName] === x) {
                    CSInspector.settings.lastActivePanel.set(panelName);
                    this._pushToHistory(panelName);
                    //x没什么用 CSInspector.userMetrics.panelShown(panelName);
                }
            }
        },

        __proto__:View.prototype
    };

    InspectorView.Events = {
        PanelSelected: "PanelSelected"
    }

    InspectorView.prototype.addPanel = function(panel) {
        this._panelOrder.push(panel.name);
        this._panels[panel.name] = panel;
    }

    InspectorView.prototype.showPanel = function(panelName){
        var panel = this._panels[panelName];
        if (panel) {
            this._currentPanel = panel;
            panel.show();
            this.dispatchEventToListeners(InspectorView.Events.PanelSelected);
        }
    }

    InspectorView.prototype.getCurrentPanel = function (argument) {
        return this._currentPanel;
    }

    InspectorView.prototype.panelsElement = function(){
        return this._panelsElement;
    }

    return InspectorView;
});
