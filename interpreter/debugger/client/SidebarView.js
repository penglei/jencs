define(function (require, exports) {

var Preferences = require("Settings").Preferences;
var SplitView = require("SplitView");

/**
 * @constructor
 * @extends {SplitView}
 * @param {string=} sidebarPosition
 * @param {string=} sidebarWidthSettingName
 * @param {number=} defaultSidebarWidth
 * @param {number=} defaultSidebarHeight
 */
function SidebarView(sidebarPosition, sidebarWidthSettingName, defaultSidebarWidth, defaultSidebarHeight)
{
    SplitView.call(this, true, sidebarWidthSettingName, defaultSidebarWidth, defaultSidebarHeight);

    this.setSidebarElementConstraints(Preferences.minSidebarWidth, Preferences.minSidebarHeight);
    this.setMainElementConstraints(0.5, 0.5);

    this.setSecondIsSidebar(sidebarPosition === SidebarView.SidebarPosition.End);
}

SidebarView.Events = {
    Resized: "Resized"
}

/**
 * @enum {string}
 */
SidebarView.SidebarPosition = {
    Start: "Start",
    End: "End"
}

SidebarView.prototype = {
    /**
     * @param {number} width
     */
    setSidebarWidth: function(width)
    {
        this.setSidebarSize(width);
    },

    /**
     * @return {number}
     */
    sidebarWidth: function()
    {
        return this.sidebarSize();
    },

    onResize: function()
    {
        SplitView.prototype.onResize.call(this);
        this.dispatchEventToListeners(SidebarView.Events.Resized, this.sidebarWidth());
    },

    hideMainElement: function()
    {
        if (this.isSidebarSecond())
            this.showOnlySecond();
        else
            this.showOnlyFirst();
    },

    showMainElement: function()
    {
        this.showBoth();
    },

    hideSidebarElement: function()
    {
        if (this.isSidebarSecond())
            this.showOnlyFirst();
        else
            this.showOnlySecond();
    },

    showSidebarElement: function()
    {
        this.showBoth();
    },

    /**
     * @return {Array.<Element>}
     */
    elementsToRestoreScrollPositionsFor: function()
    {
        return [ this.mainElement, this.sidebarElement ];
    },

    __proto__: SplitView.prototype
}

return SidebarView;

});
