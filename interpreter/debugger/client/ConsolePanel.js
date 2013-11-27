define(function(require, exports){
    var Panel = require("Panel").Panel;

    function ConsolePanel(){
        Panel.call(this);
    }

    ConsolePanel.prototype = {

        __proto__: Panel.prototype
    };

    return ConsolePanel;
});
