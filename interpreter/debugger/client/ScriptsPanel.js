define(function(require) {
    var View = require("View");
    var Panel = require("Panel").Panel;
    var ScriptsPanelDescriptor = require("ScriptsPanelDescriptor");
    var UIUtils = require("UIUtils");
    var DebugModel = require("DebugModel");
    var Preferences = require("Settings").Preferences;
    var Workspace = require("Workspace").Workspace;

    var ScriptSourceFrame = require("ScriptSourceFrame");

    var SidebarView = require("SidebarView");
    var TabbedEditorContainer = require("TabbedEditorContainer");
    var ScriptsNavigator = require("ScriptsNavigator");

    var KeyboardShortcut = require("KeyboardShortcut");
    var StatusBarButton = require("StatusBarButton").StatusBarButton;

    var NavigatorOverlayController = require("NavigatorOverlayController");

    var SidebarPaneStack = require("SidebarPane").SidebarPaneStack;

    var WatchExpressionsSidebarPane = require("WatchExpressionsSidebarPane");
    var CallStackSidebarPane = require("CallStackSidebarPane");
    var BreakpointsSidebarPane = require("BreakpointsSidebarPane");

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

        var tabbedEditorPlaceholderText = UIString("ClearSilver Debugger, Resources is loading ...");
        this._editorContentsElement = this.editorView.mainElement.createChild("div", "fill");
        this._editorFooterElement = this.editorView.mainElement.createChild("div", "inspector-footer status-bar hidden");
        this._editorContainer = new TabbedEditorContainer(this, "previouslyViewedFiles", tabbedEditorPlaceholderText);
        this._editorContainer.show(this._editorContentsElement);

        this._navigatorController = new NavigatorOverlayController(this.editorView, this._navigator.view, this._editorContainer.view);

        this._navigator.addEventListener(ScriptsNavigator.Events.ScriptSelected, this._scriptSelected, this);
        this._navigator.addEventListener(ScriptsNavigator.Events.ItemSearchStarted, this._itemSearchStarted, this);

        this._editorContainer.addEventListener(TabbedEditorContainer.Events.EditorSelected, this._editorSelected, this);
        this._editorContainer.addEventListener(TabbedEditorContainer.Events.EditorClosed, this._editorClosed, this);


        this._debugSidebarResizeWidgetElement = this.splitView.mainElement.createChild("div", "resizer-widget");
        this._debugSidebarResizeWidgetElement.id = "scripts-debug-sidebar-resizer-widget";
        this.splitView.installResizer(this._debugSidebarResizeWidgetElement);

        this.debugToolbar = this._createDebugToolbar();
        this.sidebarElement.appendChild(this.debugToolbar);


        this.sidebarPanes = {};
        this.sidebarPanes.watchExpressions = new WatchExpressionsSidebarPane();
        this.sidebarPanes.callstack = new CallStackSidebarPane();
        this.sidebarPanes.callstack.addEventListener(CallStackSidebarPane.Events.CallFrameSelected, this._callFrameSelectedInSidebar.bind(this));

        this.sidebarPanes.breakpoints = new BreakpointsSidebarPane(CSInspector.breakpointManager, this._showSourceLocation.bind(this));

        this._scriptViewStatusBarItemsContainer = document.createElement("div");
        this._scriptViewStatusBarItemsContainer.className = "inline-block";

        this._scriptViewStatusBarTextContainer = document.createElement("div");
        this._scriptViewStatusBarTextContainer.className = "inline-block";

        this._installDebuggerSidebarController();

        this._dockSideChanged();

        this._sourceFramesByUISourceCode = new Map();
        this._updateDebuggerButtons();

        CSInspector.workspace.addEventListener(Workspace.Events.UISourceCodeAdded, this._uiSourceCodeAdded, this);
        CSInspector.debugModel.addEventListener(DebugModel.Events.DebuggerPaused, this._debuggerPaused, this);//更新编辑器以外的视图
        CSInspector.debugModel.addEventListener(DebugModel.Events.CallFrameSelected, this._callFrameSelected, this);//更新编辑器视图
        CSInspector.debugModel.addEventListener(DebugModel.Events.BreakpointsActiveStateChanged, this._breakpointsActiveStateChanged, this);
    }

    ScriptsPanel.prototype = {
        get statusBarItems()
        {
            return [this._scriptViewStatusBarItemsContainer];
        },

        /**
         * @param {UISourceCode} uiSourceCode
         * @param {number=} lineNumber
         * @param {number=} columnNumber
         */
        _showSourceLocation: function(uiSourceCode, lineNumber, columnNumber)
        {
            var sourceFrame = this._showFile(uiSourceCode);
            if (typeof lineNumber === "number")
                sourceFrame.highlightPosition(lineNumber, columnNumber);
            sourceFrame.focus();

            /*
            WebInspector.notifications.dispatchEventToListeners(WebInspector.UserMetrics.UserAction, {
                action: WebInspector.UserMetrics.UserActionNames.OpenSourceLink,
                url: uiSourceCode.originURL(),
                lineNumber: lineNumber
            });
            */
        },

        _dockSideChanged: function()
        {
            //var dockSide = WebInspector.dockController.dockSide();
            //var vertically = dockSide === DockController.State.DockedToRight && CSInspector.settings.splitVerticallyWhenDockedToRight.get();
            //this._splitVertically(vertically);
            this._splitVertically(false);
        },
        /**
         * @param {boolean} vertically
         */
        _splitVertically: function(vertically)
        {
            if (this.sidebarPaneView && vertically === !this.splitView.isVertical())
                return;

            if (this.sidebarPaneView)
                this.sidebarPaneView.detach();

            this.splitView.setVertical(!vertically);

            if (!vertically) {
                this.sidebarPaneView = new SidebarPaneStack();
                for (var pane in this.sidebarPanes)
                    this.sidebarPaneView.addPane(this.sidebarPanes[pane]);

                //this.sidebarElement.appendChild(this.debugToolbar);
            } else {/*
                this._enableDebuggerSidebar(true);

                this.sidebarPaneView = new SplitView(true, this.name + "PanelSplitSidebarRatio", 0.5);

                var group1 = new SidebarPaneStack();
                group1.show(this.sidebarPaneView.firstElement());
                group1.element.id = "scripts-sidebar-stack-pane";
                group1.addPane(this.sidebarPanes.callstack);
                group1.addPane(this.sidebarPanes.jsBreakpoints);
                group1.addPane(this.sidebarPanes.domBreakpoints);
                group1.addPane(this.sidebarPanes.xhrBreakpoints);
                group1.addPane(this.sidebarPanes.eventListenerBreakpoints);
                if (this.sidebarPanes.workerList)
                    group1.addPane(this.sidebarPanes.workerList);

                var group2 = new SidebarTabbedPane();
                group2.show(this.sidebarPaneView.secondElement());
                group2.addPane(this.sidebarPanes.scopechain);
                group2.addPane(this.sidebarPanes.watchExpressions);

                this.sidebarPaneView.firstElement().appendChild(this.debugToolbar);
            */}

            this.sidebarPaneView.element.id = "scripts-debug-sidebar-contents";
            this.sidebarPaneView.show(this.splitView.sidebarElement);

            //x this.sidebarPanes.scopechain.expand();
            this.sidebarPanes.breakpoints.expand();
            this.sidebarPanes.callstack.expand();

            if (CSInspector.settings.watchExpressions.get().length > 0)
                this.sidebarPanes.watchExpressions.expand();
        },

        _clearCurrentExecutionLine: function()
        {
            if (this._executionSourceFrame)
                this._executionSourceFrame.clearExecutionLine();
            delete this._executionSourceFrame;
        },

        _setExecutionLine: function(uiLocation)
        {
            var callFrame = CSInspector.debugModel.selectedCallFrame()
            var sourceFrame = this._getOrCreateSourceFrame(uiLocation.uiSourceCode);
            sourceFrame.setExecutionLine(uiLocation.lineNumber, callFrame);
            this._executionSourceFrame = sourceFrame;
        },

        _executionLineChanged: function(uiLocation)
        {
            this._clearCurrentExecutionLine();
            this._setExecutionLine(uiLocation);

            var uiSourceCode = uiLocation.uiSourceCode;
            //var scriptFile = this._currentUISourceCode ? this._currentUISourceCode.scriptFile() : null;
            if (this._skipExecutionLineRevealing)
                return;
            this._skipExecutionLineRevealing = true;
            var sourceFrame = this._showFile(uiSourceCode);
            sourceFrame.revealLine(uiLocation.lineNumber);
            //if (sourceFrame.canEditSource())
                //sourceFrame.setSelection(TextRange.createFromLocation(uiLocation.lineNumber, 0));
            sourceFrame.focus();
        },

        _callFrameSelected: function(event)
        {
            var callFrame = event.data;

            if (!callFrame)
                return;

            //this.sidebarPanes.scopechain.update(callFrame);
            this.sidebarPanes.watchExpressions.refreshExpressions();
            this.sidebarPanes.callstack.setSelectedCallFrame(callFrame);
            callFrame.createLiveLocation(this._executionLineChanged.bind(this));
        },

        _editorClosed: function(event)
        {
            this._navigatorController.hideNavigatorOverlay();
            var uiSourceCode = /** @type {UISourceCode} */ (event.data);

            if (this._currentUISourceCode === uiSourceCode)
                delete this._currentUISourceCode;

            // ScriptsNavigator does not need to update on EditorClosed.
            this._updateScriptViewStatusBarItems();
            //x WebInspector.searchController.resetSearch();
        },

        _editorSelected: function(event)
        {
            var uiSourceCode = /** @type {UISourceCode} */ (event.data);
            var sourceFrame = this._showFile(uiSourceCode);
            this._navigatorController.hideNavigatorOverlay();
            if (!this._navigatorController.isNavigatorPinned())
                sourceFrame.focus();
            //x WebInspector.searchController.resetSearch();
        },

        _scriptSelected: function(event)
        {
            var uiSourceCode = /** @type {UISourceCode} */ (event.data.uiSourceCode);
            var sourceFrame = this._showFile(uiSourceCode);
            this._navigatorController.hideNavigatorOverlay();
            if (sourceFrame && (!this._navigatorController.isNavigatorPinned() || event.data.focusSource))
                sourceFrame.focus();
        },

        _updateDebuggerButtons: function()
        {
            /*
            if (CSInspector.debugModel.debuggerEnabled()) {
                this._pauseOnExceptionButton.visible = true;
            } else {
                this._pauseOnExceptionButton.visible = false;
            }
            */

            if (this._paused) {
                this._updateButtonTitle(this._pauseButton, UIString("Resume script execution (%s)."))
                this._pauseButton.state = true;
                //this._pauseButton.setLongClickOptionsEnabled((function() { return [ this._longResumeButton ] }).bind(this));

                this._pauseButton.setEnabled(true);
                this._stepOverButton.setEnabled(true);
                this._stepIntoButton.setEnabled(true);
                this._stepOutButton.setEnabled(true);

                this.debuggerStatusElement.textContent = UIString("Paused");
            } else {
                this._updateButtonTitle(this._pauseButton, UIString("Pause script execution (%s)."))
                this._pauseButton.state = false;
                //this._pauseButton.setLongClickOptionsEnabled(null);

                this._pauseButton.setEnabled(!this._waitingToPause);
                this._stepOverButton.setEnabled(false);
                this._stepIntoButton.setEnabled(false);
                this._stepOutButton.setEnabled(false);

                if (this._waitingToPause)
                    this.debuggerStatusElement.textContent = UIString("Pausing");
                else if (this._stepping)
                    this.debuggerStatusElement.textContent = UIString("Stepping");
                else
                    this.debuggerStatusElement.textContent = "";
            }
        },

        _itemSearchStarted: function(event)
        {
            var searchText = /** @type {string} */ (event.data);
            //TODO OpenResourceDialog.show(this, this.editorView.mainElement, searchText);
        },

        /**
         * @param {boolean} show
         */
        _enableDebuggerSidebar: function(show)
        {
            this._toggleDebuggerSidebarButton.state = show ? "right" : "left";
            this._toggleDebuggerSidebarButton.title = show ? UIString("Hide debugger") : UIString("Show debugger");
            if (show)
                this.splitView.showSidebarElement();
            else
                this.splitView.hideSidebarElement();
            this._debugSidebarResizeWidgetElement.enableStyleClass("hidden", !show);
            CSInspector.settings.debuggerSidebarHidden.set(!show);
        },

        _installDebuggerSidebarController: function()
        {
            this._toggleDebuggerSidebarButton = new StatusBarButton("", "right-sidebar-show-hide-button scripts-debugger-show-hide-button", 3);
            this._toggleDebuggerSidebarButton.addEventListener("click", clickHandler, this);
            this.editorView.element.appendChild(this._toggleDebuggerSidebarButton.element);
            this._enableDebuggerSidebar(!CSInspector.settings.debuggerSidebarHidden.get());

            function clickHandler()
            {
                this._enableDebuggerSidebar(this._toggleDebuggerSidebarButton.state === "left");
            }
        },

        get visibleView()
        {
            return this._editorContainer.visibleView;
        },

        _updateScriptViewStatusBarItems: function()
        {
            this._scriptViewStatusBarItemsContainer.removeChildren();
            this._scriptViewStatusBarTextContainer.removeChildren();

            var sourceFrame = this.visibleView;
            if (sourceFrame) {
                var statusBarItems = sourceFrame.statusBarItems() || [];
                for (var i = 0; i < statusBarItems.length; ++i)
                    this._scriptViewStatusBarItemsContainer.appendChild(statusBarItems[i]);
                var statusBarText = sourceFrame.statusBarText();
                if (statusBarText)
                    this._scriptViewStatusBarTextContainer.appendChild(statusBarText);
            }
        },

        /**
         * @param {WebInspector.Event} event
         */
        _callFrameSelectedInSidebar: function(event)
        {
            var callFrame = /** @type {CallFrame} */ (event.data);
            delete this._skipExecutionLineRevealing;
            CSInspector.debugModel.setSelectedCallFrame(callFrame);
        },

        _clearInterface: function()
        {
            this.sidebarPanes.callstack.update(null);
            //this.sidebarPanes.scopechain.update(null);
            this.sidebarPanes.breakpoints.clearBreakpointHighlight();

            this._clearCurrentExecutionLine();
            this._updateDebuggerButtons();
        },

        _toggleBreakpointsClicked: function(event)
        {
            CSInspector.debugModel.setBreakpointsActive(!CSInspector.debugModel.breakpointsActive());
        },

        _breakpointsActiveStateChanged: function(event)
        {
            var active = event.data;
            this._toggleBreakpointsButton.toggled = !active;
            if (active) {
                this._toggleBreakpointsButton.title = UIString("Deactivate breakpoints.");
                CSInspector.inspectorView.element.removeStyleClass("breakpoints-deactivated");
                this.sidebarPanes.breakpoints.listElement.removeStyleClass("breakpoints-list-deactivated");
            } else {
                this._toggleBreakpointsButton.title = UIString("Activate breakpoints.");
                CSInspector.inspectorView.element.addStyleClass("breakpoints-deactivated");
                this.sidebarPanes.breakpoints.listElement.addStyleClass("breakpoints-list-deactivated");
            }
        },

        __proto__: Panel.prototype
    };

    ScriptsPanel.prototype._debuggerPaused = function(evt) {
        var details = CSInspector.debugModel.debuggerPausedDetails();

        this._paused = true;
        this._waitingToPause = false;
        this._stepping = false;

        this._updateDebuggerButtons();

        CSInspector.inspectorView.setCurrentPanel(this);
        this.sidebarPanes.callstack.update(details.callFrames);

        if (/*debug reason is exception*/ false){
        } else {
            function didGetUILocation(uiLocation)
            {
                var breakpoint = CSInspector.breakpointManager.findBreakpoint(uiLocation.uiSourceCode, uiLocation.lineNumber);
                if (!breakpoint)
                    return;
                this.sidebarPanes.breakpoints.highlightBreakpoint(breakpoint);
                this.sidebarPanes.callstack.setStatus(UIString("Paused on a breakpoint."));
            }
            if (details.callFrames.length)
                details.callFrames[0].createLiveLocation(didGetUILocation.bind(this));
            else
                console.warn("ScriptsPanel paused, but callFrames.length is zero."); // TODO remove this once we understand this case better
        }

        this._enableDebuggerSidebar(true);
        this._toggleDebuggerSidebarButton.setEnabled(false);
        window.focus();
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
            CSInspector.debugAgent.resume();
        } else {
            this._stepping = false;
            this._waitingToPause = true;
            // Make sure pauses didn't stick skipped.
            CSInspector.debugAgent.setSkipAllPauses(false);
            CSInspector.debugAgent.pause();
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

        CSInspector.debugAgent.stepOver();
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

        CSInspector.debugAgent.stepInto();
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

        CSInspector.debugAgent.stepOut();
        return true;
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
        var sourceFrame = new ScriptSourceFrame(this, uiSourceCode);
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
        this._updateScriptViewStatusBarItems();

        return sourceFrame;
    }

    return ScriptsPanel;
});
