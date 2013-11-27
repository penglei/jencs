define(function(require){
    var View = require("View");
    var StatusBarText = require("StatusBarButton").StatusBarText;
    var CodeMirrorTextEditor = require("CodeMirrorTextEditor");
    var TextEditorDelegate = require("TextEditor").TextEditorDelegate;
    var TextEditor = require("TextEditor").TextEditor;
    var ParsedURL = require("ParsedURL");

    function SourceFrame(scriptsPanel, uiSourceCode)
    {
        View.call(this);
        this.element.addStyleClass("script-view");
        this.element.addStyleClass("fill");

        this._scriptsPanel = scriptsPanel;

        this._url = uiSourceCode.contentURL();
        this._contentProvider = uiSourceCode;

        var textEditorDelegate = new TextEditorDelegateForSourceFrame(this);

        this._textEditor = new CodeMirrorTextEditor(this._url, textEditorDelegate);

        this._searchResults = [];

        this._messages = [];
        this._rowMessages = {};
        this._messageBubbles = {};

        //this._textEditor.setReadOnly(true);

        this._sourcePosition = new StatusBarText("", "source-frame-cursor-position");
    }

    SourceFrame.Events = {
        ScrollChanged: "ScrollChanged",
        SelectionChanged: "SelectionChanged"
    }

    SourceFrame.prototype = {
        /**
         * @param {number} key
         * @param {function()} handler
         */
        addShortcut: function(key, handler)
        {
            this._shortcuts[key] = handler;
        },

        wasShown: function()
        {
            this._ensureContentLoaded();
            this._textEditor.show(this.element);
            this._editorAttached = true;
            this._wasShownOrLoaded();
        },

        /**
         * @return {boolean}
         */
        _isEditorShowing: function()
        {
            return this.isShowing() && this._editorAttached;
        },

        willHide: function()
        {
            View.prototype.willHide.call(this);

            this._clearPositionHighlight();
            this._clearLineToReveal();
        },

        /**
         * @return {?Element}
         */
        statusBarText: function()
        {
            return this._sourcePosition.element;
        },

        /**
         * @return {Array.<Element>}
         */
        statusBarItems: function()
        {
            return [];
        },

        defaultFocusedElement: function()
        {
            return this._textEditor.defaultFocusedElement();
        },

        get loaded()
        {
            return this._loaded;
        },

        hasContent: function()
        {
            return true;
        },

        get textEditor()
        {
            return this._textEditor;
        },

        _ensureContentLoaded: function()
        {
            /*可实现动态获取文件内容
            if (!this._contentRequested) {
                this._contentRequested = true;
                this._contentProvider.requestContent(this.setContent.bind(this));
            }
            */
            this.setContent(this._contentProvider.content());
        },

        addMessage: function(msg)
        {
            this._messages.push(msg);
            if (this.loaded)
                this.addMessageToSource(msg.line - 1, msg);
        },

        clearMessages: function()
        {
            for (var line in this._messageBubbles) {
                var bubble = this._messageBubbles[line];
                var lineNumber = parseInt(line, 10);
                this._textEditor.removeDecoration(lineNumber, bubble);
            }

            this._messages = [];
            this._rowMessages = {};
            this._messageBubbles = {};
        },

        /**
         * @override
         */
        canHighlightPosition: function()
        {
            return true;
        },

        /**
         * @override
         */
        highlightPosition: function(line, column)
        {
            this._clearLineToReveal();
            this._clearLineToScrollTo();
            this._clearSelectionToSet();
            this._positionToHighlight = { line: line, column: column };
            this._innerHighlightPositionIfNeeded();
        },

        _innerHighlightPositionIfNeeded: function()
        {
            if (!this._positionToHighlight)
                return;

            if (!this.loaded || !this._isEditorShowing())
                return;

            this._textEditor.highlightPosition(this._positionToHighlight.line, this._positionToHighlight.column);
            delete this._positionToHighlight;
        },

        _clearPositionHighlight: function()
        {
            this._textEditor.clearPositionHighlight();
            delete this._positionToHighlight;
        },

        /**
         * @param {number} line
         */
        revealLine: function(line)
        {
            this._clearPositionHighlight();
            this._clearLineToScrollTo();
            this._clearSelectionToSet();
            this._lineToReveal = line;
            this._innerRevealLineIfNeeded();
        },

        _innerRevealLineIfNeeded: function()
        {
            if (typeof this._lineToReveal === "number") {
                if (this.loaded && this._isEditorShowing()) {
                    this._textEditor.revealLine(this._lineToReveal);
                    delete this._lineToReveal;
                }
            }
        },

        _clearLineToReveal: function()
        {
            delete this._lineToReveal;
        },

        /**
         * @param {number} line
         */
        scrollToLine: function(line)
        {
            this._clearPositionHighlight();
            this._clearLineToReveal();
            this._lineToScrollTo = line;
            this._innerScrollToLineIfNeeded();
        },

        _innerScrollToLineIfNeeded: function()
        {
            if (typeof this._lineToScrollTo === "number") {
                if (this.loaded && this._isEditorShowing()) {
                    this._textEditor.scrollToLine(this._lineToScrollTo);
                    delete this._lineToScrollTo;
                }
            }
        },

        _clearLineToScrollTo: function()
        {
            delete this._lineToScrollTo;
        },

        /**
         * @param {TextRange} textRange
         */
        setSelection: function(textRange)
        {
            this._selectionToSet = textRange;
            this._innerSetSelectionIfNeeded();
        },

        _innerSetSelectionIfNeeded: function()
        {
            if (this._selectionToSet && this.loaded && this._isEditorShowing()) {
                this._textEditor.setSelection(this._selectionToSet);
                delete this._selectionToSet;
            }
        },

        _clearSelectionToSet: function()
        {
            delete this._selectionToSet;
        },

        _wasShownOrLoaded: function()
        {
            this._innerHighlightPositionIfNeeded();
            this._innerRevealLineIfNeeded();
            this._innerSetSelectionIfNeeded();
            this._innerScrollToLineIfNeeded();
        },

        onTextChanged: function(oldRange, newRange)
        {
            if (!this._isReplacing)
                searchController.cancelSearch();
            this.clearMessages();
        },

        _simplifyMimeType: function(content, mimeType)
        {
            if (!mimeType)
                return "";
            if (mimeType.indexOf("javascript") >= 0 ||
                mimeType.indexOf("jscript") >= 0 ||
                mimeType.indexOf("ecmascript") >= 0)
                return "text/javascript";
            // A hack around the fact that files with "php" extension might be either standalone or html embedded php scripts.
            if (mimeType === "text/x-php" && content.match(/\<\?.*\?\>/g))
                return "application/x-httpd-php";
            return mimeType;
        },

        /**
         * @param {?string} content
         * @param {string} mimeType
         */
        setContent: function(content, mimeType)
        {
            if (!this._loaded) {
                this._loaded = true;
                this._textEditor.setText(content || "");
                this._textEditor.markClean();
            } else {
                var firstLine = this._textEditor.firstVisibleLine();
                var selection = this._textEditor.selection();
                this._textEditor.setText(content || "");
                this._textEditor.scrollToLine(firstLine);
                this._textEditor.setSelection(selection);
            }

            this._textEditor.setMimeType(this._simplifyMimeType(content, mimeType) || 'text/clearsilver');

            this._textEditor.beginUpdates();

            this._setTextEditorDecorations();

            this._wasShownOrLoaded();

            if (this._delayedFindSearchMatches) {
                this._delayedFindSearchMatches();
                delete this._delayedFindSearchMatches;
            }

            this.onTextEditorContentLoaded();

            this._textEditor.endUpdates();
        },

        onTextEditorContentLoaded: function() {},

        _setTextEditorDecorations: function()
        {
            this._rowMessages = {};
            this._messageBubbles = {};

            this._textEditor.beginUpdates();

            this._addExistingMessagesToSource();

            this._textEditor.endUpdates();
        },

        /**
         * @param {string} query
         * @param {boolean} shouldJump
         * @param {function(View, number)} callback
         * @param {function(number)=} currentMatchChangedCallback
         */
        performSearch: function(query, shouldJump, callback, currentMatchChangedCallback)
        {
            function doFindSearchMatches(query)
            {
                this._currentSearchResultIndex = -1;
                this._searchResults = [];

                var regex = SourceFrame.createSearchRegex(query);
                this._searchRegex = regex;
                this._searchResults = this._collectRegexMatches(regex);
                if (!this._searchResults.length)
                    this._textEditor.cancelSearchResultsHighlight();
                else if (shouldJump)
                    this.jumpToNextSearchResult();
                else
                    this._textEditor.highlightSearchResults(regex, null);
                callback(this, this._searchResults.length);
            }

            this._resetSearch();
            this._currentSearchMatchChangedCallback = currentMatchChangedCallback;
            if (this.loaded)
                doFindSearchMatches.call(this, query);
            else
                this._delayedFindSearchMatches = doFindSearchMatches.bind(this, query);

            this._ensureContentLoaded();
        },

        _editorFocused: function()
        {
            if (!this._searchResults.length)
                return;
            this._currentSearchResultIndex = -1;
            if (this._currentSearchMatchChangedCallback)
                this._currentSearchMatchChangedCallback(this._currentSearchResultIndex);
            this._textEditor.highlightSearchResults(this._searchRegex, null);
        },

        _searchResultAfterSelectionIndex: function(selection)
        {
            if (!selection)
                return 0;
            for (var i = 0; i < this._searchResults.length; ++i) {
                if (this._searchResults[i].compareTo(selection) >= 0)
                    return i;
            }
            return 0;
        },

        _resetSearch: function()
        {
            delete this._delayedFindSearchMatches;
            delete this._currentSearchMatchChangedCallback;
            this._currentSearchResultIndex = -1;
            this._searchResults = [];
            delete this._searchRegex;
        },

        searchCanceled: function()
        {
            var range = this._currentSearchResultIndex !== -1 ? this._searchResults[this._currentSearchResultIndex] : null;
            this._resetSearch();
            if (!this.loaded)
                return;
            this._textEditor.cancelSearchResultsHighlight();
            if (range)
                this._textEditor.setSelection(range);
        },

        hasSearchResults: function()
        {
            return this._searchResults.length > 0;
        },

        jumpToFirstSearchResult: function()
        {
            this.jumpToSearchResult(0);
        },

        jumpToLastSearchResult: function()
        {
            this.jumpToSearchResult(this._searchResults.length - 1);
        },

        jumpToNextSearchResult: function()
        {
            var currentIndex = this._searchResultAfterSelectionIndex(this._textEditor.selection());
            var nextIndex = this._currentSearchResultIndex === -1 ? currentIndex : currentIndex + 1;
            this.jumpToSearchResult(nextIndex);
        },

        jumpToPreviousSearchResult: function()
        {
            var currentIndex = this._searchResultAfterSelectionIndex(this._textEditor.selection());
            this.jumpToSearchResult(currentIndex - 1);
        },

        showingFirstSearchResult: function()
        {
            return this._searchResults.length &&  this._currentSearchResultIndex === 0;
        },

        showingLastSearchResult: function()
        {
            return this._searchResults.length && this._currentSearchResultIndex === (this._searchResults.length - 1);
        },

        get currentSearchResultIndex()
        {
            return this._currentSearchResultIndex;
        },

        jumpToSearchResult: function(index)
        {
            if (!this.loaded || !this._searchResults.length)
                return;
            this._currentSearchResultIndex = (index + this._searchResults.length) % this._searchResults.length;
            if (this._currentSearchMatchChangedCallback)
                this._currentSearchMatchChangedCallback(this._currentSearchResultIndex);
            this._textEditor.highlightSearchResults(this._searchRegex, this._searchResults[this._currentSearchResultIndex]);
        },

        /**
         * @param {string} text
         */
        replaceSearchMatchWith: function(text)
        {
            var range = this._searchResults[this._currentSearchResultIndex];
            if (!range)
                return;
            this._textEditor.highlightSearchResults(this._searchRegex, null);

            this._isReplacing = true;
            var newRange = this._textEditor.editRange(range, text);
            delete this._isReplacing;

            this._textEditor.setSelection(newRange.collapseToEnd());
        },

        /**
         * @param {string} query
         * @param {string} replacement
         */
        replaceAllWith: function(query, replacement)
        {
            this._textEditor.highlightSearchResults(this._searchRegex, null);

            var text = this._textEditor.text();
            var range = this._textEditor.range();
            var regex = SourceFrame.createSearchRegex(query, "g");
            if (regex.__fromRegExpQuery)
                text = text.replace(regex, replacement);
            else
                text = text.replace(regex, function() { return replacement; });

            this._isReplacing = true;
            this._textEditor.editRange(range, text);
            delete this._isReplacing;
        },

        _collectRegexMatches: function(regexObject)
        {
            var ranges = [];
            for (var i = 0; i < this._textEditor.linesCount; ++i) {
                var line = this._textEditor.line(i);
                var offset = 0;
                do {
                    var match = regexObject.exec(line);
                    if (match) {
                        if (match[0].length)
                            ranges.push(new TextRange(i, offset + match.index, i, offset + match.index + match[0].length));
                        offset += match.index + 1;
                        line = line.substring(match.index + 1);
                    }
                } while (match && line);
            }
            return ranges;
        },

        _addExistingMessagesToSource: function()
        {
            var length = this._messages.length;
            for (var i = 0; i < length; ++i)
                this.addMessageToSource(this._messages[i].line - 1, this._messages[i]);
        },

        /**
         * @param {number} lineNumber
         * @param {ConsoleMessage} msg
         */
        addMessageToSource: function(lineNumber, msg)
        {
            if (lineNumber >= this._textEditor.linesCount)
                lineNumber = this._textEditor.linesCount - 1;
            if (lineNumber < 0)
                lineNumber = 0;

            var rowMessages = this._rowMessages[lineNumber];
            if (!rowMessages) {
                rowMessages = [];
                this._rowMessages[lineNumber] = rowMessages;
            }

            for (var i = 0; i < rowMessages.length; ++i) {
                if (rowMessages[i].consoleMessage.isEqual(msg)) {
                    rowMessages[i].repeatCount = msg.totalRepeatCount;
                    this._updateMessageRepeatCount(rowMessages[i]);
                    return;
                }
            }

            var rowMessage = { consoleMessage: msg };
            rowMessages.push(rowMessage);

            this._textEditor.beginUpdates();
            var messageBubbleElement = this._messageBubbles[lineNumber];
            if (!messageBubbleElement) {
                messageBubbleElement = document.createElement("div");
                messageBubbleElement.className = "webkit-html-message-bubble";
                this._messageBubbles[lineNumber] = messageBubbleElement;
                this._textEditor.addDecoration(lineNumber, messageBubbleElement);
            }

            var imageElement = document.createElement("div");
            switch (msg.level) {
                case ConsoleMessage.MessageLevel.Error:
                    messageBubbleElement.addStyleClass("webkit-html-error-message");
                    imageElement.className = "error-icon-small";
                    break;
                case ConsoleMessage.MessageLevel.Warning:
                    messageBubbleElement.addStyleClass("webkit-html-warning-message");
                    imageElement.className = "warning-icon-small";
                    break;
            }

            var messageLineElement = document.createElement("div");
            messageLineElement.className = "webkit-html-message-line";
            messageBubbleElement.appendChild(messageLineElement);

            // Create the image element in the Inspector's document so we can use relative image URLs.
            messageLineElement.appendChild(imageElement);
            messageLineElement.appendChild(document.createTextNode(msg.message));

            rowMessage.element = messageLineElement;
            rowMessage.repeatCount = msg.totalRepeatCount;
            this._updateMessageRepeatCount(rowMessage);
            this._textEditor.endUpdates();
        },

        _updateMessageRepeatCount: function(rowMessage)
        {
            if (rowMessage.repeatCount < 2)
                return;

            if (!rowMessage.repeatCountElement) {
                var repeatCountElement = document.createElement("span");
                rowMessage.element.appendChild(repeatCountElement);
                rowMessage.repeatCountElement = repeatCountElement;
            }

            rowMessage.repeatCountElement.textContent = UIString(" (repeated %d times)", rowMessage.repeatCount);
        },

        /**
         * @param {number} lineNumber
         * @param {ConsoleMessage} msg
         */
        removeMessageFromSource: function(lineNumber, msg)
        {
            if (lineNumber >= this._textEditor.linesCount)
                lineNumber = this._textEditor.linesCount - 1;
            if (lineNumber < 0)
                lineNumber = 0;

            var rowMessages = this._rowMessages[lineNumber];
            for (var i = 0; rowMessages && i < rowMessages.length; ++i) {
                var rowMessage = rowMessages[i];
                if (rowMessage.consoleMessage !== msg)
                    continue;

                var messageLineElement = rowMessage.element;
                var messageBubbleElement = messageLineElement.parentElement;
                messageBubbleElement.removeChild(messageLineElement);
                rowMessages.remove(rowMessage);
                if (!rowMessages.length)
                    delete this._rowMessages[lineNumber];
                if (!messageBubbleElement.childElementCount) {
                    this._textEditor.removeDecoration(lineNumber, messageBubbleElement);
                    delete this._messageBubbles[lineNumber];
                }
                break;
            }
        },

        populateLineGutterContextMenu: function(contextMenu, lineNumber)
        {
        },

        populateTextAreaContextMenu: function(contextMenu, lineNumber)
        {
        },

        inheritScrollPositions: function(sourceFrame)
        {
            this._textEditor.inheritScrollPositions(sourceFrame._textEditor);
        },

        /**
         * @return {boolean}
         */
        canEditSource: function()
        {
            return false;
        },

        /**
         * @param {string} text 
         */
        commitEditing: function(text)
        {
        },

        /**
         * @param {TextRange} textRange
         */
        selectionChanged: function(textRange)
        {
            this._updateSourcePosition(textRange);
            this.dispatchEventToListeners(SourceFrame.Events.SelectionChanged, textRange);
        },

        /**
         * @param {TextRange} textRange
         */
        _updateSourcePosition: function(textRange)
        {
            if (!textRange)
                return;

            if (textRange.isEmpty()) {
                this._sourcePosition.setText(UIString("Line %d, Column %d", textRange.endLine + 1, textRange.endColumn + 1));
                return;
            }
            textRange = textRange.normalize();

            var selectedText = this._textEditor.copyRange(textRange);
            if (textRange.startLine === textRange.endLine)
                this._sourcePosition.setText(UIString("%d characters selected", selectedText.length));
            else
                this._sourcePosition.setText(UIString("%d lines, %d characters selected", textRange.endLine - textRange.startLine + 1, selectedText.length));
        },

        /**
         * @param {number} lineNumber
         */
        scrollChanged: function(lineNumber)
        {
            this.dispatchEventToListeners(SourceFrame.Events.ScrollChanged, lineNumber);
        },

        _handleKeyDown: function(e)
        {
            var shortcutKey = KeyboardShortcut.makeKeyFromEvent(e);
            var handler = this._shortcuts[shortcutKey];
            if (handler && handler())
                e.consume(true);
        },

        _commitEditing: function()
        {
            if (this._textEditor.readOnly())
                return false;

            var content = this._textEditor.text();
            this.commitEditing(content);
            return true;
        },

        __proto__: View.prototype
    }



    /**
     * @implements {TextEditorDelegate}
     * @constructor
     */
    TextEditorDelegateForSourceFrame = function(sourceFrame)
    {
        this._sourceFrame = sourceFrame;
    }

    TextEditorDelegateForSourceFrame.prototype = {
        onTextChanged: function(oldRange, newRange)
        {
            this._sourceFrame.onTextChanged(oldRange, newRange);
        },

        /**
         * @param {TextRange} textRange
         */
        selectionChanged: function(textRange)
        {
            this._sourceFrame.selectionChanged(textRange);
        },

        /**
         * @param {number} lineNumber
         */
        scrollChanged: function(lineNumber)
        {
            this._sourceFrame.scrollChanged(lineNumber);
        },

        editorFocused: function()
        {
            this._sourceFrame._editorFocused();
        },

        populateLineGutterContextMenu: function(contextMenu, lineNumber)
        {
            this._sourceFrame.populateLineGutterContextMenu(contextMenu, lineNumber);
        },

        populateTextAreaContextMenu: function(contextMenu, lineNumber)
        {
            this._sourceFrame.populateTextAreaContextMenu(contextMenu, lineNumber);
        },

        /**
         * @param {string} hrefValue
         * @param {boolean} isExternal
         * @return {Element}
         */
        createLink: function(hrefValue, isExternal)
        {
            //ResourceUtil.js
            /*
            var targetLocation = ParsedURL.completeURL(this._sourceFrame._url, hrefValue);
            return WebInspector.linkifyURLAsNode(targetLocation || hrefValue, hrefValue, undefined, isExternal);
            */
        },

        __proto__: TextEditorDelegate.prototype
    }
    return SourceFrame;
});
