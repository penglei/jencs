define(function(require, exports, module) {
    var inherits = require("util").inherits;

    function EventObjectEmitter() {
        this._listeners = {};
    }

    EventObjectEmitter.prototype = {
        /**
         * @param {string} eventType
         * @param {function(EventObject)} listener
         * @param {Object=} thisObject
         */
        addEventListener: function(eventType, listener, thisObject)
        {
            if (!listener)
                console.assert(false);

            if (!this._listeners)
            //if sub class did't call EventObjectEmitter constructor.
                this._listeners = {};

            if (!this._listeners[eventType])
                this._listeners[eventType] = [];
            this._listeners[eventType].push({ thisObject: thisObject, listener: listener });
            return this;
        },

        /**
         * @param {string} eventType
         * @param {function(EventObject)} listener
         * @param {Object=} thisObject
         */
        removeEventListener: function(eventType, listener, thisObject)
        {
            console.assert(listener);

            if (!this._listeners || !this._listeners[eventType])
                return;
            var listeners = this._listeners[eventType];
            for (var i = 0; i < listeners.length; ++i) {
                if (listener && listeners[i].listener === listener && listeners[i].thisObject === thisObject)
                    listeners.splice(i, 1);
                else if (!listener && thisObject && listeners[i].thisObject === thisObject)
                    listeners.splice(i, 1);
            }

            if (!listeners.length)
                delete this._listeners[eventType];
            return this;
        },

        removeAllListeners: function()
        {
            this._listeners = {};
        },

        /**
         * @param {string} eventType
         * @return {boolean}
         */
        hasEventListeners: function(eventType)
        {
            if (!this._listeners || !this._listeners[eventType])
                return false;
            return true;
        },

        /**
         * @param {string} eventType
         * @param {*=} eventData
         * @return {boolean}
         */
        dispatchEventToListeners: function(eventType, eventData)
        {
            if (!this._listeners || !this._listeners[eventType])
                return false;

            var event = new EventObject(this, eventType, eventData);
            var listeners = this._listeners[eventType].slice(0);
            for (var i = 0; i < listeners.length; ++i) {
                listeners[i].listener.call(listeners[i].thisObject, event);
                if (event._stoppedPropagation)
                    break;
            }

            return event.defaultPrevented;
        }
    };

    /**
     * @constructor
     * @param {EventObjectEmitter} target
     * @param {string} type
     * @param {*=} data
     */
    function EventObject (target, type, data){
        this.target = target;
        this.type = type;
        this.data = data;
        this.defaultPrevented = false;
        this._stoppedPropagation = false;
    }

    EventObject.prototype = {
        stopPropagation: function()
        {
            this._stoppedPropagation = true;
        },

        preventDefault: function()
        {
            this.defaultPrevented = true;
        },

        /**
         * @param {boolean=} preventDefault
         */
        consume: function(preventDefault)
        {
            this.stopPropagation();
            if (preventDefault)
                this.preventDefault();
        }
    }


    function EventEmitter() {
        EventObjectEmitter.call(this);
    }

    inherits(EventEmitter, EventObjectEmitter);

    EventEmitter.prototype.emit = function(type){

        var listeners, handler, thisObject, len, args, i;

        listeners = this._listeners[type];

        if (!listeners || !listeners.length) return false;

        len = arguments.length;
        if (len > 3) {
            args = new Array(len - 1);
            for (i = 1; i < len; i++)
                args[i - 1] = arguments[i];
        }
        for (i = 0; i < listeners.length; i++) {
            handler = listeners[i].listener;
            switch (len) {
                // fast cases
                case 1:
                    handler.call(this);
                    break;
                case 2:
                    handler.call(this, arguments[1]);
                    break;
                case 3:
                    handler.call(this, arguments[1], arguments[2]);
                    break;
                    // slower
                default:
                    handler.apply(this, args);
            }
        }

        return true;
    }

    EventEmitter.prototype.on = function(type, listener){
        this.addEventListener(type, listener, this);
        return this;
    }

    EventEmitter.prototype.off = function(type, listener) {
        this.removeEventListener(type, listener, this);
        return this;
    };


    exports.EventObjectEmitter = EventObjectEmitter;
    exports.EventEmitter = EventEmitter;
});
