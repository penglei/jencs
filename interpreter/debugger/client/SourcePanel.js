define(function(require, exports){

    $("#files-nav").html(require("sourceview.html"));

    var CMEditor = null;
    var SourcesData = {};

    var sourcesMenuElement = $("#sources-list");
    var currentFileid = -1;

    sourcesMenuElement.delegate(".source-item", "click", function(){
        var $this = $(this);
        ActiveSource($this.data("id"));
    });

    function ActiveSource(id){
        var obj = SourcesData[id];
        var $elem = sourcesMenuElement.find('li[data-id=' + id + ']');
        sourcesMenuElement.find(".active").toggleClass("active");
        $elem.toggleClass("active");
        if (id != currentFileid){
            CMEditor.setValue(obj.source);
            currentFileid = id;
        }
    }


    var executionLineHandle = null;

    exports.initSourcesView = function(editor, data){
        var sources = data.sources;
        for (var i = 0, l = sources.length; i < l; i ++) {
            var v = sources[i];
                //'<li class="source-item" data-id="{id}"><a href="#">{name}</a></li>"
            sourcesMenuElement.append('<li class="source-item" data-id="' + v.id + '"><a href="#">' + v.name + '</a></li>');
            SourcesData[v.id] = v;
        }
        CMEditor = editor;
        if (data.DebugBreak){
            this.ActiveSourceLine(data.DebugBreak.id, data.DebugBreak.first_line);
        }
    };

    var coordinateMode = "local";
    function ScrollLineToClientView(linenum){
        var scrollInfo = CMEditor.getScrollInfo();

        var targetLineTop = CMEditor.heightAtLine(linenum, coordinateMode);

        var clientviewTop = scrollInfo.top,
            clientviewBottom = scrollInfo.clientHeight + scrollInfo.top;

        if (targetLineTop > clientviewTop && targetLineTop < clientviewBottom - CMEditor.defaultTextHeight()){
            //不需要做任何操作
        } else {
             if (scrollInfo.height - targetLineTop < scrollInfo.clientHeight){
                 //在最后一屏
                 CMEditor.scrollTo(0, scrollInfo.height - scrollInfo.clientHeight);
             } else {
                 //把目标行滚动到最上面，留4行方便看到上下文
                 CMEditor.scrollTo(0, CMEditor.heightAtLine(linenum - 4, coordinateMode));
                 //CMEditor.scrollTo(0, targetLineTop);
             }
        }
    }

    exports.ActiveSourceLine = function(id, linenum){
        linenum = linenum - 1;
        ActiveSource(id);

        ScrollLineToClientView(linenum);

        if (executionLineHandle) CMEditor.removeLineClass(executionLineHandle, "background", "cm-execution-line");

        executionLineHandle = CMEditor.getLineHandle(linenum);
        CMEditor.addLineClass(executionLineHandle, "background", "cm-execution-line");
    };

    exports.UnActiveSourceLine = function () {
        if (executionLineHandle) CMEditor.removeLineClass(executionLineHandle, "background", "cm-execution-line");
    };
});

