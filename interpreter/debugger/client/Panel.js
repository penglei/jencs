define(function(require, exports){

    exports.codeEditor = null;
    exports.toolbar = null;

    exports.setReSources = function(sources){
        this.codeEditor.setSources(sources);

    };

    exports.debuggerPaused = function(executeLine, scopeChain, watchExpressions){
        this.codeEditor.pausedDetail(executeLine);
    };

});
