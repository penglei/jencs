define(function(require){

var EventObjectEmitter = require("events").EventObjectEmitter;
var TabbedPane = require("TabbedPane");
var UISourceCode = require("UISourceCode");

/**
 * @interface
 */
TabbedEditorContainerDelegate = function() { }

TabbedEditorContainerDelegate.prototype = {
    /**
     * @param {UISourceCode} uiSourceCode
     * @return {SourceFrame}
     */
    viewForFile: function(uiSourceCode) { }
}

/**
 * @constructor
 * @extends {Object}
 * @param {TabbedEditorContainerDelegate} delegate
 * @param {string} settingName
 * @param {string} placeholderText
 */
function TabbedEditorContainer(delegate, settingName, placeholderText)
{
    EventObjectEmitter.call(this);
    this._delegate = delegate;//ScriptsPanel

    this._tabbedPane = new TabbedPane();
    this._tabbedPane.setPlaceholderText(placeholderText);
    this._tabbedPane.setTabDelegate(new EditorContainerTabDelegate(this));

    this._tabbedPane.closeableTabs = true;
    this._tabbedPane.element.id = "scripts-editor-container-tabbed-pane";

    this._tabbedPane.addEventListener(TabbedPane.EventTypes.TabClosed, this._tabClosed, this);
    this._tabbedPane.addEventListener(TabbedPane.EventTypes.TabSelected, this._tabSelected, this);

    this._tabIds = new Map();
    this._files = {};

    this._previouslyViewedFilesSetting = CSInspector.settings.createSetting(settingName, []);
    this._history = TabbedEditorContainer.History.fromObject(this._previouslyViewedFilesSetting.get());
}


TabbedEditorContainer.Events = {
    EditorSelected: "EditorSelected",
    EditorClosed: "EditorClosed"
}

TabbedEditorContainer._tabId = 0;

TabbedEditorContainer.maximalPreviouslyViewedFilesCount = 30;

TabbedEditorContainer.prototype = {
    /**
     * @return {View}
     */
    get view()
    {
        return this._tabbedPane;
    },

    /**
     * @type {SourceFrame}
     */
    get visibleView()
    {
        return this._tabbedPane.visibleView;
    },

    /**
     * @param {Element} parentElement
     */
    show: function(parentElement)
    {
        this._tabbedPane.show(parentElement);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    showFile: function(uiSourceCode)
    {
        this._innerShowFile(uiSourceCode, true);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @param {boolean=} userGesture
     */
    _innerShowFile: function(uiSourceCode, userGesture)
    {
        if (this._currentFile === uiSourceCode)
            return;

        this._currentFile = uiSourceCode;

        var tabId = this._tabIds.get(uiSourceCode) || this._appendFileTab(uiSourceCode, userGesture);

        this._tabbedPane.selectTab(tabId, userGesture);
        if (userGesture)
            this._editorSelectedByUserAction();

        this._currentView = this.visibleView;

        this.dispatchEventToListeners(TabbedEditorContainer.Events.EditorSelected, this._currentFile);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {string}
     */
    _titleForFile: function(uiSourceCode)
    {
        var maxDisplayNameLength = 30;
        var title = uiSourceCode.displayName(true).trimMiddle(maxDisplayNameLength);
        return title;
    },

    /**
     * @param {Array.<string>} ids
     */
    _closeTabs: function(ids)
    {
        var cleanTabs = [];
        for (var i = 0; i < ids.length; ++i) {
            var id = ids[i];
            var uiSourceCode = this._files[id];
            cleanTabs.push(id);
        }
        this._tabbedPane.closeTabs(cleanTabs, true);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    addUISourceCode: function(uiSourceCode)
    {
        var uri = uiSourceCode.uri();
        if (this._userSelectedFiles)
            return;

        var index = this._history.index(uri)
        if (index === -1)
            return;

        var tabId = this._tabIds.get(uiSourceCode) || this._appendFileTab(uiSourceCode, false);

        if (!this._currentFile)
            return;

        // Select tab if this file was the last to be shown.
        if (!index) {
            this._innerShowFile(uiSourceCode, false);
            return;
        }

        /*
        var currentProjectType = this._currentFile.project().type();
        var addedProjectType = uiSourceCode.project().type();
        var snippetsProjectType = WebInspector.projectTypes.Snippets;
        if (this._history.index(this._currentFile.uri()) && currentProjectType === snippetsProjectType && addedProjectType !== snippetsProjectType)
            this._innerShowFile(uiSourceCode, false);
        */
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    removeUISourceCode: function(uiSourceCode)
    {
        this.removeUISourceCodes([uiSourceCode]);
    },

    /**
     * @param {Array.<UISourceCode>} uiSourceCodes
     */
    removeUISourceCodes: function(uiSourceCodes)
    {
        var tabIds = [];
        for (var i = 0; i < uiSourceCodes.length; ++i) {
            var uiSourceCode = uiSourceCodes[i];
            var tabId = this._tabIds.get(uiSourceCode);
            if (tabId)
                tabIds.push(tabId);
        }
        this._tabbedPane.closeTabs(tabIds);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    _editorClosedByUserAction: function(uiSourceCode)
    {
        this._userSelectedFiles = true;
    },

    _editorSelectedByUserAction: function()
    {
        this._userSelectedFiles = true;
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {string}
     */
    _tooltipForFile: function(uiSourceCode)
    {
        return uiSourceCode.originURL();
    },

    /**
     * @param {UISourceCode} uiSourceCode
     * @param {boolean=} userGesture
     * @return {string}
     */
    _appendFileTab: function(uiSourceCode, userGesture)
    {
        var view = this._delegate.viewForFile(uiSourceCode);
        var title = this._titleForFile(uiSourceCode);
        var tooltip = this._tooltipForFile(uiSourceCode);

        var tabId = this._generateTabId();
        this._tabIds.put(uiSourceCode, tabId);
        this._files[tabId] = uiSourceCode;

        var savedScrollLineNumber = this._history.scrollLineNumber(uiSourceCode.uri());
        if (savedScrollLineNumber)
            view.scrollToLine(savedScrollLineNumber);

        this._tabbedPane.appendTab(tabId, title, view, tooltip, userGesture);

        this._updateFileTitle(uiSourceCode);
        this._addUISourceCodeListeners(uiSourceCode);
        return tabId;
    },

    /**
     * @param {Event} event
     */
    _tabClosed: function(event)
    {
        var tabId = /** @type {string} */ (event.data.tabId);
        var userGesture = /** @type {boolean} */ (event.data.isUserGesture);

        var uiSourceCode = this._files[tabId];
        if (this._currentFile === uiSourceCode) {
            delete this._currentView;
            delete this._currentFile;
        }
        this._tabIds.remove(uiSourceCode);
        delete this._files[tabId];

        this._removeUISourceCodeListeners(uiSourceCode);

        this.dispatchEventToListeners(TabbedEditorContainer.Events.EditorClosed, uiSourceCode);

        if (userGesture)
            this._editorClosedByUserAction(uiSourceCode);
    },

    /**
     * @param {Event} event
     */
    _tabSelected: function(event)
    {
        var tabId = /** @type {string} */ (event.data.tabId);
        var userGesture = /** @type {boolean} */ (event.data.isUserGesture);

        var uiSourceCode = this._files[tabId];
        this._innerShowFile(uiSourceCode, userGesture);
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    _addUISourceCodeListeners: function(uiSourceCode)
    {
        uiSourceCode.addEventListener(UISourceCode.Events.TitleChanged, this._uiSourceCodeTitleChanged, this);
        /*
        uiSourceCode.addEventListener(UISourceCode.Events.WorkingCopyChanged, this._uiSourceCodeWorkingCopyChanged, this);
        uiSourceCode.addEventListener(UISourceCode.Events.WorkingCopyCommitted, this._uiSourceCodeWorkingCopyCommitted, this);
        uiSourceCode.addEventListener(UISourceCode.Events.SavedStateUpdated, this._uiSourceCodeSavedStateUpdated, this);
        uiSourceCode.addEventListener(UISourceCode.Events.FormattedChanged, this._uiSourceCodeFormattedChanged, this);
        */
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    _removeUISourceCodeListeners: function(uiSourceCode)
    {
        uiSourceCode.removeEventListener(UISourceCode.Events.TitleChanged, this._uiSourceCodeTitleChanged, this);
        /*
        uiSourceCode.removeEventListener(UISourceCode.Events.WorkingCopyChanged, this._uiSourceCodeWorkingCopyChanged, this);
        uiSourceCode.removeEventListener(UISourceCode.Events.WorkingCopyCommitted, this._uiSourceCodeWorkingCopyCommitted, this);
        uiSourceCode.removeEventListener(UISourceCode.Events.SavedStateUpdated, this._uiSourceCodeSavedStateUpdated, this);
        uiSourceCode.removeEventListener(UISourceCode.Events.FormattedChanged, this._uiSourceCodeFormattedChanged, this);
        */
    },

    /**
     * @param {UISourceCode} uiSourceCode
     */
    _updateFileTitle: function(uiSourceCode)
    {
        var tabId = this._tabIds.get(uiSourceCode);
        if (tabId) {
            var title = this._titleForFile(uiSourceCode);
            this._tabbedPane.changeTabTitle(tabId, title);
            this._tabbedPane.setTabIcon(tabId, "");
        }
    },

    _uiSourceCodeTitleChanged: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._updateFileTitle(uiSourceCode);
    },

    _uiSourceCodeWorkingCopyChanged: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._updateFileTitle(uiSourceCode);
    },

    _uiSourceCodeWorkingCopyCommitted: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._updateFileTitle(uiSourceCode);
    },

    _uiSourceCodeSavedStateUpdated: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._updateFileTitle(uiSourceCode);
    },

    _uiSourceCodeFormattedChanged: function(event)
    {
        var uiSourceCode = /** @type {UISourceCode} */ (event.target);
        this._updateFileTitle(uiSourceCode);
    },

    reset: function()
    {
        delete this._userSelectedFiles;
    },

    /**
     * @return {string}
     */
    _generateTabId: function()
    {
        return "tab_" + (TabbedEditorContainer._tabId++);
    },

    /**
     * @return {UISourceCode} uiSourceCode
     */
    currentFile: function()
    {
        return this._currentFile;
    },

    __proto__: EventObjectEmitter.prototype
}

/**
 * @constructor
 * @param {string} url
 * @param {number=} scrollLineNumber
 */
TabbedEditorContainer.HistoryItem = function(url, scrollLineNumber)
{
    /** @const */ this.url = url;
    /** @const */ this._isSerializable = url.length < TabbedEditorContainer.HistoryItem.serializableUrlLengthLimit;
    this.scrollLineNumber = scrollLineNumber;
}

TabbedEditorContainer.HistoryItem.serializableUrlLengthLimit = 4096;

/**
 * @param {Object} serializedHistoryItem
 * @return {TabbedEditorContainer.HistoryItem}
 */
TabbedEditorContainer.HistoryItem.fromObject = function (serializedHistoryItem)
{
    return new TabbedEditorContainer.HistoryItem(serializedHistoryItem.url, serializedHistoryItem.scrollLineNumber);
}

TabbedEditorContainer.HistoryItem.prototype = {
    /**
     * @return {?Object}
     */
    serializeToObject: function()
    {
        if (!this._isSerializable)
            return null;
        var serializedHistoryItem = {};
        serializedHistoryItem.url = this.url;
        serializedHistoryItem.scrollLineNumber = this.scrollLineNumber;
        return serializedHistoryItem;
    },

    __proto__: EventObjectEmitter.prototype
}

/**
 * @constructor
 * @param {Array.<TabbedEditorContainer.HistoryItem>} items
 */
TabbedEditorContainer.History = function(items)
{
    this._items = items;
    this._rebuildItemIndex();
}

/**
 * @param {!Array.<!Object>} serializedHistory
 * @return {TabbedEditorContainer.History}
 */
TabbedEditorContainer.History.fromObject = function(serializedHistory)
{
    var items = [];
    for (var i = 0; i < serializedHistory.length; ++i)
        items.push(TabbedEditorContainer.HistoryItem.fromObject(serializedHistory[i]));
    return new TabbedEditorContainer.History(items);
}

TabbedEditorContainer.History.prototype = {
    /**
     * @param {string} url
     * @return {number}
     */
    index: function(url)
    {
        var index = this._itemsIndex[url];
        if (typeof index === "number")
            return index;
        return -1;
    },

    _rebuildItemIndex: function()
    {
        this._itemsIndex = {};
        for (var i = 0; i < this._items.length; ++i) {
            console.assert(!this._itemsIndex.hasOwnProperty(this._items[i].url));
            this._itemsIndex[this._items[i].url] = i;
        }
    },

    /**
     * @param {string} url
     * @return {number|undefined}
     */
    scrollLineNumber: function(url)
    {
        var index = this.index(url);
        return index !== -1 ? this._items[index].scrollLineNumber : undefined;
    },

    /**
     * @param {string} url
     * @param {number} scrollLineNumber
     */
    updateScrollLineNumber: function(url, scrollLineNumber)
    {
        var index = this.index(url);
        if (index === -1)
            return;
        this._items[index].scrollLineNumber = scrollLineNumber;
    },

    /**
     * @param {Array.<string>} urls
     */
    update: function(urls)
    {
        for (var i = urls.length - 1; i >= 0; --i) {
            var index = this.index(urls[i]);
            var item;
            if (index !== -1) {
                item = this._items[index];
                this._items.splice(index, 1);
            } else
                item = new TabbedEditorContainer.HistoryItem(urls[i]);
            this._items.unshift(item);
            this._rebuildItemIndex();
        }
    },

    /**
     * @param {string} url
     */
    remove: function(url)
    {
        var index = this.index(url);
        if (index !== -1) {
            this._items.splice(index, 1);
            this._rebuildItemIndex();
        }
    },

    /**
     * @param {Setting} setting
     */
    save: function(setting)
    {
        setting.set(this._serializeToObject());
    },

    /**
     * @return {!Array.<!Object>}
     */
    _serializeToObject: function()
    {
        var serializedHistory = [];
        for (var i = 0; i < this._items.length; ++i) {
            var serializedItem = this._items[i].serializeToObject();
            if (serializedItem)
                serializedHistory.push(serializedItem);
            if (serializedHistory.length === TabbedEditorContainer.maximalPreviouslyViewedFilesCount)
                break;
        }
        return serializedHistory;
    },


    /**
     * @return {Array.<string>}
     */
    _urls: function()
    {
        var result = [];
        for (var i = 0; i < this._items.length; ++i)
            result.push(this._items[i].url);
        return result;
    },

    __proto__: EventObjectEmitter.prototype
}


/**
 * @constructor
 * @implements {TabbedPaneTabDelegate}
 * @param {TabbedEditorContainer} editorContainer
 */
EditorContainerTabDelegate = function(editorContainer)
{
    this._editorContainer = editorContainer;
}

EditorContainerTabDelegate.prototype = {
    /**
     * @param {TabbedPane} tabbedPane
     * @param {Array.<string>} ids
     */
    closeTabs: function(tabbedPane, ids)
    {
        this._editorContainer._closeTabs(ids);
    }
}

return TabbedEditorContainer;

});
