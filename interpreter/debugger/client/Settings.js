define(function(require, exports){

var EventObjectEmitter = require("events").EventObjectEmitter;

var Preferences = {
    maxInlineTextChildLength: 80,
    minConsoleHeight: 75,
    minSidebarWidth: 100,
    minSidebarHeight: 75,
    minElementsSidebarWidth: 200,
    minElementsSidebarHeight: 200,
    minScriptsSidebarWidth: 200,
    applicationTitle: "Developer Tools - %s",
    experimentsEnabled: false
}

/**
 * @constructor
 */
function Settings()
{
    this._eventSupport = new EventObjectEmitter();
    this._registry = /** @type {!Object.<string, !WebInspector.Setting>} */ ({});

    this.navigatorWasOnceHidden = this.createSetting("navigatorWasOnceHidden", false);
    this.debuggerSidebarHidden = this.createSetting("debuggerSidebarHidden", false);

    this.colorFormat = this.createSetting("colorFormat", "original");
    this.consoleHistory = this.createSetting("consoleHistory", []);
    this.domWordWrap = this.createSetting("domWordWrap", true);
    this.eventListenersFilter = this.createSetting("eventListenersFilter", "all");
    this.lastActivePanel = this.createSetting("lastActivePanel", "elements");
    this.lastViewedScriptFile = this.createSetting("lastViewedScriptFile", "application");
    this.monitoringXHREnabled = this.createSetting("monitoringXHREnabled", false);
    this.preserveConsoleLog = this.createSetting("preserveConsoleLog", false);
    this.resourcesLargeRows = this.createSetting("resourcesLargeRows", true);
    this.resourcesSortOptions = this.createSetting("resourcesSortOptions", {timeOption: "responseTime", sizeOption: "transferSize"});
    this.resourceViewTab = this.createSetting("resourceViewTab", "preview");
    this.showInheritedComputedStyleProperties = this.createSetting("showInheritedComputedStyleProperties", false);
    this.showUserAgentStyles = this.createSetting("showUserAgentStyles", true);
    this.watchExpressions = this.createSetting("watchExpressions", []);
    this.breakpoints = this.createSetting("breakpoints", []);
    this.eventListenerBreakpoints = this.createSetting("eventListenerBreakpoints", []);
    this.domBreakpoints = this.createSetting("domBreakpoints", []);
    this.xhrBreakpoints = this.createSetting("xhrBreakpoints", []);
    this.jsSourceMapsEnabled = this.createSetting("sourceMapsEnabled", true);
    this.cssSourceMapsEnabled = this.createSetting("cssSourceMapsEnabled", true);
    this.cacheDisabled = this.createSetting("cacheDisabled", false);
    this.enableOverridesOnStartup = this.createSetting("enableOverridesOnStartup", false);
    this.overrideUserAgent = this.createSetting("overrideUserAgent", false);
    this.userAgent = this.createSetting("userAgent", "");
    this.overrideDeviceMetrics = this.createSetting("overrideDeviceMetrics", false);
    this.deviceMetrics = this.createSetting("deviceMetrics", "");
    this.deviceFitWindow = this.createSetting("deviceFitWindow", false);
    this.emulateTouchEvents = this.createSetting("emulateTouchEvents", false);
    this.showShadowDOM = this.createSetting("showShadowDOM", false);
    this.zoomLevel = this.createSetting("zoomLevel", 0);
    this.savedURLs = this.createSetting("savedURLs", {});
    this.javaScriptDisabled = this.createSetting("javaScriptDisabled", false);
    this.overrideGeolocation = this.createSetting("overrideGeolocation", false);
    this.geolocationOverride = this.createSetting("geolocationOverride", "");
    this.overrideDeviceOrientation = this.createSetting("overrideDeviceOrientation", false);
    this.deviceOrientationOverride = this.createSetting("deviceOrientationOverride", "");
    this.showAdvancedHeapSnapshotProperties = this.createSetting("showAdvancedHeapSnapshotProperties", false);
    this.searchInContentScripts = this.createSetting("searchInContentScripts", false);
    this.textEditorIndent = this.createSetting("textEditorIndent", "    ");
    this.textEditorAutoDetectIndent = this.createSetting("textEditorAutoIndentIndent", true);
    this.lastDockState = this.createSetting("lastDockState", "");
    this.cssReloadEnabled = this.createSetting("cssReloadEnabled", false);
    this.showCpuOnTimelineRuler = this.createSetting("showCpuOnTimelineRuler", false);
    this.timelineStackFramesToCapture = this.createSetting("timelineStackFramesToCapture", 30);
    this.timelineLimitStackFramesFlag = this.createSetting("timelineLimitStackFramesFlag", false);
    this.showMetricsRulers = this.createSetting("showMetricsRulers", false);
    this.overrideCSSMedia = this.createSetting("overrideCSSMedia", false);
    this.emulatedCSSMedia = this.createSetting("emulatedCSSMedia", "print");
    this.workerInspectorWidth = this.createSetting("workerInspectorWidth", 600);
    this.workerInspectorHeight = this.createSetting("workerInspectorHeight", 600);
    this.messageURLFilters = this.createSetting("messageURLFilters", {});
    this.messageSourceFilters = this.createSetting("messageSourceFilters", {"CSS": true});
    this.messageLevelFilters = this.createSetting("messageLevelFilters", {});
    this.splitVerticallyWhenDockedToRight = this.createSetting("splitVerticallyWhenDockedToRight", true);
    this.visiblePanels = this.createSetting("visiblePanels", {});
    this.shortcutPanelSwitch = this.createSetting("shortcutPanelSwitch", false);
    this.showWhitespacesInEditor = this.createSetting("showWhitespacesInEditor", false);
    this.skipStackFramesSwitch = this.createSetting("skipStackFramesSwitch", false);
    this.skipStackFramesPattern = this.createSetting("skipStackFramesPattern", "");
}

Settings.prototype = {
    /**
     * @param {string} key
     * @param {*} defaultValue
     * @return {!WebInspector.Setting}
     */
    createSetting: function(key, defaultValue)
    {
        if (!this._registry[key])
            this._registry[key] = new Setting(key, defaultValue, this._eventSupport, window.localStorage);
        return this._registry[key];
    }
}

/**
 * @constructor
 * @param {string} name
 * @param {*} defaultValue
 * @param {!WebInspector.Object} eventSupport
 * @param {?Storage} storage
 */
Setting = function(name, defaultValue, eventSupport, storage)
{
    this._name = name;
    this._defaultValue = defaultValue;
    this._eventSupport = eventSupport;
    this._storage = storage;
}

Setting.prototype = {
    /**
     * @param {function(WebInspector.Event)} listener
     * @param {Object=} thisObject
     */
    addChangeListener: function(listener, thisObject)
    {
        this._eventSupport.addEventListener(this._name, listener, thisObject);
    },

    /**
     * @param {function(WebInspector.Event)} listener
     * @param {Object=} thisObject
     */
    removeChangeListener: function(listener, thisObject)
    {
        this._eventSupport.removeEventListener(this._name, listener, thisObject);
    },

    get name()
    {
        return this._name;
    },

    get: function()
    {
        if (typeof this._value !== "undefined")
            return this._value;

        this._value = this._defaultValue;
        if (this._storage && this._name in this._storage) {
            try {
                this._value = JSON.parse(this._storage[this._name]);
            } catch(e) {
                delete this._storage[this._name];
            }
        }
        return this._value;
    },

    set: function(value)
    {
        this._value = value;
        if (this._storage) {
            try {
                this._storage[this._name] = JSON.stringify(value);
            } catch(e) {
                console.error("Error saving setting with name:" + this._name);
            }
        }
        this._eventSupport.dispatchEventToListeners(this._name, value);
    }
}


exports.Preferences = Preferences;
exports.Settings = Settings;
exports.Setting = Setting;

});
