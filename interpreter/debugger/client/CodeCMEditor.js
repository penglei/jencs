define(function(require, exports) {
    var Util = require("util");
    var EventEmitter = require("events").EventEmitter;
    var $ = require('jquery');

    function CodeCMEditor(){
        EventEmitter.call(this);
        this._sourceNavView = new SourceNavView(this);

        var editor = new CodeMirror($("#code").get(0), {
            //value: code || "",
            cursorBlinkRate: 0, //0 is disabled
            readOnly: true,
            tabMode: "indent",
            theme: "monokai",
            lineNumbers: true,
            indentUnit: 4,
            mode: "text/clearsilver"
        });
        editor.setSize("auto", "500");

        this._sources = {};
        this._currentSourceid = null;
        this._editor = editor;
        this._execLineHandle = null;
        this._coordinateMode = "local";
        this._scrollMaginOfExeclineNums = 4;
    }

    Util.inherits(CodeCMEditor, EventEmitter);

    CodeCMEditor.prototype.setSources = function(sources){
        /*
        sobj {
            "name":name,
            "source":source,
            "id": id
        }
        */
        for(var i = 0; i < sources.length; i++){
            var sobj = sources[i];
            this._sources[sobj.id] = sobj;
        }
        this._sourceNavView.init(sources);
    };

    CodeCMEditor.prototype.pausedDetail = function(lineInfo){
        /*
        "evaluateLine": {
            "stype": astnode.type,
            "line": astnode.pos.first_line,
            "colnum": astnode.pos.first_column,
            "sourceName": astnode.pos.name,
            "sourceId": astnode.pos.fileid,
        },
        */
        this._sourceNavView.activeSource(lineInfo.sourceId);
        this.unActiveExecutionLine();
        this.activeExecutionLine(lineInfo.line);
    };

    CodeCMEditor.prototype.scrollLineToClientView = function(linenum){
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

    CodeCMEditor.prototype.activeExecutionLine = function(linenum){
        var editor = this._editor;
        linenum = linenum - 1;

        this.scrollLineToClientView(linenum);

        this._execLineHandle = editor.getLineHandle(linenum);
        this._editor.addLineClass(this._execLineHandle, "background", "cm-execution-line");
    };

    CodeCMEditor.prototype.unActiveExecutionLine = function(){
        if (this._execLineHandle) this._editor.removeLineClass(this._execLineHandle, "background", "cm-execution-line");
    };

    CodeCMEditor.prototype.switchSourceFrame = function(id){
        if (!this._sources[id]){
            console.error("source not found");
            return;
        }
        if (this._currentSourceid != id) {
            this._editor.setValue(this._sources[id].source);
            this._currentSourceid = id;
        }
    };

    var SourceNavItemTpl = '<li class="source-item" data-id="{id}"><a href="#">{name}</a></li>';
    function SourceNavView(sourcePanel){
        $("#files-nav").html(require("sourceview.html"));
        this._itemClass = ".source-item";
        this._elem = $("#sources-list");
        this._sourcePanel = sourcePanel;
        this._elem.delegate(this._itemClass, "click", this._sourceFrameClickHandler.bind(this));
    }

    SourceNavView.prototype.init = function(datas){
        for(var i = 0; i < datas.length; i++){
            this._elem.append(Util.format(SourceNavItemTpl, datas[i]));
        }
    };

    SourceNavView.prototype._sourceFrameClickHandler = function(evt){
        var $target = $(evt.target).parents(this._itemClass).eq(0) || $(evt.target);
        var fileid = $target.data("id");
        this.activeSource(fileid);
    };

    SourceNavView.prototype.activeSource = function(id){
        this._elem.find(".active").toggleClass("active");
        var $elem = this._elem.find('li[data-id=' + id + ']').addClass("active");
        this._sourcePanel.switchSourceFrame(id);
    };

    return CodeCMEditor;
});
