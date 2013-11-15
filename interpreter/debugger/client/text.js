//template(text)
(function(){
    var progIds = ['Msxml2.XMLHTTP', 'Microsoft.XMLHTTP', 'Msxml2.XMLHTTP.4.0'];
    function createXHR(){
        //Would love to dump the ActiveX crap in here. Need IE 6 to die first.
        var xhr, i, progId;
        if (typeof XMLHttpRequest !== "undefined") {
            return new XMLHttpRequest();
        } else if (typeof ActiveXObject !== "undefined") {
            for (i = 0; i < 3; i += 1) {
                progId = progIds[i];
                try {
                    xhr = new ActiveXObject(progId);
                } catch (e) {}

                if (xhr) {
                    progIds = [progId];  // so faster next time
                    break;
                }
            }
        }
        return xhr;
    }

    function get(url, callback, errback, headers) {
        var xhr = createXHR(), header;
        headers = headers || {};
        //标准XHR http头
        headers["X-Requested-With"] = "XMLHttpRequest";
        xhr.open('GET', url, true);

        //Allow plugins direct access to xhr headers
        for (header in headers) {
            if (headers.hasOwnProperty(header)) {
                xhr.setRequestHeader(header.toLowerCase(), headers[header]);
            }
        }

        xhr.onreadystatechange = function (evt) {
            var status, err;
            //Do not explicitly handle errors, those should be
            //visible via console output in the browser.
            if (xhr.readyState === 4) {
                status = xhr.status;
                if (status > 399 && status < 600) {
                    //An http 4xx or 5xx error. Signal an error.
                    err = new Error(url + ' HTTP status: ' + status);
                    err.xhr = xhr;
                    errback(err);
                } else {
                    callback(xhr.responseText);
                }
            }
        };
        xhr.send();
    }
    require.plugin(function (sys, util){
        var textExts = ['txt', 'html', 'htm', 'tpl'];
        var textReg = new RegExp(textExts.join("|"));
        sys.on("config", function(opts){
            //XXX 可以增加模板的扩展名
            var extendTextType = opts.textType, exist;
            if (util.isString(opts.textType)){
                extendTextType = [ extendTextType ];
            }

            if (util.isArray(extendTextType)){
                for(var j = 0; extendTextType[j]; j++){
                    exist = false;
                    for (var i = 0; i < textExts.length; i++){
                        if (textExts[i] == extendTextType[j]) {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist) textExts.push(extendTextType[j]);
                }
            }
            textReg = new RegExp(textExts.join("|"));
        });
        sys.on("meta", function(meta){
            if (textReg.test(meta.name)){
                meta.isText = true;
                meta.noConcat = true;
            }
        })//on meta
        sys.on("new", function(module){
            if (!module.meta.isText) return;
            module.on("request", function(parentModule){
                //TODO 跨域问题
                get(module.meta.url, function(responseText){
                    var def = { //name:xx, //deps:xx
                        factory: responseText//直接用返回的内容
                    };
                    //必须提交fetched事件，给其他插件使用
                    module.emit("fetched",def) || module.update(def);
                }, function(err){
                    module.emit("fetched", null) || module.update(null);
                });
                return true;
            });
        })//on new
    })//plugin
})();



