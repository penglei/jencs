//css
(function(){
    var head = document.getElementsByTagName("head");
    if (!head || !head.length) return;
    head = head[0];

    function isCss(name){
        var pos;
        if ((pos = (name + "").lastIndexOf(".")) > -1){
            var ext = name.substring(pos + 1);
            return ext == "css";
        }
    }

    var R_ready = /loaded|complete/;
    function importCss(url, sucCb, errCb){
        var link = document.createElement("link");
        link.setAttribute("rel", "stylesheet");
        link.setAttribute("type", "text/css");
        link.setAttribute("href", url);
        head.appendChild(link);
        link.onload = sucCb;
        errCb && (link.onerror = errCb);
        link.onreadystatechange = function(){
            if (R_ready.test(this.readyState)){
                tool.isFunction(sucCb) && sucCb();
            }
        };
        return link;
    }
    var tool;
    require.plugin(function(sys, util){
        tool = util;
        sys.on("meta", function(meta){
            if (isCss(meta.name)){
                meta.css = true;
                meta.noConcat = true;
            }
        });
        sys.on("new", function(moduleLoader){
            moduleLoader.on("request", function(){
                var flag = false;
                if (moduleLoader.meta.css){
                    var link = importCss(moduleLoader.meta.url, function(){
                        if (flag) return;
                        flag = true;
                        link.onreadystatechange = link.onload = null;
                        (function(elem){
                            moduleLoader.factory = function(){
                                return {
                                    "elem":elem
                                }
                            }
                        })(link);
                        moduleLoader.exec();
                        link = null;
                    });
                    return true;
                }
            })//module on "request"
        })//on "new"
    });
})();
