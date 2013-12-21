define(function (require, exports) {
    var EventObjectEmitter = require("events").EventObjectEmitter;
    var inherits = require("util").inherits;

    var UIUtils = require("UIUtils");

    function View(){
        this.element = document.createElement("div");
        this.element.__view = this;
        this._visible = true;
        this._isRoot = false;
        this._isShowing = false;
        this._children = [];
        this._hideOnDetach = false;
        this._notificationDepth = 0;
    }

    inherits(View, EventObjectEmitter);

    /**
     * @return {?Element}
     */
    View.prototype.statusBarText = function()
    {
        return null;
    }

    View.prototype.markAsRoot = function()
    {
        View._assert(!this.element.parentElement, "Attempt to mark as root attached node");
        this._isRoot = true;
    }

    /**
     * @return {?View}
     */
    View.prototype.parentView = function()
    {
        return this._parentView;
    }

    View.prototype.isShowing = function()
    {
        return this._isShowing;
    }

    View.prototype.setHideOnDetach = function()
    {
        this._hideOnDetach = true;
    }

    /**
     * @return {boolean} 
     */
    View.prototype._inNotification = function()
    {
        return !!this._notificationDepth || (this._parentView && this._parentView._inNotification());
    }

    View.prototype._parentIsShowing = function()
    {
        if (this._isRoot)
            return true;
        return this._parentView && this._parentView.isShowing();
    }

    /**
     * @param {function(this:View)} method
     */
    View.prototype._callOnVisibleChildren = function(method)
    {
        var copy = this._children.slice();
        for (var i = 0; i < copy.length; ++i) {
            if (copy[i]._parentView === this && copy[i]._visible)
                method.call(copy[i]);
        }
    }

    View.prototype._processWillShow = function()
    {
        this._callOnVisibleChildren(this._processWillShow);
        this._isShowing = true;
    }

    View.prototype._processWasShown = function()
    {
        if (this._inNotification())
            return;
        this.restoreScrollPositions();
        this._notify(this.wasShown);
        this._notify(this.onResize);
        this._callOnVisibleChildren(this._processWasShown);
    }

    View.prototype._processWillHide = function()
    {
        if (this._inNotification())
            return;
        this.storeScrollPositions();

        this._callOnVisibleChildren(this._processWillHide);
        this._notify(this.willHide);
        this._isShowing = false;
    }

    View.prototype._processWasHidden = function()
    {
        this._callOnVisibleChildren(this._processWasHidden);
    }

    View.prototype._processOnResize = function()
    {
        if (this._inNotification())
            return;
        if (!this.isShowing())
            return;
        this._notify(this.onResize);
        this._callOnVisibleChildren(this._processOnResize);
    }

    /**
     * @param {function(this:View)} notification
     */
    View.prototype._notify = function(notification)
    {
        ++this._notificationDepth;
        try {
            notification.call(this);
        } finally {
            --this._notificationDepth;
        }
    }

    View.prototype.wasShown = function()
    {
    }

    View.prototype.willHide = function()
    {
    }

    View.prototype.onResize = function()
    {
    }

    /**
     * @param {Element} parentElement
     * @param {Element=} insertBefore
     */
    View.prototype.show = function(parentElement, insertBefore)
    {
        View._assert(parentElement, "Attempt to attach view with no parent element");

        // Update view hierarchy
        if (this.element.parentElement !== parentElement) {
            if (this.element.parentElement)
                this.detach();

            var currentParent = parentElement;
            while (currentParent && !currentParent.__view)
                currentParent = currentParent.parentElement;

            if (currentParent) {
                this._parentView = currentParent.__view;
                this._parentView._children.push(this);
                this._isRoot = false;
            } else
                View._assert(this._isRoot, "Attempt to attach view to orphan node");
        } else if (this._visible)
            return;

        this._visible = true;

        if (this._parentIsShowing())
            this._processWillShow();

        this.element.addStyleClass("visible");

        // Reparent
        if (this.element.parentElement !== parentElement) {
            View._incrementViewCounter(parentElement, this.element);
            if (insertBefore)
                View._originalInsertBefore.call(parentElement, this.element, insertBefore);
            else
                View._originalAppendChild.call(parentElement, this.element);
        }

        if (this._parentIsShowing())
            this._processWasShown();
    }

    View._originalAppendChild = Element.prototype.appendChild;
    View._originalInsertBefore = Element.prototype.insertBefore;
    View._originalRemoveChild = Element.prototype.removeChild;
    View._originalRemoveChildren = Element.prototype.removeChildren;


    View._incrementViewCounter = function(parentElement, childElement)
    {
        var count = (childElement.__viewCounter || 0) + (childElement.__view ? 1 : 0);
        if (!count)
            return;

        while (parentElement) {
            parentElement.__viewCounter = (parentElement.__viewCounter || 0) + count;
            parentElement = parentElement.parentElement;
        }
    }


    /**
     * @param {boolean=} overrideHideOnDetach
     */
    View.prototype.detach = function(overrideHideOnDetach)
    {
        var parentElement = this.element.parentElement;
        if (!parentElement)
            return;

        if (this._parentIsShowing())
            this._processWillHide();

        if (this._hideOnDetach && !overrideHideOnDetach) {
            this.element.removeStyleClass("visible");
            this._visible = false;
            if (this._parentIsShowing())
                this._processWasHidden();
            return;
        }

        // Force legal removal
        View._decrementViewCounter(parentElement, this.element);
        View._originalRemoveChild.call(parentElement, this.element);

        this._visible = false;
        if (this._parentIsShowing())
            this._processWasHidden();

        // Update view hierarchy
        if (this._parentView) {
            var childIndex = this._parentView._children.indexOf(this);
            View._assert(childIndex >= 0, "Attempt to remove non-child view");
            this._parentView._children.splice(childIndex, 1);
            this._parentView = null;
        } else
            View._assert(this._isRoot, "Removing non-root view from DOM");
    }

    View.prototype.detachChildViews = function()
    {
        var children = this._children.slice();
        for (var i = 0; i < children.length; ++i)
            children[i].detach();
    }

    View.prototype.elementsToRestoreScrollPositionsFor = function()
    {
        return [this.element];
    }

    View.prototype.storeScrollPositions = function()
    {
        var elements = this.elementsToRestoreScrollPositionsFor();
        for (var i = 0; i < elements.length; ++i) {
            var container = elements[i];
            container._scrollTop = container.scrollTop;
            container._scrollLeft = container.scrollLeft;
        }
    }

    View.prototype.restoreScrollPositions = function()
    {
        var elements = this.elementsToRestoreScrollPositionsFor();
        for (var i = 0; i < elements.length; ++i) {
            var container = elements[i];
            if (container._scrollTop)
                container.scrollTop = container._scrollTop;
            if (container._scrollLeft)
                container.scrollLeft = container._scrollLeft;
        }
    }

    View.prototype.canHighlightPosition = function()
    {
        return false;
    }

    /**
     * @param {number} line
     * @param {number=} column
     */
    View.prototype.highlightPosition = function(line, column)
    {
    }

    View.prototype.doResize = function()
    {
        this._processOnResize();
    }

    View._decrementViewCounter = function(parentElement, childElement)
    {
        var count = (childElement.__viewCounter || 0) + (childElement.__view ? 1 : 0);
        if (!count)
            return;

        while (parentElement) {
            parentElement.__viewCounter -= count;
            parentElement = parentElement.parentElement;
        }
    }

    View._assert = function(condition, message)
    {
        if (!condition) {
            console.trace();
            throw new Error(message);
        }
    }


    View.prototype.appendChild = function(child)
    {
        View._assert(!child.__view, "Attempt to add view via regular DOM operation.");
        return View._originalAppendChild.call(this, child);
    }

    View.prototype.insertBefore = function(child, anchor)
    {
        View._assert(!child.__view, "Attempt to add view via regular DOM operation.");
        return View._originalInsertBefore.call(this, child, anchor);
    }


    View.prototype.removeChild = function(child)
    {
        View._assert(!child.__viewCounter && !child.__view, "Attempt to remove element containing view via regular DOM operation");
        return View._originalRemoveChild.call(this, child);
    }

    View.prototype.removeChildren = function()
    {
        View._assert(!this.__viewCounter, "Attempt to remove element containing view via regular DOM operation");
        View._originalRemoveChildren.call(this);
    }


    /**
     * @return {Element}
     */
    View.prototype.defaultFocusedElement = function()
    {
        return this._defaultFocusedElement || this.element;
    }

    /**
     * @param {Element} element
     */
    View.prototype.setDefaultFocusedElement = function(element)
    {
        this._defaultFocusedElement = element;
    }

    View.prototype.focus = function()
    {
        var element = this.defaultFocusedElement();
        if (!element || element.isAncestor(document.activeElement))
            return;

        UIUtils.setCurrentFocusElement(element);
    }

    return View;
});
