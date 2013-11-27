define(function(require, exports){
    var View = require("View");
    var inherits = require("util").inherits;

    function MainView(){
        View.call(this);
        this.markAsRoot();

        this._panelsElement = this.element.createChild("div", "fill");

        this._panels = {};
        this._panelOrder = [];

        this._currentPanel = null;

        this._footerElementContainer = this.element.createChild("div", "inspector-footer status-bar hidden");
    }

    inherits(MainView, View);

    MainView.Events = {
        PanelSelected: "PanelSelected"
    }

    MainView.prototype.addPanel = function(panel) {
        this._panelOrder.push(panel.name);
        this._panels[panel.name] = panel;
    }

    MainView.prototype.showPanel = function(panelName){
        var panel = this._panels[panelName];
        if (panel) {
            this._currentPanel = panel;
            panel.show();
            this.dispatchEventToListeners(MainView.Events.PanelSelected);
        }
    }

    MainView.prototype.getCurrentPanel = function (argument) {
        return this._currentPanel;
    }

    MainView.prototype.panelsElement = function(){
        return this._panelsElement;
    }

    return MainView;
});
