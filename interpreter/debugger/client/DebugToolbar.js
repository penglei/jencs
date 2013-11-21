define(function (require, exports){
    var $ = require("jquery");

    function DebugToolbar(){

        this._disabled = false;
        this._resumeHandler = this.resume.bind(this);
        this._stepoverHandler = this.stepOver.bind(this);
        this._stepintoHandler = this.stepInto.bind(this);

        $("#resume").click(this._resumeHandler);
        $("#step-over").click(this._stepoverHandler);
        $("#step-into").click(this._stepintoHandler);
    }

    DebugToolbar.prototype.resume = function(){
        if (!this._disabled) DebugAgent.resume();
    };

    DebugToolbar.prototype.stepOver = function(){
        if (!this._disabled) DebugAgent.stepOver();
    };

    DebugToolbar.prototype.stepInto = function(){
        if (!this._disabled) DebugAgent.stepInto();
    };

    DebugToolbar.prototype.disable = function(){
        this._disabled = true;
    };

    return DebugToolbar;
});
