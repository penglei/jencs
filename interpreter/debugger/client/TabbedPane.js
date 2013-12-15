define(function(require){

//require("tabbedPane.css");

var View = require("View");

var UIUtils = require("UIUtils");
/**
 * @extends {View}
 * @constructor
 */
function TabbedPane()
{
    View.call(this);
    this.element.addStyleClass("tabbed-pane");
    this._headerElement = this.element.createChild("div", "tabbed-pane-header");
    this._headerContentsElement = this._headerElement.createChild("div", "tabbed-pane-header-contents");
    this._tabsElement = this._headerContentsElement.createChild("div", "tabbed-pane-header-tabs");
    this._contentElement = this.element.createChild("div", "tabbed-pane-content scroll-target");
    this._tabs = [];
    this._tabsHistory = [];
    this._tabsById = {};
    this.element.addEventListener("click", this.focus.bind(this), true);
    this.element.addEventListener("mouseup", this.onMouseUp.bind(this), false);

    this._dropDownButton = this._createDropDownButton();
}

TabbedPane.EventTypes = {
    TabSelected: "TabSelected",
    TabClosed: "TabClosed"
}

TabbedPane.prototype = {
    /**
     * @return {View}
     */
    get visibleView()
    {
        return this._currentTab ? this._currentTab.view : null;
    },

    /**
     * @return {string}
     */
    get selectedTabId()
    {
        return this._currentTab ? this._currentTab.id : null;
    },

    /**
     * @type {boolean} shrinkableTabs
     */
    set shrinkableTabs(shrinkableTabs)
    {
        this._shrinkableTabs = shrinkableTabs;
    },

    /**
     * @type {boolean} verticalTabLayout
     */
    set verticalTabLayout(verticalTabLayout)
    {
        this._verticalTabLayout = verticalTabLayout;
    },

    /**
     * @type {boolean} closeableTabs
     */
    set closeableTabs(closeableTabs)
    {
        this._closeableTabs = closeableTabs;
    },

    /**
     * @param {boolean} retainTabsOrder
     */
    setRetainTabsOrder: function(retainTabsOrder)
    {
        this._retainTabsOrder = retainTabsOrder;
    },

    defaultFocusedElement: function()
    {
        return this.visibleView ? this.visibleView.defaultFocusedElement() : null;
    },

    /**
     * @param {TabbedPaneTabDelegate} delegate
     */
    setTabDelegate: function(delegate)
    {
        var tabs = this._tabs.slice();
        for (var i = 0; i < tabs.length; ++i)
            tabs[i].setDelegate(delegate);
        this._delegate = delegate;
    },

    /**
     * @param {Event} event
     */
    onMouseUp: function(event)
    {
        // This is needed to prevent middle-click pasting on linux when tabs are clicked.
        if (event.button === 1)
            event.consume(true);
    },

    /**
     * @param {string} id
     * @param {string} tabTitle
     * @param {View} view
     * @param {string=} tabTooltip
     * @param {boolean=} userGesture
     */
    appendTab: function(id, tabTitle, view, tabTooltip, userGesture)
    {
        var tab = new TabbedPaneTab(this, id, tabTitle, this._closeableTabs, view, tabTooltip);
        tab.setDelegate(this._delegate);
        this._tabsById[id] = tab;

        this._tabs.push(tab);
        this._tabsHistory.push(tab);

        if (this._tabsHistory[0] === tab)
            this.selectTab(tab.id, userGesture);

        this._updateTabElements();
    },

    /**
     * @param {string} id
     * @param {boolean=} userGesture
     */
    closeTab: function(id, userGesture)
    {
        this.closeTabs([id], userGesture);
    },

     /**
      * @param {Array.<string>} ids
      * @param {boolean=} userGesture
      */
     closeTabs: function(ids, userGesture)
     {
         for (var i = 0; i < ids.length; ++i)
             this._innerCloseTab(ids[i], userGesture);
         this._updateTabElements();
         if (this._tabsHistory.length)
             this.selectTab(this._tabsHistory[0].id, userGesture);
     },

    /**
     * @param {string} id
     * @param {boolean=} userGesture
     */
    _innerCloseTab: function(id, userGesture)
    {
        if (this._currentTab && this._currentTab.id === id)
            this._hideCurrentTab();

        var tab = this._tabsById[id];
        delete this._tabsById[id];

        this._tabsHistory.splice(this._tabsHistory.indexOf(tab), 1);
        this._tabs.splice(this._tabs.indexOf(tab), 1);
        if (tab._shown)
            this._hideTabElement(tab);

        var eventData = { tabId: id, view: tab.view, isUserGesture: userGesture };
        this.dispatchEventToListeners(TabbedPane.EventTypes.TabClosed, eventData);
        return true;
    },

    /**
     * @return {Array.<string>}
     */
    allTabs: function()
    {
        var result = [];
        var tabs = this._tabs.slice();
        for (var i = 0; i < tabs.length; ++i)
            result.push(tabs[i].id);
        return result;
    },

    /**
     * @param {string} id
     * @return {Array.<string>}
     */
    otherTabs: function(id)
    {
        var result = [];
        var tabs = this._tabs.slice();
        for (var i = 0; i < tabs.length; ++i) {
            if (tabs[i].id !== id)
                result.push(tabs[i].id);
        }
        return result;
    },

    /**
     * @param {string} id
     * @param {boolean=} userGesture
     */
    selectTab: function(id, userGesture)
    {
        var tab = this._tabsById[id];
        if (!tab)
            return;
        if (this._currentTab && this._currentTab.id === id)
            return;

        this._hideCurrentTab();
        this._showTab(tab);
        this._currentTab = tab;

        this._tabsHistory.splice(this._tabsHistory.indexOf(tab), 1);
        this._tabsHistory.splice(0, 0, tab);

        this._updateTabElements();

        var eventData = { tabId: id, view: tab.view, isUserGesture: userGesture };
        this.dispatchEventToListeners(TabbedPane.EventTypes.TabSelected, eventData);
        return true;
    },

    /**
     * @param {number} tabsCount
     * @return {Array.<string>}
     */
    lastOpenedTabIds: function(tabsCount)
    {
        function tabToTabId(tab) {
            return tab.id;
        }

        return this._tabsHistory.slice(0, tabsCount).map(tabToTabId);
    },

    /**
     * @param {string} id
     * @param {string} iconClass
     * @param {string=} iconTooltip
     */
    setTabIcon: function(id, iconClass, iconTooltip)
    {
        var tab = this._tabsById[id];
        tab._setIconClass(iconClass, iconTooltip);
        this._updateTabElements();
    },

    /**
     * @param {string} id
     * @param {string} tabTitle
     */
    changeTabTitle: function(id, tabTitle)
    {
        var tab = this._tabsById[id];
        tab.title = tabTitle;
        this._updateTabElements();
    },

    /**
     * @param {string} id
     * @param {View} view
     */
    changeTabView: function(id, view)
    {
        var tab = this._tabsById[id];
        if (this._currentTab && this._currentTab.id === tab.id) {
            this._hideTab(tab);
            tab.view = view;
            this._showTab(tab);
        } else
            tab.view = view;
    },

    /**
     * @param {string} id
     * @param {string=} tabTooltip
     */
    changeTabTooltip: function(id, tabTooltip)
    {
        var tab = this._tabsById[id];
        tab.tooltip = tabTooltip;
    },

    onResize: function()
    {
        this._updateTabElements();
    },

    _updateTabElements: function()
    {
        //WebInspector.invokeOnceAfterBatchUpdate(this, this._innerUpdateTabElements);
        this._innerUpdateTabElements();
    },

    /**
     * @param {string} text
     */
    setPlaceholderText: function(text)
    {
        this._noTabsMessage = text;
    },

    _innerUpdateTabElements: function()
    {
        if (!this.isShowing())
            return;

        if (!this._tabs.length) {
            this._contentElement.addStyleClass("has-no-tabs");
            if (this._noTabsMessage && !this._noTabsMessageElement) {
                this._noTabsMessageElement = this._contentElement.createChild("div", "tabbed-pane-placeholder fill");
                this._noTabsMessageElement.textContent = this._noTabsMessage;
            }
        } else {
            this._contentElement.removeStyleClass("has-no-tabs");
            if (this._noTabsMessageElement) {
                this._noTabsMessageElement.remove();
                delete this._noTabsMessageElement;
            }
        }

        if (!this._measuredDropDownButtonWidth)
            this._measureDropDownButton();

        this._updateWidths();
        this._updateTabsDropDown();
    },

    /**
     * @param {number} index
     * @param {TabbedPaneTab} tab
     */
    _showTabElement: function(index, tab)
    {
        if (index >= this._tabsElement.children.length)
            this._tabsElement.appendChild(tab.tabElement);
        else
            this._tabsElement.insertBefore(tab.tabElement, this._tabsElement.children[index]);
        tab._shown = true;
    },

    /**
     * @param {TabbedPaneTab} tab
     */
    _hideTabElement: function(tab)
    {
        this._tabsElement.removeChild(tab.tabElement);
        tab._shown = false;
    },

    _createDropDownButton: function()
    {
        var dropDownContainer = document.createElement("div");
        dropDownContainer.addStyleClass("tabbed-pane-header-tabs-drop-down-container");
        var dropDownButton = dropDownContainer.createChild("div", "tabbed-pane-header-tabs-drop-down");
        dropDownButton.appendChild(document.createTextNode("\u00bb"));
        this._tabsSelect = dropDownButton.createChild("select", "tabbed-pane-header-tabs-drop-down-select");
        this._tabsSelect.addEventListener("change", this._tabsSelectChanged.bind(this), false);
        return dropDownContainer;
    },

    _totalWidth: function()
    {
        return this._headerContentsElement.getBoundingClientRect().width;
    },

    _updateTabsDropDown: function()
    {
        var tabsToShowIndexes = this._tabsToShowIndexes(this._tabs, this._tabsHistory, this._totalWidth(), this._measuredDropDownButtonWidth);

        for (var i = 0; i < this._tabs.length; ++i) {
            if (this._tabs[i]._shown && tabsToShowIndexes.indexOf(i) === -1)
                this._hideTabElement(this._tabs[i]);
        }
        for (var i = 0; i < tabsToShowIndexes.length; ++i) {
            var tab = this._tabs[tabsToShowIndexes[i]];
            if (!tab._shown)
                this._showTabElement(i, tab);
        }

        this._populateDropDownFromIndex();
    },

    _populateDropDownFromIndex: function()
    {
        if (this._dropDownButton.parentElement)
            this._headerContentsElement.removeChild(this._dropDownButton);

        this._tabsSelect.removeChildren();
        var tabsToShow = [];
        for (var i = 0; i < this._tabs.length; ++i) {
            if (!this._tabs[i]._shown)
                tabsToShow.push(this._tabs[i]);
                continue;
        }

        function compareFunction(tab1, tab2)
        {
            return tab1.title.localeCompare(tab2.title);
        }
        tabsToShow.sort(compareFunction);

        var selectedIndex = -1;
        for (var i = 0; i < tabsToShow.length; ++i) {
            var option = new Option(tabsToShow[i].title);
            option.tab = tabsToShow[i];
            this._tabsSelect.appendChild(option);
            if (this._tabsHistory[0] === tabsToShow[i])
                selectedIndex = i;
        }
        if (this._tabsSelect.options.length) {
            this._headerContentsElement.appendChild(this._dropDownButton);
            this._tabsSelect.selectedIndex = selectedIndex;
        }
    },

    _tabsSelectChanged: function()
    {
        var options = this._tabsSelect.options;
        var selectedOption = options[this._tabsSelect.selectedIndex];
        this.selectTab(selectedOption.tab.id, true);
    },

    _measureDropDownButton: function()
    {
        this._dropDownButton.addStyleClass("measuring");
        this._headerContentsElement.appendChild(this._dropDownButton);
        this._measuredDropDownButtonWidth = this._dropDownButton.getBoundingClientRect().width;
        this._headerContentsElement.removeChild(this._dropDownButton);
        this._dropDownButton.removeStyleClass("measuring");
    },

    _updateWidths: function()
    {
        var measuredWidths = this._measureWidths();
        var maxWidth = this._shrinkableTabs ? this._calculateMaxWidth(measuredWidths.slice(), this._totalWidth()) : Number.MAX_VALUE;

        var i = 0;
        for (var tabId in this._tabs) {
            var tab = this._tabs[tabId];
            tab.setWidth(this._verticalTabLayout ? -1 : Math.min(maxWidth, measuredWidths[i++]));
        }
    },

    _measureWidths: function()
    {
        // Add all elements to measure into this._tabsElement
        var measuringTabElements = [];
        for (var tabId in this._tabs) {
            var tab = this._tabs[tabId];
            if (typeof tab._measuredWidth === "number")
                continue;
            var measuringTabElement = tab._createTabElement(true);
            measuringTabElement.__tab = tab;
            measuringTabElements.push(measuringTabElement);
            this._tabsElement.appendChild(measuringTabElement);
        }

        // Perform measurement
        for (var i = 0; i < measuringTabElements.length; ++i)
            measuringTabElements[i].__tab._measuredWidth = measuringTabElements[i].getBoundingClientRect().width;

        // Nuke elements from the UI
        for (var i = 0; i < measuringTabElements.length; ++i)
            measuringTabElements[i].remove();

        // Combine the results.
        var measuredWidths = [];
        for (var tabId in this._tabs)
            measuredWidths.push(this._tabs[tabId]._measuredWidth);

        return measuredWidths;
    },

    /**
     * @param {Array.<number>} measuredWidths
     * @param {number} totalWidth
     */
    _calculateMaxWidth: function(measuredWidths, totalWidth)
    {
        if (!measuredWidths.length)
            return 0;

        measuredWidths.sort(function(x, y) { return x - y });

        var totalMeasuredWidth = 0;
        for (var i = 0; i < measuredWidths.length; ++i)
            totalMeasuredWidth += measuredWidths[i];

        if (totalWidth >= totalMeasuredWidth)
            return measuredWidths[measuredWidths.length - 1];

        var totalExtraWidth = 0;
        for (var i = measuredWidths.length - 1; i > 0; --i) {
            var extraWidth = measuredWidths[i] - measuredWidths[i - 1];
            totalExtraWidth += (measuredWidths.length - i) * extraWidth;

            if (totalWidth + totalExtraWidth >= totalMeasuredWidth)
                return measuredWidths[i - 1] + (totalWidth + totalExtraWidth - totalMeasuredWidth) / (measuredWidths.length - i); 
        }

        return totalWidth / measuredWidths.length;
    },

    /**
     * @param {Array.<TabbedPaneTab>} tabsOrdered
     * @param {Array.<TabbedPaneTab>} tabsHistory
     * @param {number} totalWidth
     * @param {number} measuredDropDownButtonWidth
     * @return {Array.<number>}
     */
    _tabsToShowIndexes: function(tabsOrdered, tabsHistory, totalWidth, measuredDropDownButtonWidth)
    {
        var tabsToShowIndexes = [];

        var totalTabsWidth = 0;
        var tabCount = tabsOrdered.length;
        for (var i = 0; i < tabCount; ++i) {
            var tab = this._retainTabsOrder ? tabsOrdered[i] : tabsHistory[i];
            totalTabsWidth += tab.width();
            var minimalRequiredWidth = totalTabsWidth;
            if (i !== tabCount - 1)
                minimalRequiredWidth += measuredDropDownButtonWidth;
            if (!this._verticalTabLayout && minimalRequiredWidth > totalWidth)
                break;
            tabsToShowIndexes.push(tabsOrdered.indexOf(tab));
        }

        tabsToShowIndexes.sort(function(x, y) { return x - y });

        return tabsToShowIndexes;
    },
    
    _hideCurrentTab: function()
    {
        if (!this._currentTab)
            return;

        this._hideTab(this._currentTab);
        delete this._currentTab;
    },

    /**
     * @param {TabbedPaneTab} tab
     */
    _showTab: function(tab)
    {
        tab.tabElement.addStyleClass("selected");
        tab.view.show(this._contentElement);
    },

    /**
     * @param {TabbedPaneTab} tab
     */
    _hideTab: function(tab)
    {
        tab.tabElement.removeStyleClass("selected");
        tab.view.detach();
    },

    /**
     * @override
     */
    canHighlightPosition: function()
    {
        return this._currentTab && this._currentTab.view && this._currentTab.view.canHighlightPosition();
    },

    /**
     * @override
     */
    highlightPosition: function(line, column)
    {
        if (this.canHighlightPosition())
            this._currentTab.view.highlightPosition(line, column);
    },

    /**
     * @return {Array.<Element>}
     */
    elementsToRestoreScrollPositionsFor: function()
    {
        return [ this._contentElement ];
    },

    /**
     * @param {TabbedPaneTab} tab
     * @param {number} index
     */
    _insertBefore: function(tab, index)
    {
        this._tabsElement.insertBefore(tab._tabElement, this._tabsElement.childNodes[index]);
        var oldIndex = this._tabs.indexOf(tab);
        this._tabs.splice(oldIndex, 1);
        if (oldIndex < index)
            --index;
        this._tabs.splice(index, 0, tab);
    },

    __proto__: View.prototype
}


/**
 * @constructor
 * @param {TabbedPane} tabbedPane
 * @param {string} id
 * @param {string} title
 * @param {boolean} closeable
 * @param {View} view
 * @param {string=} tooltip
 */
function TabbedPaneTab(tabbedPane, id, title, closeable, view, tooltip)
{
    this._closeable = closeable;
    this._tabbedPane = tabbedPane;
    this._id = id;
    this._title = title;
    this._tooltip = tooltip;
    this._view = view;
    this._shown = false;
    /** @type {number} */ this._measuredWidth;
    /** @type {Element} */ this._tabElement;
}

TabbedPaneTab.prototype = {
    /**
     * @return {string}
     */
    get id()
    {
        return this._id;
    },

    /**
     * @return {string}
     */
    get title()
    {
        return this._title;
    },

    set title(title)
    {
        if (title === this._title)
            return;
        this._title = title;
        if (this._titleElement)
            this._titleElement.textContent = title;
        delete this._measuredWidth;
    },

    /**
     * @return {string}
     */
    iconClass: function()
    {
        return this._iconClass;
    },

    /**
     * @param {string} iconClass
     * @param {string} iconTooltip
     */
    _setIconClass: function(iconClass, iconTooltip)
    {
        if (iconClass === this._iconClass && iconTooltip === this._iconTooltip)
            return;
        this._iconClass = iconClass;
        this._iconTooltip = iconTooltip;
        if (this._iconElement)
            this._iconElement.remove();
        if (this._iconClass && this._tabElement)
            this._iconElement = this._createIconElement(this._tabElement, this._titleElement);
        delete this._measuredWidth;
    },

    /**
     * @return {View}
     */
    get view()
    {
        return this._view;
    },

    set view(view)
    {
        this._view = view;
    },

    /**
     * @return {string|undefined}
     */
    get tooltip()
    {
        return this._tooltip;
    },

    set tooltip(tooltip)
    {
        this._tooltip = tooltip;
        if (this._titleElement)
            this._titleElement.title = tooltip || "";
    },

    /**
     * @return {Element}
     */
    get tabElement()
    {
        if (typeof(this._tabElement) !== "undefined")
            return this._tabElement;
        
        this._createTabElement(false);
        return this._tabElement;
    },

    /**
     * @return {number}
     */
    width: function()
    {
        return this._width;
    },

    /**
     * @param {number} width
     */
    setWidth: function(width)
    {
        this.tabElement.style.width = width === -1 ? "" : (width + "px");
        this._width = width;
    },

    /**
     * @param {TabbedPaneTabDelegate} delegate
     */
    setDelegate: function(delegate)
    {
        this._delegate = delegate;
    },

    _createIconElement: function(tabElement, titleElement)
    {
        var iconElement = document.createElement("span");
        iconElement.className = "tabbed-pane-header-tab-icon " + this._iconClass;
        if (this._iconTooltip)
            iconElement.title = this._iconTooltip;
        tabElement.insertBefore(iconElement, titleElement);
        return iconElement;
    },

    /**
     * @param {boolean} measuring
     */
    _createTabElement: function(measuring)
    {
        var tabElement = document.createElement("div");
        tabElement.addStyleClass("tabbed-pane-header-tab");
        tabElement.id = "tab-" + this._id;
        tabElement.tabIndex = -1;

        var titleElement = tabElement.createChild("span", "tabbed-pane-header-tab-title");
        titleElement.textContent = this.title;
        titleElement.title = this.tooltip || "";
        if (this._iconClass)
            this._createIconElement(tabElement, titleElement);
        if (!measuring)
            this._titleElement = titleElement;

        if (this._closeable)
            tabElement.createChild("div", "close-button-gray");

        if (measuring)
            tabElement.addStyleClass("measuring");
        else {
            this._tabElement = tabElement;
            tabElement.addEventListener("click", this._tabClicked.bind(this), false);
            tabElement.addEventListener("mousedown", this._tabMouseDown.bind(this), false);
            if (this._closeable) {
                tabElement.addEventListener("contextmenu", this._tabContextMenu.bind(this), false);
                UIUtils.installDragHandle(tabElement, this._startTabDragging.bind(this), this._tabDragging.bind(this), this._endTabDragging.bind(this), "pointer");
            }
        }

        return tabElement;
    },

    /**
     * @param {Event} event
     */
    _tabClicked: function(event)
    {
        var middleButton = event.button === 1;
        var shouldClose = this._closeable && (middleButton || event.target.hasStyleClass("close-button-gray"));
        if (!shouldClose)
            return;
        this._closeTabs([this.id]);
        event.consume(true);
    },

    /**
     * @param {Event} event
     */
    _tabMouseDown: function(event)
    {
        if (event.target.hasStyleClass("close-button-gray") || event.button === 1)
            return;
        this._tabbedPane.selectTab(this.id, true);
    },

    /**
     * @param {Array.<string>} ids
     */
    _closeTabs: function(ids)
    {
        if (this._delegate) {
            this._delegate.closeTabs(this._tabbedPane, ids);
            return;
        }
        this._tabbedPane.closeTabs(ids, true);
    },

    _tabContextMenu: function(event)
    {
        function close()
        {
            this._closeTabs([this.id]);
        }
  
        function closeOthers()
        {
            this._closeTabs(this._tabbedPane.otherTabs(this.id));
        }
  
        function closeAll()
        {
            this._closeTabs(this._tabbedPane.allTabs(this.id));
        }
  
        var contextMenu = new ContextMenu(event);
        contextMenu.appendItem(UIString("Close"), close.bind(this));
        contextMenu.appendItem(UIString(useLowerCaseMenuTitles() ? "Close others" : "Close Others"), closeOthers.bind(this));
        contextMenu.appendItem(UIString(useLowerCaseMenuTitles() ? "Close all" : "Close All"), closeAll.bind(this));
        contextMenu.show();
    },

    /**
     * @param {Event} event
     * @return {boolean}
     */
    _startTabDragging: function(event)
    {
        if (event.target.hasStyleClass("close-button-gray"))
            return false;
        this._dragStartX = event.pageX;
        return true;
    },

    /**
     * @param {Event} event
     */
    _tabDragging: function(event)
    {
        var tabElements = this._tabbedPane._tabsElement.childNodes;
        for (var i = 0; i < tabElements.length; ++i) {
            var tabElement = tabElements[i];
            if (tabElement === this._tabElement)
                continue;

            var intersects = tabElement.offsetLeft + tabElement.clientWidth > this._tabElement.offsetLeft &&
                this._tabElement.offsetLeft + this._tabElement.clientWidth > tabElement.offsetLeft;
            if (!intersects)
                continue;

            if (Math.abs(event.pageX - this._dragStartX) < tabElement.clientWidth / 2 + 5)
                break;

            if (event.pageX - this._dragStartX > 0) {
                tabElement = tabElement.nextSibling;
                ++i;
            }

            var oldOffsetLeft = this._tabElement.offsetLeft;
            this._tabbedPane._insertBefore(this, i);
            this._dragStartX += this._tabElement.offsetLeft - oldOffsetLeft;
            break;
        }

        if (!this._tabElement.previousSibling && event.pageX - this._dragStartX < 0) {
            this._tabElement.style.setProperty("left", "0px");
            return;
        }
        if (!this._tabElement.nextSibling && event.pageX - this._dragStartX > 0) {
            this._tabElement.style.setProperty("left", "0px");
            return;
        }

        this._tabElement.style.setProperty("position", "relative");
        this._tabElement.style.setProperty("left", (event.pageX - this._dragStartX) + "px");
    },

    /**
     * @param {Event} event
     */
    _endTabDragging: function(event)
    {
        this._tabElement.style.removeProperty("position");
        this._tabElement.style.removeProperty("left");
        delete this._dragStartX;
    }
}

/**
 * @interface
 */
function TabbedPaneTabDelegate()
{
}

TabbedPaneTabDelegate.prototype = {
    /**
     * @param {TabbedPane} tabbedPane
     * @param {Array.<string>} ids
     */
    closeTabs: function(tabbedPane, ids) { }
}

return TabbedPane;

});