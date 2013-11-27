define(function(require) {
    var View = require("View");
    var Panel = require("Panel").Panel;
    var ScriptsPanelDescriptor = require("ScriptsPanelDescriptor");
    var UIUtils = require("UIUtils");
    var DebugModel = require("DebugModel");
    var Preferences = require("Settings").Preferences;
    var Workspace = require("Workspace").Workspace;

    var SourceFrame = require("SourceFrame");

    var SidebarView = require("SidebarView");
    var TabbedEditorContainer = require("TabbedEditorContainer");
    var ScriptsNavigator = require("ScriptsNavigator");

    var KeyboardShortcut = require("KeyboardShortcut");
    var StatusBarButton = require("StatusBarButton").StatusBarButton;

    var NavigatorOverlayController = require("NavigatorOverlayController");


    function ScriptsPanel(){
        Panel.call(this, "scripts");

        const initialDebugSidebarWidth = 225;
        const minimumDebugSidebarWidthPercent = 0.5;
        this.createSidebarView(this.element, SidebarView.SidebarPosition.End, initialDebugSidebarWidth);
        this.splitView.element.id = "scripts-split-view";
        this.splitView.setSidebarElementConstraints(Preferences.minScriptsSidebarWidth);
        this.splitView.setMainElementConstraints(minimumDebugSidebarWidthPercent);

        // Create scripts navigator
        const initialNavigatorWidth = 225;
        const minimumViewsContainerWidthPercent = 0.5;
        this.editorView = new SidebarView(SidebarView.SidebarPosition.Start, "scriptsPanelNavigatorSidebarWidth", initialNavigatorWidth);
        this.editorView.element.tabIndex = 0;
        this.editorView.setSidebarElementConstraints(Preferences.minScriptsSidebarWidth);
        this.editorView.setMainElementConstraints(minimumViewsContainerWidthPercent);
        this.editorView.show(this.splitView.mainElement);

        this._navigator = new ScriptsNavigator();
        this._navigator.view.show(this.editorView.sidebarElement);
        /*
        this.editorView = new View();
        this.editorView.mainElement = this.editorView.element;
        this.editorView.show(this.splitView.mainElement);
        */

        //var tabbedEditorPlaceholderText = UIUtils.isMac() ? UIString("Hit Cmd+O to open a file") : UIString("Hit Ctrl+O to open a file");
        var tabbedEditorPlaceholderText = UIString("ClearSilver Debugger, Resources is loading ...");
        this._editorContentsElement = this.editorView.mainElement.createChild("div", "fill");
        this._editorFooterElement = this.editorView.mainElement.createChild("div", "inspector-footer status-bar hidden");
        this._editorContainer = new TabbedEditorContainer(this, "previouslyViewedFiles", tabbedEditorPlaceholderText);
        this._editorContainer.show(this._editorContentsElement);

        this._navigatorController = new NavigatorOverlayController(this.editorView, this._navigator.view, this._editorContainer.view);

        this.debugToolbar = this._createDebugToolbar();
        this.sidebarElement.appendChild(this.debugToolbar);


        this.sidebarPanes = {};
        this.sidebarPanes.watchExpressions = /*new WatchExpressionsSidebarPane();*/null;
        this.sidebarPanes.callstack = /*new CallStackSidebarPane();*/null;

        this.sidebarPanes.breakpoints = /*new JavaScriptBreakpointsSidebarPane(this._showSourceLocation.bind(this));*/null;


        this._sourceFramesByUISourceCode = new Map();
        this._bindWithModel();
    }

    ScriptsPanel.prototype = {
        __proto__: Panel.prototype
    };

    ScriptsPanel.prototype._bindWithModel = function() {
        CSInspector.debugModel.addEventListener(DebugModel.Events.DebuggerPaused, this._debuggerPaused, this);

        CSInspector.workspace.addEventListener(Workspace.Events.UISourceCodeAdded, this._uiSourceCodeAdded, this);
    }

    ScriptsPanel.prototype._debuggerPaused = function() {
    }


    ScriptsPanel.prototype.wasShown = function() {
        Panel.prototype.wasShown.call(this);
        //this._navigatorController.wasShown();

        /*
        this.element.addEventListener("keydown", this._boundOnKeyDown, false);
        this.element.addEventListener("keyup", this._boundOnKeyUp, false);
        */
    }

    ScriptsPanel.prototype.willHide = function() {
        /*
        this.element.removeEventListener("keydown", this._boundOnKeyDown, false);
        this.element.removeEventListener("keyup", this._boundOnKeyUp, false);
        */

        Panel.prototype.willHide.call(this);
        //closeViewInDrawer();
    }

    /**
     *@Overrides
     */
    ScriptsPanel.prototype.getStatusBarItems = function()
    {
        //return [this._pauseOnExceptionButton.element, this._toggleFormatSourceButton.element, this._scriptViewStatusBarItemsContainer];
    }


    ScriptsPanel.prototype._createDebugToolbar = function()
    {
        var debugToolbar = document.createElement("div");
        debugToolbar.className = "status-bar";
        debugToolbar.id = "scripts-debug-toolbar";

        var title, handler;
        var platformSpecificModifier = KeyboardShortcut.Modifiers.CtrlOrMeta;

        /*
        // Run snippet.
        title = UIString("Run snippet (%s).");
        handler = this._runSnippet.bind(this);
        this._runSnippetButton = this._createButtonAndRegisterShortcuts("scripts-run-snippet", title, handler, ScriptsPanelDescriptor.ShortcutKeys.RunSnippet);
        debugToolbar.appendChild(this._runSnippetButton.element);
        this._runSnippetButton.element.addStyleClass("hidden");
        */

        // Continue.
        handler = this._togglePause.bind(this);
        this._pauseButton = this._createButtonAndRegisterShortcuts("scripts-pause", "", handler, ScriptsPanelDescriptor.ShortcutKeys.PauseContinue);
        debugToolbar.appendChild(this._pauseButton.element);

        /*
        // Long resume.
        title = UIString("Resume with all pauses blocked for 500 ms");
        this._longResumeButton = new StatusBarButton(title, "scripts-long-resume");
        this._longResumeButton.addEventListener("click", this._longResume.bind(this), this);
        */

        // Step over.
        title = UIString("Step over next macro call or include command (%s).");
        handler = this._stepOverClicked.bind(this);
        this._stepOverButton = this._createButtonAndRegisterShortcuts("scripts-step-over", title, handler, ScriptsPanelDescriptor.ShortcutKeys.StepOver);
        debugToolbar.appendChild(this._stepOverButton.element);

        // Step into.
        title = UIString("Step into next macro call or include command (%s).");
        handler = this._stepIntoClicked.bind(this);
        this._stepIntoButton = this._createButtonAndRegisterShortcuts("scripts-step-into", title, handler, ScriptsPanelDescriptor.ShortcutKeys.StepInto);
        debugToolbar.appendChild(this._stepIntoButton.element);

        // Step into selection (keyboard shortcut only).
        //this.registerShortcuts(ScriptsPanelDescriptor.ShortcutKeys.StepIntoSelection, this._stepIntoSelectionClicked.bind(this))

        // Step out.
        title = UIString("Step out of current macro call (%s).");
        handler = this._stepOutClicked.bind(this);
        this._stepOutButton = this._createButtonAndRegisterShortcuts("scripts-step-out", title, handler, ScriptsPanelDescriptor.ShortcutKeys.StepOut);
        debugToolbar.appendChild(this._stepOutButton.element);

        this._toggleBreakpointsButton = new StatusBarButton(UIString("Deactivate breakpoints."), "scripts-toggle-breakpoints");
        this._toggleBreakpointsButton.toggled = false;
        this._toggleBreakpointsButton.addEventListener("click", this._toggleBreakpointsClicked, this);
        debugToolbar.appendChild(this._toggleBreakpointsButton.element);

        this.debuggerStatusElement = document.createElement("div");
        this.debuggerStatusElement.id = "scripts-debugger-status";
        debugToolbar.appendChild(this.debuggerStatusElement);

        return debugToolbar;
    }

    /**
     * @param {StatusBarButton} button
     * @param {string} buttonTitle
     */
    ScriptsPanel.prototype._updateButtonTitle = function(button, buttonTitle)
    {
        var hasShortcuts = button.shortcuts && button.shortcuts.length;
        if (hasShortcuts)
            button.title = String.vsprintf(buttonTitle, [button.shortcuts[0].name]);
        else
            button.title = buttonTitle;
    }

    /**
     * @param {string} buttonId
     * @param {string} buttonTitle
     * @param {function(Event=):boolean} handler
     * @param {!Array.<!KeyboardShortcut.Descriptor>} shortcuts
     * @return {StatusBarButton}
     */
    ScriptsPanel.prototype._createButtonAndRegisterShortcuts = function(buttonId, buttonTitle, handler, shortcuts)
    {
        var button = new StatusBarButton(buttonTitle, buttonId);
        button.element.addEventListener("click", handler, false);
        button.shortcuts = shortcuts;
        this._updateButtonTitle(button, buttonTitle);
        this.registerShortcuts(shortcuts, handler);
        return button;
    }

    /**
     * @param {Event=} event
     * @return {boolean}
     */
    ScriptsPanel.prototype._togglePause = function(event)
    {
        if (this._paused) {
            delete this._skipExecutionLineRevealing;
            this._paused = false;
            this._waitingToPause = false;
            DebuggerAgent.resume();
        } else {
            this._stepping = false;
            this._waitingToPause = true;
            // Make sure pauses didn't stick skipped.
            DebuggerAgent.setSkipAllPauses(false);
            DebuggerAgent.pause();
        }

        this._clearInterface();
        return true;
    }

    /**
     * @param {Event=} event
     * @return {boolean}
     */
    ScriptsPanel.prototype._stepOverClicked = function(event)
    {
        if (!this._paused)
            return true;

        delete this._skipExecutionLineRevealing;
        this._paused = false;
        this._stepping = true;

        this._clearInterface();

        DebuggerAgent.stepOver();
        return true;
    }

    /**
     * @param {Event=} event
     * @return {boolean}
     */
    ScriptsPanel.prototype._stepIntoClicked = function(event)
    {
        if (!this._paused)
            return true;

        delete this._skipExecutionLineRevealing;
        this._paused = false;
        this._stepping = true;

        this._clearInterface();

        DebuggerAgent.stepInto();
        return true;
    }

    /**
     * @param {Event=} event
     * @return {boolean}
     */
    ScriptsPanel.prototype._stepOutClicked = function(event)
    {
        if (!this._paused)
            return true;

        delete this._skipExecutionLineRevealing;
        this._paused = false;
        this._stepping = true;

        this._clearInterface();

        DebuggerAgent.stepOut();
        return true;
    }

    ScriptsPanel.prototype._toggleBreakpointsClicked = function(event)
    {
        WebInspector.debuggerModel.setBreakpointsActive(!WebInspector.debuggerModel.breakpointsActive());
    }

    ScriptsPanel.prototype._uiSourceCodeAdded = function(event){
        var uiSourceCode = /** @type {UISourceCode} */ (event.data);

        this._navigator.addUISourceCode(uiSourceCode);
        this._editorContainer.addUISourceCode(uiSourceCode);

        /*
        // Replace debugger script-based uiSourceCode with a network-based one.
        var currentUISourceCode = this._currentUISourceCode;
        if (currentUISourceCode && currentUISourceCode !== uiSourceCode && currentUISourceCode.url === uiSourceCode.url) {
            this._showFile(uiSourceCode);
            this._editorContainer.removeUISourceCode(currentUISourceCode);
        }
        */
    }

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {SourceFrame}
     */
    ScriptsPanel.prototype.viewForFile = function(uiSourceCode)
    {
        return this._getOrCreateSourceFrame(uiSourceCode);
    }

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {SourceFrame}
     */
    ScriptsPanel.prototype._getOrCreateSourceFrame = function(uiSourceCode)
    {
        return this._sourceFramesByUISourceCode.get(uiSourceCode) || this._createSourceFrame(uiSourceCode);
    }


    /**
     * @param {UISourceCode} uiSourceCode
     * @return {SourceFrame}
     */
    ScriptsPanel.prototype._createSourceFrame = function(uiSourceCode)
    {
        var sourceFrame = new SourceFrame(this, uiSourceCode);
        //sourceFrame = new JavaScriptSourceFrame(this, uiSourceCode);
        //sourceFrame = new UISourceCodeFrame(uiSourceCode);
        this._sourceFramesByUISourceCode.put(uiSourceCode, sourceFrame);
        return sourceFrame;
    }

    /**
     * @param {UISourceCode} uiSourceCode
     * @return {SourceFrame}
     */
    ScriptsPanel.prototype._showFile = function(uiSourceCode){
        var sourceFrame = this._getOrCreateSourceFrame(uiSourceCode);
        if (this._currentUISourceCode === uiSourceCode)
            return sourceFrame;

        this._currentUISourceCode = uiSourceCode;
        this._navigator.revealUISourceCode(uiSourceCode, true);

        this._editorContainer.showFile(uiSourceCode);
        //this._updateScriptViewStatusBarItems();

        return sourceFrame;
    }

    return ScriptsPanel;
});
