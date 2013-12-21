define(function(require) {
    var View = require("View");
    var TextUtils = require("TextUtils");
    var TextRange = require("TextRange");
    var TextEditor= require("TextEditor").TextEditor;

    /**
     * @constructor
     * @extends {View}
     * @implements {TextEditor}
     * @param {?string} url
     * @param {TextEditorDelegate} delegate
     */
    function CodeMirrorTextEditor(url, delegate){
        View.call(this);

        this._delegate = delegate;
        this._url = url;

        this._lineSeparator = "\n";

        this._codeMirror = window.CodeMirror(this.element, {
            cursorBlinkRate: 0, //0 is disabled
            //theme: "monokai",
            readOnly: true,
            lineNumbers: true,
            gutters: ["CodeMirror-linenumbers"],
            matchBrackets: true,
            smartIndent: false,
            styleSelectedText: true,
            electricChars: false,
            autoCloseBrackets: { explode: false }
        });
        this._codeMirror._codeMirrorTextEditor = this;

        this._codeMirror.setOption("flattenSpans", false);
        this._codeMirror.setOption("maxHighlightLength", 1000);
        this._codeMirror.setOption("mode", null);


        this._codeMirror.on("gutterClick", this._gutterClick.bind(this));
        this._codeMirror.on("scroll", this._scroll.bind(this));
        this._codeMirror.on("focus", this._focus.bind(this));

        this.element.addStyleClass("fill");
        this.element.style.overflow = "hidden";
        this.element.firstChild.addStyleClass("source-code");
        this.element.firstChild.addStyleClass("fill");

        this.element.addEventListener("focus", this._handleElementFocus.bind(this), false);

        this._setupSelectionColor();
    }

    CodeMirrorTextEditor.prototype = {
        wasShown: function()
        {
            this._codeMirror.refresh();
        },

        _guessIndentationLevel: function()
        {
            var tabRegex = /^\t+/;
            var tabLines = 0;
            var indents = {};
            function processLine(lineHandle)
            {
                var text = lineHandle.text;
                if (text.length === 0 || !TextUtils.isSpaceChar(text[0]))
                    return;
                if (tabRegex.test(text)) {
                    ++tabLines;
                    return;
                }
                var i = 0;
                while (i < text.length && TextUtils.isSpaceChar(text[i]))
                    ++i;
                if (i % 2 !== 0)
                    return;
                indents[i] = 1 + (indents[i] || 0);
            }
            this._codeMirror.eachLine(processLine);

            var onePercentFilterThreshold = this.linesCount / 100;
            if (tabLines && tabLines > onePercentFilterThreshold)
                return "\t";
            var minimumIndent = Infinity;
            for (var i in indents) {
                if (indents[i] < onePercentFilterThreshold)
                    continue;
                var indent = parseInt(i, 10);
                if (minimumIndent > indent)
                    minimumIndent = indent;
            }
            if (minimumIndent === Infinity)
                return TextUtils.Indent.FourSpaces;
            return new Array(minimumIndent + 1).join(" ");
        },

        _updateEditorIndentation: function()
        {
            var extraKeys = {};
            var indent = CSInspector.settings.textEditorIndent.get();
            if (CSInspector.settings.textEditorAutoDetectIndent.get())
                indent = this._guessIndentationLevel();
            if (indent === TextUtils.Indent.TabCharacter) {
                this._codeMirror.setOption("indentWithTabs", true);
                this._codeMirror.setOption("indentUnit", 4);
            } else {
                this._codeMirror.setOption("indentWithTabs", false);
                this._codeMirror.setOption("indentUnit", indent.length);
                extraKeys.Tab = function(codeMirror)
                {
                    if (codeMirror.somethingSelected())
                        return CodeMirror.Pass;
                    var pos = codeMirror.getCursor("head");
                    codeMirror.replaceRange(indent.substring(pos.ch % indent.length), codeMirror.getCursor());
                }
            }
            this._codeMirror.setOption("extraKeys", extraKeys);
            this._indentationLevel = indent;
        },

        /**
         * @return {string}
         */
        indent: function()
        {
            return this._indentationLevel;
        },

        /**
         * @param {!RegExp} regex
         * @param {TextRange} range
         */
        highlightSearchResults: function(regex, range)
        {
            function innerHighlightRegex()
            {
                if (range) {
                    this.revealLine(range.startLine);
                    this.setSelection(TextRange.createFromLocation(range.startLine, range.startColumn));
                } else {
                    // Collapse selection to end on search start so that we jump to next occurence on the first enter press.
                    this.setSelection(this.selection().collapseToEnd());
                }
                this._tokenHighlighter.highlightSearchResults(regex, range);
            }

            this._codeMirror.operation(innerHighlightRegex.bind(this));
        },

        cancelSearchResultsHighlight: function()
        {
            this._codeMirror.operation(this._tokenHighlighter.highlightSelectedTokens.bind(this._tokenHighlighter));
        },

        undo: function()
        {
            this._codeMirror.undo();
        },

        redo: function()
        {
            this._codeMirror.redo();
        },

        _setupSelectionColor: function()
        {
            if (CodeMirrorTextEditor._selectionStyleInjected)
                return;
            CodeMirrorTextEditor._selectionStyleInjected = true;
            var backgroundColor = "#6e86ff";//getSelectionBackgroundColor();
            var backgroundColorRule = backgroundColor ? ".CodeMirror .CodeMirror-selected { background-color: " + backgroundColor + ";}" : "";
            var foregroundColor = "#ffffff";//getSelectionForegroundColor();
            var foregroundColorRule = foregroundColor ? ".CodeMirror .CodeMirror-selectedtext:not(.CodeMirror-persist-highlight) { color: " + foregroundColor + "!important;}" : "";
            if (!foregroundColorRule && !backgroundColorRule)
                return;

            var style = document.createElement("style");
            style.textContent = backgroundColorRule + foregroundColorRule;
            document.head.appendChild(style);
        },

        _setupWhitespaceHighlight: function()
        {
            if (CodeMirrorTextEditor._whitespaceStyleInjected || !CSInspector.settings.showWhitespacesInEditor.get())
                return;
            CodeMirrorTextEditor._whitespaceStyleInjected = true;
            const classBase = ".cm-whitespace-";
            const spaceChar = "·";
            var spaceChars = "";
            var rules = "";
            for(var i = 1; i <= CodeMirrorTextEditor.MaximumNumberOfWhitespacesPerSingleSpan; ++i) {
                spaceChars += spaceChar;
                var rule = classBase + i + "::before { content: '" + spaceChars + "';}\n";
                rules += rule;
            }
            rules += ".cm-tab:before { display: block !important; }\n";
            var style = document.createElement("style");
            style.textContent = rules;
            document.head.appendChild(style);
        },

        _handleKeyDown: function(e)
        {
            if (this._autocompleteController.keyDown(e))
                e.consume(true);
        },

        _shouldProcessWordForAutocompletion: function(word)
        {
            return word.length && (word[0] < '0' || word[0] > '9');
        },

        /**
         * @param {string} text
         */
        _addTextToCompletionDictionary: function(text)
        {
            var words = TextUtils.textToWords(text);
            for(var i = 0; i < words.length; ++i) {
                if (this._shouldProcessWordForAutocompletion(words[i]))
                    this._dictionary.addWord(words[i]);
            }
        },

        /**
         * @param {string} text
         */
        _removeTextFromCompletionDictionary: function(text)
        {
            var words = TextUtils.textToWords(text);
            for(var i = 0; i < words.length; ++i) {
                if (this._shouldProcessWordForAutocompletion(words[i]))
                    this._dictionary.removeWord(words[i]);
            }
        },

        /**
         * @param {CompletionDictionary} dictionary
         */
        setCompletionDictionary: function(dictionary)
        {
            this._dictionary = dictionary;
            this._addTextToCompletionDictionary(this.text());
        },

        /**
         * @param {number} lineNumber
         * @param {number} column
         * @return {?{x: number, y: number, height: number}}
         */
        cursorPositionToCoordinates: function(lineNumber, column)
        {
            if (lineNumber >= this._codeMirror.lineCount() || lineNumber < 0 || column < 0 || column > this._codeMirror.getLine(lineNumber).length)
                return null;

            var metrics = this._codeMirror.cursorCoords(new CodeMirror.Pos(lineNumber, column));

            return {
                x: metrics.left,
                y: metrics.top,
                height: metrics.bottom - metrics.top
            };
        },

        /**
         * @param {number} x
         * @param {number} y
         * @return {?TextRange}
         */
        coordinatesToCursorPosition: function(x, y)
        {
            var element = document.elementFromPoint(x, y);
            if (!element || !element.isSelfOrDescendant(this._codeMirror.getWrapperElement()))
                return null;
            var gutterBox = this._codeMirror.getGutterElement().boxInWindow();
            if (x >= gutterBox.x && x <= gutterBox.x + gutterBox.width &&
                y >= gutterBox.y && y <= gutterBox.y + gutterBox.height)
                return null;
            var coords = this._codeMirror.coordsChar({left: x, top: y});
            return this._toRange(coords, coords);
        },

        /**
         * @param {number} lineNumber
         * @param {number} column
         * @return {?{startColumn: number, endColumn: number, type: string}}
         */
        tokenAtTextPosition: function(lineNumber, column)
        {
            if (lineNumber < 0 || lineNumber >= this._codeMirror.lineCount())
                return null;
            var token = this._codeMirror.getTokenAt(new CodeMirror.Pos(lineNumber, (column || 0) + 1));
            if (!token || !token.type)
                return null;
            var convertedType = CodeMirrorUtils.convertTokenType(token.type);
            if (!convertedType)
                return null;
            return {
                startColumn: token.start,
                endColumn: token.end - 1,
                type: convertedType
            };
        },

        /**
         * @param {TextRange} textRange
         * @return {string}
         */
        copyRange: function(textRange)
        {
            var pos = this._toPos(textRange.normalize());
            return this._codeMirror.getRange(pos.start, pos.end);
        },

        /**
         * @return {boolean}
         */
        isClean: function()
        {
            return this._codeMirror.isClean();
        },

        markClean: function()
        {
            this._codeMirror.markClean();
        },

        _hasLongLines: function()
        {
            function lineIterator(lineHandle)
            {
                if (lineHandle.text.length > CodeMirrorTextEditor.LongLineModeLineLengthThreshold)
                    hasLongLines = true;
                return hasLongLines;
            }
            var hasLongLines = false;
            this._codeMirror.eachLine(lineIterator);
            return hasLongLines;
        },

        /**
         * @param {string} mimeType
         * @return {string}
         */
        _whitespaceOverlayMode: function(mimeType)
        {
            var modeName = CodeMirror.mimeModes[mimeType] + "+whitespaces";
            if (CodeMirror.modes[modeName])
                return modeName;

            function modeConstructor(config, parserConfig)
            {
                function nextToken(stream)
                {
                    if (stream.peek() === " ") {
                        var spaces = 0;
                        while (spaces < CodeMirrorTextEditor.MaximumNumberOfWhitespacesPerSingleSpan && stream.peek() === " ") {
                            ++spaces;
                            stream.next();
                        }
                        return "whitespace whitespace-" + spaces;
                    }
                    while (!stream.eol() && stream.peek() !== " ")
                        stream.next();
                    return null;
                }
                var whitespaceMode = {
                    token: nextToken
                };
                return CodeMirror.overlayMode(CodeMirror.getMode(config, mimeType), whitespaceMode, false);
            }
            CodeMirror.defineMode(modeName, modeConstructor);
            return modeName;
        },

        _enableLongLinesMode: function()
        {
            this._codeMirror.setOption("styleSelectedText", false);
            this._longLinesMode = true;
        },

        _disableLongLinesMode: function()
        {
            this._codeMirror.setOption("styleSelectedText", true);
            this._longLinesMode = false;
        },

        _updateCodeMirrorMode: function()
        {
            var showWhitespaces = CSInspector.settings.showWhitespacesInEditor.get();
            this._codeMirror.setOption("mode", showWhitespaces ? this._whitespaceOverlayMode(this._mimeType) : this._mimeType);
        },

        /**
         * @param {string} mimeType
         */
        setMimeType: function(mimeType)
        {
            this._mimeType = mimeType;
            if (this._hasLongLines())
                this._enableLongLinesMode();
            else
                this._disableLongLinesMode();
            this._updateCodeMirrorMode();
        },

        /**
         * @param {boolean} readOnly
         */
        setReadOnly: function(readOnly)
        {
            this.element.enableStyleClass("CodeMirror-readonly", readOnly)
            this._codeMirror.setOption("readOnly", readOnly);
        },

        /**
         * @return {boolean}
         */
        readOnly: function()
        {
            return !!this._codeMirror.getOption("readOnly");
        },

        /**
         * @param {Object} highlightDescriptor
         */
        removeHighlight: function(highlightDescriptor)
        {
            highlightDescriptor.clear();
        },

        /**
         * @param {TextRange} range
         * @param {string} cssClass
         * @return {Object}
         */
        highlightRange: function(range, cssClass)
        {
            cssClass = "CodeMirror-persist-highlight " + cssClass;
            var pos = this._toPos(range);
            ++pos.end.ch;
            return this._codeMirror.markText(pos.start, pos.end, {
                className: cssClass,
                startStyle: cssClass + "-start",
                endStyle: cssClass + "-end"
            });
        },

        /**
         * @param {string} regex
         * @param {string} cssClass
         * @return {Object}
         */
        highlightRegex: function(regex, cssClass) { },

        /**
         * @return {Element}
         */
        defaultFocusedElement: function()
        {
            return this.element;
        },

        focus: function()
        {
            this._codeMirror.focus();
        },

        _handleElementFocus: function()
        {
            this._codeMirror.focus();
        },

        beginUpdates: function()
        {
            ++this._nestedUpdatesCounter;
        },

        endUpdates: function()
        {
            if (!--this._nestedUpdatesCounter)
                this._codeMirror.refresh();
        },

        /**
         * @param {number} lineNumber
         */
        revealLine: function(lineNumber)
        {
            this._innerRevealLine(lineNumber, this._codeMirror.getScrollInfo());
        },

        /**
         * @param {number} lineNumber
         * @param {{left: number, top: number, width: number, height: number, clientWidth: number, clientHeight: number}} scrollInfo
         */
        _innerRevealLine: function(lineNumber, scrollInfo)
        {
            var topLine = this._codeMirror.lineAtHeight(scrollInfo.top, "local");
            var bottomLine = this._codeMirror.lineAtHeight(scrollInfo.top + scrollInfo.clientHeight, "local");
            var linesPerScreen = bottomLine - topLine + 1;
            if (lineNumber < topLine) {
                var topLineToReveal = Math.max(lineNumber - (linesPerScreen / 2) + 1, 0) | 0;
                this._codeMirror.scrollIntoView(new CodeMirror.Pos(topLineToReveal, 0));
            } else if (lineNumber > bottomLine) {
                var bottomLineToReveal = Math.min(lineNumber + (linesPerScreen / 2) - 1, this.linesCount - 1) | 0;
                this._codeMirror.scrollIntoView(new CodeMirror.Pos(bottomLineToReveal, 0));
            }
        },

        _gutterClick: function(instance, lineNumber, gutter, event)
        {
            this.dispatchEventToListeners(TextEditor.Events.GutterClick, { lineNumber: lineNumber, event: event });
        },

        _contextMenu: function(event)
        {
            var contextMenu = new ContextMenu(event);
            var target = event.target.enclosingNodeOrSelfWithClass("CodeMirror-gutter-elt");
            if (target)
                this._delegate.populateLineGutterContextMenu(contextMenu, parseInt(target.textContent, 10) - 1);
            else
                this._delegate.populateTextAreaContextMenu(contextMenu, 0);
            contextMenu.show();
        },

        /**
         * @param {number} lineNumber
         * @param {boolean} disabled
         * @param {boolean} conditional
         */
        addBreakpoint: function(lineNumber, disabled, conditional)
        {
            if (lineNumber < 0 || lineNumber >= this._codeMirror.lineCount())
                return;
            var className = "cm-breakpoint" + (conditional ? " cm-breakpoint-conditional" : "") + (disabled ? " cm-breakpoint-disabled" : "");
            this._codeMirror.addLineClass(lineNumber, "wrap", className);
        },

        /**
         * @param {number} lineNumber
         */
        removeBreakpoint: function(lineNumber)
        {
            if (lineNumber < 0 || lineNumber >= this._codeMirror.lineCount())
                return;
            var wrapClasses = this._codeMirror.getLineHandle(lineNumber).wrapClass;
            if (!wrapClasses)
                return;
            var classes = wrapClasses.split(" ");
            for(var i = 0; i < classes.length; ++i) {
                if (classes[i].startsWith("cm-breakpoint"))
                    this._codeMirror.removeLineClass(lineNumber, "wrap", classes[i]);
            }
        },

        /**
         * @param {number} lineNumber
         */
        setExecutionLine: function(lineNumber)
        {
            this._executionLine = this._codeMirror.getLineHandle(lineNumber);
            this._codeMirror.addLineClass(this._executionLine, "wrap", "cm-execution-line");
        },

        clearExecutionLine: function()
        {
            if (this._executionLine)
                this._codeMirror.removeLineClass(this._executionLine, "wrap", "cm-execution-line");
            delete this._executionLine;
        },

        /**
         * @param {number} lineNumber
         * @param {Element} element
         */
        addDecoration: function(lineNumber, element)
        {
            var widget = this._codeMirror.addLineWidget(lineNumber, element);
            this._elementToWidget.put(element, widget);
        },

        /**
         * @param {number} lineNumber
         * @param {Element} element
         */
        removeDecoration: function(lineNumber, element)
        {
            var widget = this._elementToWidget.remove(element);
            if (widget)
                this._codeMirror.removeLineWidget(widget);
        },

        /**
         * @param {number} lineNumber
         * @param {number=} columnNumber
         */
        highlightPosition: function(lineNumber, columnNumber)
        {
            if (lineNumber < 0)
                return;
            lineNumber = Math.min(lineNumber, this._codeMirror.lineCount() - 1);
            if (typeof columnNumber !== "number" || columnNumber < 0 || columnNumber > this._codeMirror.getLine(lineNumber).length)
                columnNumber = 0;

            this.clearPositionHighlight();
            this._highlightedLine = this._codeMirror.getLineHandle(lineNumber);
            if (!this._highlightedLine)
              return;
            this.revealLine(lineNumber);
            this._codeMirror.addLineClass(this._highlightedLine, null, "cm-highlight");
            this._clearHighlightTimeout = setTimeout(this.clearPositionHighlight.bind(this), 2000);
            if (!this.readOnly())
                this._codeMirror.setSelection(new CodeMirror.Pos(lineNumber, columnNumber));
        },

        clearPositionHighlight: function()
        {
            if (this._clearHighlightTimeout)
                clearTimeout(this._clearHighlightTimeout);
            delete this._clearHighlightTimeout;

             if (this._highlightedLine)
                this._codeMirror.removeLineClass(this._highlightedLine, null, "cm-highlight");
            delete this._highlightedLine;
        },

        /**
         * @return {Array.<Element>}
         */
        elementsToRestoreScrollPositionsFor: function()
        {
            return [];
        },

        /**
         * @param {TextEditor} textEditor
         */
        inheritScrollPositions: function(textEditor)
        {
        },

        /**
         * @param {number} width
         * @param {number} height
         */
        _updatePaddingBottom: function(width, height)
        {
            var scrollInfo = this._codeMirror.getScrollInfo();
            var newPaddingBottom;
            var linesElement = this.element.firstChild.querySelector(".CodeMirror-lines");
            var lineCount = this._codeMirror.lineCount();
            if (lineCount <= 1)
                newPaddingBottom = 0;
            else
                newPaddingBottom = Math.max(scrollInfo.clientHeight - this._codeMirror.getLineHandle(this._codeMirror.lastLine()).height, 0);
            newPaddingBottom += "px";
            linesElement.style.paddingBottom = newPaddingBottom;
            this._codeMirror.setSize(width, height);
        },

        _resizeEditor: function()
        {
            var parentElement = this.element.parentElement;
            if (!parentElement || !this.isShowing())
                return;
            var scrollInfo = this._codeMirror.getScrollInfo();
            var width = parentElement.offsetWidth;
            var height = parentElement.offsetHeight;
            this._codeMirror.setSize(width, height);
            this._updatePaddingBottom(width, height);
            this._codeMirror.scrollTo(scrollInfo.left, scrollInfo.top);
        },

        onResize: function()
        {
            this._resizeEditor();
        },

        /**
         * @param {TextRange} range
         * @param {string} text
         * @return {TextRange}
         */
        editRange: function(range, text)
        {
            var pos = this._toPos(range);
            this._codeMirror.replaceRange(text, pos.start, pos.end);
            var newRange = this._toRange(pos.start, this._codeMirror.posFromIndex(this._codeMirror.indexFromPos(pos.start) + text.length));
            this._delegate.onTextChanged(range, newRange);
            if (CSInspector.settings.textEditorAutoDetectIndent.get())
                this._updateEditorIndentation();
            return newRange;
        },

        /**
         * @param {number} lineNumber
         * @param {number} column
         * @param {boolean=} prefixOnly
         * @return {?TextRange}
         */
        _wordRangeForCursorPosition: function(lineNumber, column, prefixOnly)
        {
            var line = this.line(lineNumber);
            if (column === 0 || !TextUtils.isWordChar(line.charAt(column - 1)))
                return null;
            var wordStart = column - 1;
            while(wordStart > 0 && TextUtils.isWordChar(line.charAt(wordStart - 1)))
                --wordStart;
            if (prefixOnly)
                return new TextRange(lineNumber, wordStart, lineNumber, column);
            var wordEnd = column;
            while(wordEnd < line.length && TextUtils.isWordChar(line.charAt(wordEnd)))
                ++wordEnd;
            return new TextRange(lineNumber, wordStart, lineNumber, wordEnd);
        },

        _beforeChange: function(codeMirror, changeObject)
        {
            if (!this._dictionary)
                return;
            this._updatedLines = this._updatedLines || {};
            for(var i = changeObject.from.line; i <= changeObject.to.line; ++i)
                this._updatedLines[i] = this.line(i);
        },

        /**
         * @param {CodeMirror} codeMirror
         * @param {{origin: string, text: Array.<string>, removed: Array.<string>}} changeObject
         */
        _change: function(codeMirror, changeObject)
        {
            // We do not show "scroll beyond end of file" span for one line documents, so we need to check if "document has one line" changed.
            var hasOneLine = this._codeMirror.lineCount() === 1;
            if (hasOneLine !== this._hasOneLine)
                this._resizeEditor();
            this._hasOneLine = hasOneLine;
            var widgets = this._elementToWidget.values();
            for (var i = 0; i < widgets.length; ++i)
                this._codeMirror.removeLineWidget(widgets[i]);
            this._elementToWidget.clear();

            if (this._updatedLines) {
                for(var lineNumber in this._updatedLines)
                    this._removeTextFromCompletionDictionary(this._updatedLines[lineNumber]);
                delete this._updatedLines;
            }

            var linesToUpdate = {};
            var singleCharInput = false;
            do {
                var oldRange = this._toRange(changeObject.from, changeObject.to);
                var newRange = oldRange.clone();
                var linesAdded = changeObject.text.length;
                singleCharInput = (changeObject.origin === "+input" && changeObject.text.length === 1 && changeObject.text[0].length === 1) ||
                    (changeObject.origin === "+delete" && changeObject.removed.length === 1 && changeObject.removed[0].length === 1);
                if (linesAdded === 0) {
                    newRange.endLine = newRange.startLine;
                    newRange.endColumn = newRange.startColumn;
                } else if (linesAdded === 1) {
                    newRange.endLine = newRange.startLine;
                    newRange.endColumn = newRange.startColumn + changeObject.text[0].length;
                } else {
                    newRange.endLine = newRange.startLine + linesAdded - 1;
                    newRange.endColumn = changeObject.text[linesAdded - 1].length;
                }

                if (!this._muteTextChangedEvent)
                    this._delegate.onTextChanged(oldRange, newRange);

                for(var i = newRange.startLine; i <= newRange.endLine; ++i) {
                    linesToUpdate[i] = true;
                }
                if (this._dictionary) {
                    for(var i = newRange.startLine; i <= newRange.endLine; ++i)
                        linesToUpdate[i] = this.line(i);
                }
            } while (changeObject = changeObject.next);
            if (this._dictionary) {
                for(var lineNumber in linesToUpdate)
                    this._addTextToCompletionDictionary(linesToUpdate[lineNumber]);
            }
            if (singleCharInput)
                this._autocompleteController.autocomplete();
        },

        _cursorActivity: function()
        {
            var start = this._codeMirror.getCursor("anchor");
            var end = this._codeMirror.getCursor("head");
            this._delegate.selectionChanged(this._toRange(start, end));
            if (!this._tokenHighlighter.highlightedRegex())
                this._codeMirror.operation(this._tokenHighlighter.highlightSelectedTokens.bind(this._tokenHighlighter));
        },

        _scroll: function()
        {
            if (this._scrollTimer)
                clearTimeout(this._scrollTimer);
            var topmostLineNumber = this._codeMirror.lineAtHeight(this._codeMirror.getScrollInfo().top, "local");
            this._scrollTimer = setTimeout(this._delegate.scrollChanged.bind(this._delegate, topmostLineNumber), 100);
        },

        _focus: function()
        {
            this._delegate.editorFocused();
        },

        _blur: function()
        {
            this._autocompleteController.finishAutocomplete();
        },

        /**
         * @param {number} lineNumber
         */
        scrollToLine: function(lineNumber)
        {
            var pos = new CodeMirror.Pos(lineNumber, 0);
            var coords = this._codeMirror.charCoords(pos, "local");
            this._codeMirror.scrollTo(0, coords.top);
        },

        /**
         * @return {number}
         */
        firstVisibleLine: function()
        {
            return this._codeMirror.lineAtHeight(this._codeMirror.getScrollInfo().top, "local");
        },

        /**
         * @return {number}
         */
        lastVisibleLine: function()
        {
            var scrollInfo = this._codeMirror.getScrollInfo();
            return this._codeMirror.lineAtHeight(scrollInfo.top + scrollInfo.clientHeight, "local");
        },

        /**
         * @return {TextRange}
         */
        selection: function()
        {
            var start = this._codeMirror.getCursor("anchor");
            var end = this._codeMirror.getCursor("head");

            return this._toRange(start, end);
        },

        /**
         * @return {TextRange?}
         */
        lastSelection: function()
        {
            return this._lastSelection;
        },

        /**
         * @param {TextRange} textRange
         */
        setSelection: function(textRange)
        {
            this._lastSelection = textRange;
            var pos = this._toPos(textRange);
            this._codeMirror.setSelection(pos.start, pos.end);
        },

        /**
         * @param {string} text
         */
        _detectLineSeparator: function(text)
        {
            this._lineSeparator = text.indexOf("\r\n") >= 0 ? "\r\n" : "\n";
        },

        /**
         * @param {string} text
         */
        setText: function(text)
        {
            this._muteTextChangedEvent = true;
            this._codeMirror.setValue(text);
            this._updateEditorIndentation();
            if (this._shouldClearHistory) {
                this._codeMirror.clearHistory();
                this._shouldClearHistory = false;
            }
            this._detectLineSeparator(text);
            delete this._muteTextChangedEvent;
        },

        /**
         * @return {string}
         */
        text: function()
        {
            return this._codeMirror.getValue().replace(/\n/g, this._lineSeparator);
        },

        /**
         * @return {TextRange}
         */
        range: function()
        {
            var lineCount = this.linesCount;
            var lastLine = this._codeMirror.getLine(lineCount - 1);
            return this._toRange(new CodeMirror.Pos(0, 0), new CodeMirror.Pos(lineCount - 1, lastLine.length));
        },

        /**
         * @param {number} lineNumber
         * @return {string}
         */
        line: function(lineNumber)
        {
            return this._codeMirror.getLine(lineNumber);
        },

        /**
         * @return {number}
         */
        get linesCount()
        {
            return this._codeMirror.lineCount();
        },

        /**
         * @param {number} line
         * @param {string} name
         * @param {Object?} value
         */
        setAttribute: function(line, name, value)
        {
            if (line < 0 || line >= this._codeMirror.lineCount())
                return;
            var handle = this._codeMirror.getLineHandle(line);
            if (handle.attributes === undefined) handle.attributes = {};
            handle.attributes[name] = value;
        },

        /**
         * @param {number} line
         * @param {string} name
         * @return {?Object} value
         */
        getAttribute: function(line, name)
        {
            if (line < 0 || line >= this._codeMirror.lineCount())
                return null;
            var handle = this._codeMirror.getLineHandle(line);
            return handle.attributes && handle.attributes[name] !== undefined ? handle.attributes[name] : null;
        },

        /**
         * @param {number} line
         * @param {string} name
         */
        removeAttribute: function(line, name)
        {
            if (line < 0 || line >= this._codeMirror.lineCount())
                return;
            var handle = this._codeMirror.getLineHandle(line);
            if (handle && handle.attributes)
                delete handle.attributes[name];
        },

        /**
         * @param {TextRange} range
         * @return {{start: CodeMirror.Pos, end: CodeMirror.Pos}}
         */
        _toPos: function(range)
        {
            return {
                start: new CodeMirror.Pos(range.startLine, range.startColumn),
                end: new CodeMirror.Pos(range.endLine, range.endColumn)
            }
        },

        _toRange: function(start, end)
        {
            return new TextRange(start.line, start.ch, end.line, end.ch);
        },

        __proto__: View.prototype
    }

    /*
    CodeMirrorTextEditor.prototype.scrollLineToClientView = function(linenum){
        var editor = this._editor;
        var scrollInfo = editor.getScrollInfo();

        var targetLineTop = editor.heightAtLine(linenum, this._coordinateMode);

        var clientviewTop = scrollInfo.top,
            clientviewBottom = scrollInfo.clientHeight + scrollInfo.top;

        if (targetLineTop > clientviewTop && targetLineTop < clientviewBottom - editor.defaultTextHeight()){
            //不需要做任何操作
        } else {
             if (scrollInfo.height - targetLineTop < scrollInfo.clientHeight){
                 //在最后一屏
                 editor.scrollTo(0, scrollInfo.height - scrollInfo.clientHeight);
             } else {
                 //把目标行滚动到最上面，留4行方便看到上下文
                 editor.scrollTo(0, editor.heightAtLine(linenum - this._scrollMaginOfExeclineNums, this._coordinateMode));
                 //editor.scrollTo(0, targetLineTop);
             }
        }
    };
    */

    return CodeMirrorTextEditor;
});
