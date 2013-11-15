(function(global) {
    function require(deps, cb) {
        globalRequire(deps, cb, cfgBase);
    }

    require.config = function(opt) { //see https://github.com/amdjs/amdjs-api/wiki/Common-Config
        if (!isObject(opt)) return;
        //this should before another
        var baseUrl = opt.baseUrl;
        if (baseUrl) createBase(baseUrl + (R_suffix.test(baseUrl) ? "" : "/"));
        opt.charset && (cfg.charset = opt.charset);
        emit("config", [opt]);
    }

    var cfg = {}, cfgBase, //全局base地址
        moduleSets = {}, //已经加载完成的模块
        events = {},
        util = {
            each: each,
            warn: warn,
            parseURI: parseURI
        },
        sys = {
            modules: moduleSets,
            parse: resolve,
            on: function(name, cb) {
                on(name, cb);
            },
            emit: function(name) {
                return emit(name, Array.prototype.slice.call(arguments, 1));
            },
            config: cfg
        };

    //[match, protocol, host, port, path, target, search, hash]
    var R_uri = /^(?:(\w+):)?(?:\/\/([^\/\?\#:]*(?=:\d+|\/|$))?(?:\:(\d+))?)?([^#\?]*?)([^\/\?#]*)?(\?[^#]*)?(#.*)?$/,
        R_jsEnd = /\.js$/,
        R_suffix = /(?:\.\w*[a-zA-Z_]\w*|#)$/,
        R_pathSplit = /\/+/;

    function on(name, cb, evts) {
        evts = evts || events;
        (evts[name] = evts[name] || []).push(cb);
    }

    function emit(name, args, _this, evts) {
        evts = evts || events;
        return each(evts[name], function(cb) {
            return cb.apply(_this, args);
        });
    }

    function each(ary, func, that) {
        if (ary) {
            for (var i = 0; i < ary.length; i += 1) {
                if (ary[i] && func.call(that, ary[i], i, ary)) return true;
            }
        }
    }

    function type(name) {
        return util["is" + name] = function(obj) {
            return Object.prototype.toString.call(obj) == "[object " + name + "]";
        }
    }
    var isFunction = type("Function"),
        isObject = type("Object"),
        isString = type("String"),
        isArray = type("Array");

    function parseURI(str) {
        var result = R_uri.exec(str);
        return {
            protocol: result[1],
            host: result[2] || "",
            port: result[3],
            path: (result[4] ? result[4] : ""),
            pathname: (result[4] ? (result[4] + (result[5] ? result[5] : "")) : ""),
            target: result[5] || "",
            search: result[6],
            hash: result[7]
        };
    }

    function warn(msg) {
        if (typeof console != "undefined") {
            console.warn(msg);
        }
    }
    /////////////////////////

    function insertArray(arr, object) {
        for (var i = 0; arr[i]; i++) if (arr[i] === object) return;
        return arr.push(object);
    }

    function normalizePath(path) {
        // if the path tries to go above the root, `up` ends up > 0
        var parts = path.split(R_pathSplit),
            i;
        for (i = parts.length - 1; i >= 0; i--) {
            if (!parts[i]) parts.splice(i, 1);
        }

        var up = 0,
            last;
        for (i = parts.length - 1; i >= 0; i--) {
            last = parts[i];
            if (last == '.') {
                parts.splice(i, 1);
            } else if (last === '..') {
                parts.splice(i, 1);
                up++;
            } else if (up) {
                parts.splice(i, 1);
                up--;
            }
        } //XXX 是否在这里判断相对路径不合法呢?
        if (parts.length) parts.unshift("");
        return parts.join("/") + "/";
    }

    function createBase(url) {
        cfg.base = cfgBase = resolve(url, cfgBase).base;
    }

    function makeBaseHost(uri) {
        return (uri.protocol || "http") + "://" + uri.host +
            ((uri.port == "80" || !uri.port) ? "" : ":" + uri.port);
    }

    function noticeCircular(ringModule) {
        var i, str = "circular found:\n@: ",
            r = [],
            ringModule = ringModule.reverse(),
            module;
        for (i = 0; module = ringModule[i]; i++) {
            r.push(module.id + "\n");
        }
        r.push(ringModule[0].id);
        warn(str + r.join("-->"));
    }

    ///////////name parse/////////////////

    function nameMeta(name, base) {
        //XXX 其实分析名字是根父模块相关的，是不是应该放在Module.on里面?
        var meta = resolve(name, base);
        meta.name = name;
        //用两个事件，提供对名字处理的两个优先级
        emit("name", [name, base, meta]);
        emit("meta", [meta, base]);
        return meta;
    }
    /**
     * 把一个模块名分析后的meta对象，包含id，逻辑上的url
     * @param {String} name 模块名
     * @param {String} base
     * @returns {Object} 包含模块基本信息的meta对象
     */
    function resolve(name, base) {
        var twoChar = name.substr(0, 2),
            threeChar = name.substr(0, 3),
            j, hash, search = "";

        if ((j = name.lastIndexOf("#")) > -1) {
            hash = name.substr(j);
            name = name.substr(0, j);
        }

        if ((j = name.lastIndexOf("?")) > -1) {
            search = name.substr(j);
            name = name.substr(0, j);
        }

        var i = name.lastIndexOf("/"),
            basename = i > -1 ? name.substr(i + 1) : name,
            dirname = name.substr(0, i + 1),
            dir, id;

        if (twoChar == "./" || threeChar == "../") {
            var baseuri = parseURI(base);
            dir = makeBaseHost(baseuri) + normalizePath(baseuri.path + dirname);
            id = dir + basename;
        } else if (twoChar == "//" || name.indexOf("://") > -1) {
            var r = parseURI(name);
            dir = dirname;
            id = name;
        } else {
            dir = cfgBase + dirname;
            id = dir + basename;
        }

        R_suffix.test(basename) || (id += ".js");
        url = id + search;
        //TODO if (hash && !R_jsEnd.test(target)) id += "#";//只有"/test/foo#"才表示id不一样,foo.js#与foo.js是相同的

        return {
            id: id,
            base: dir,
            url: url
        }
    };

    var globalRequire;
    (function() {
        var R_specified = /^(?:require|exports|module)$/,
            moduleRunners = sys.moduleRequires = {}, //正在加载或者执行的模块容器
            globalDefs = sys.defines = {}; //已经加载完成的模块定义，但没有执行

        globalRequire = function(deps, cb, base) {
            if (isString(deps)) deps = [deps];
            if (isArray(deps)) {
                createModuleLoader({
                    "base": base
                }, deps, cb).require();
            }
        }
        sys.segment = segmentDef;

        /**
         *模块加载对象。 *Module是任何模块加载的载体，也可以作为一个中间临时加载的载体来传递加载的模块
         */
        function Module() {
            var t = this; //t.m = ModuleWrapper;//真实的模块包装器，最后缓存的模块都是ModuleWrapper
            t.mods = {}; //依赖的模块列表
            t.parents = []; //包含父模块的数组
            t.wait = 0; //等待完成的依赖数
        }

        Module.prototype = {
            /**
             * 启动一个module运行，请求还未加载的模块
             */
            require: function() {
                var t = this,
                    depNames = t.deps || [],
                    expects = [], //需要加载的
                    boots = []; //需要马上启动的
                t.deps = []; //原来的dpes只是名字数组，现在要更新为Module.meta数组
                for (var name, i = 0; name = depNames[i]; i++) {
                    //root的依赖是没有specified区分的(root也是没有id的)
                    if (R_specified.test(name) && t.id) t.deps.push({
                            "specified": name
                        });
                    else { //主要的依赖逻辑
                        var meta = nameMeta(name, t.meta.base),
                            def, depModule, _id = meta.id;
                        t.deps.push(meta);
                        if (moduleSets[_id]) { //模块已经存在
                            t.mods[_id] = moduleSets[_id];
                        } else {
                            if (depModule = moduleRunners[_id]) { //is loading //TODO Module未被删除但是已经执行完
                                if (!isCircular(depModule, t.parents, [t])) {
                                    insertArray(depModule.parents, t) && t.wait++; //添加依赖关系(防重)
                                } else {
                                    t.mods[_id] = depModule.mod;
                                }
                            } else { //建立需要启动或加载的模块
                                if (def = globalDefs[_id]) {
                                    boots.push(depModule = createModuleLoader(meta, def.deps, def.factory));
                                    delete globalDefs[_id]; //清掉被使用了的def
                                } else {
                                    expects.push(depModule = createModuleLoader(meta));
                                }
                                insertArray(depModule.parents, t); //添加依赖关系
                                t.wait++;
                            }
                        }
                    }
                } //for deps
                //先判断这里，再做下面的逻辑，一个经典的问题:我们都是以异步的眼光看待模块的
                if (!t.wait && !t.emit("required")) t.exec();
                expects.length && t.emit("prepare", expects) || each(expects, function(module) { //拉取还未加载的模块
                    module.request(t);
                });
                each(boots, function(module) { //运行已经加载完成的模块
                    module.require();
                });
            },
            /**
             *加载模块自身的文件
             */
            request: function(_parent) {
                var t = this;
                if (!t.emit("request", _parent)) {
                    fetch(t.meta.url, function(defs) {
                        //最终，segmentDef会调用update方法完成依赖的加载
                        //有些插件可能需要改变加载顺序：加载依赖->再加载自身模块
                        //XXX fetched事件改成reuested事件更好
                        if (!t.emit("fetched", defs)) {
                            var children = segmentDef([t], defs);
                            each(children, function(item) {
                                item[0].update(item[1]); //item[1]为undefined也没问题
                            });
                        }
                    });
                }
            },
            /**
             *根据获取的依赖def信息，开始加载依赖
             */
            update: function(def) {
                var t = this;
                if (!def) {
                    if (t.mod) t.mod.exports = null;
                    delete moduleRunners[t.id];
                    completeModuleLoader(t);
                } else { //把dpes和cb都设置好
                    t.deps = def.deps || [];
                    t.factory = def.factory;
                    t.require();
                }
            },
            /**
             * 模块及其依赖加载完成，开始执行factory
             */
            exec: function() {
                var t = this,
                    depsParam, exports = null,
                    timer, factory = t.factory;
                if (isFunction(factory)) {
                    depsParam = makeFactoryParam(t);
                    //如果factory执行错误，程序会停留在这个地方，
                    delete moduleRunners[t.id];
                    //TODO 如果模块初始化错误，是否还要继续运行呢?
                    //var timer = setTimeout(function(){
                    //      completeModuleLoader(t);
                    //});
                    exports = factory.apply(null, depsParam);
                    //clearTimeout(timer);
                } else if (isObject(factory) || isString(factory)) { //如果强制转换为true,就把它设为模块
                    exports = factory;
                }
                //XXX !== undefind 是否太过宽泛？
                if (t.mod && exports !== undefined) t.mod.exports = exports; //return exports 覆盖 内置exports
                completeModuleLoader(t);
            },
            on: function(name, cb) {
                on(name, cb, (this.events = this.events || []));
            },
            emit: function(name) {
                return emit(name, Array.prototype.slice.call(arguments, 1), this, this.events);
            }
        }

        //把所有def分割到module上面去

        function segmentDef(modules, defs) {
            var mod, i, def, meta, updates = [];
            for (i = 0; modules[i]; i++) { //给每一个模块指定加载的信息(deps, factory)
                mod = modules[i];
                while (def = defs.shift()) {
                    if (!def.name) {
                        updates.push([mod, def]);
                        break;
                    } else {
                        def.meta = meta = resolve(def.name, cfgBase); //def是全局的
                        if (meta.id == mod.id) {
                            updates.push([mod, def]);
                            break;
                        } else {
                            globalDefs[meta.id] = def;
                        }
                    }
                }
                def || updates.push([mod]); //容错，如果没有找到还是得添加空模块
            } //for modules
            while (def = defs.pop()) { //保存还未处理的具名模块，这样一个文件就可以定义多个模块了
                if (def.name) {
                    def.meta = resolve(def.name, cfgBase);
                    globalDefs[def.meta.id] = def;
                }
            }
            return updates;
        }

        /**
         *检查模块是否有循环依赖
         */
        function isCircular(childModule, depParents, container) {
            function check(child, parents) {
                for (var module, i = 0; module = parents[i]; i++) {
                    container.push(module);
                    if (child == module) return true;
                    else {
                        if (check(child, module.parents, container)) return true;
                        container.pop();
                    }
                }
                return false;
            }
            if (check(childModule, depParents)) {
                noticeCircular(container);
                return true;
            }
        }

        /**
         * 新建一个运行器，用以完成模块的加载
         * @returns {Module} 返回新生成的模块运行器
         */
        function createModuleLoader(meta, deps, cb) {
            var id = meta.id,
                module = new Module();
            module.meta = meta;
            if (cb) module.factory = cb;
            module.deps = deps || [];
            if (id) {
                module.id = id;
                module.mod = { //!important real module wrapper
                    exports: {},
                    id: id,
                    uri: meta.url
                }
                moduleRunners[id] = module;
            }
            emit("new", [module]); //root模块也要触发这个事件
            return module;
        }

        //{{{Module.prototype 上的方法，分离出来是为了不向外暴露接口

        /**
         * 执行完成，保存模块，通知父module
         */
        function completeModuleLoader(t) {
            //保存这些数据，因为会马上清掉
            var moduleWrapper = t.mod,
                parents = t.parents,
                tid = t.id,
                meta = t.meta;
            //clear to free memory
            t.factory = t.mods = t.parents = t.deps = t.meta = t.mod = null;

            if (tid) {
                //保存模块(moduleWrapper)并通知父模块(module)加载完成
                moduleSets[meta.id] = moduleWrapper; //这里一定要用meta.id，插件才有修改保存模块id的可能
                //通知父模块，子模块加载完成
                for (var pModule, i = 0; pModule = parents[i]; i++) { //childComplete 通知父module模块已经加载完成
                    for (var j = 0; pModule.deps[j]; j++) {
                        if (pModule.deps[j].id == tid) {
                            pModule.mods[tid] = moduleWrapper; //pModule's childModule
                            pModule.wait--;
                            break;
                        }
                    }
                    if (pModule.wait == 0) {
                        pModule.emit("required") || pModule.exec();
                    }
                }
            }
        }

        /**
         *创建模块加载完成后执行factory需要加载其提供给的接口(require, exports, module)
         */
        function makeFactoryParam(t) {
            var params = [],
                specified, result, meta, i, deps = t.deps || [];
            for (i = 0; meta = deps[i]; i++) {
                specified = meta.specified;
                if (specified == "require") result = createRequireParam(t.meta.base, t.mod, t.mods, t.deps);
                else if (specified == "exports") result = t.mod.exports
                else if (specified == "module") result = t.mod; //详见amd规范说明
                else result = t.mods[meta.id].exports;
                params.push(result);
                meta = t.deps[i];
            }
            return params;
        }
        ///}}}Module.prototype extern function

        /**
         * 创建模块内使用的接口(require, specified, module).
         */
        function createRequireParam(base, thisModule, depModuleList, deps) {
            function inlineRequire(dep, callback) {
                if (isArray(dep)) { //数组依赖表示要异步加载模块
                    setTimeout(function() { //强制异步执行
                        globalRequire(dep, function() {
                            isFunction(callback) && callback.apply(null, arguments);
                        }, base);
                    });
                } else if (isString(dep)) { //标准异步获取已经加载完成的模块
                    for (var i = 0, id; deps[i]; i++) { //直接在依赖中寻找id，速度更快
                        if (deps[i].name == dep) {
                            id = deps[i].id;
                            break;
                        }
                    }
                    //循环依赖的模块已经包含在这里面
                    if (depModuleList[id] || moduleSets[id]) return (depModuleList[id] || moduleSets[id]).exports;
                    //这个时候说明依赖的可能是本身
                    if (id == thisModule.id) return thisModule.exports;
                }
            }

            inlineRequire.toUrl = function(name) { //这里不受alias影响
                return resolve(name, base).url;
            };
            return inlineRequire
        }
    })();

    ////////////////////////init//////////////////////////
    var fetch;
    (function() {
        var doc = document,
            interactiveScript,
            currentScript = null,
            useInteractive = false,
            interactiveDefs = [], //ie 脚本执行时临时定义存放
            globalDefQueue = [], //加载完成后的所有的定义
            baseUrl,
            head = getByName('head'),
            baseNode = getByName("base");

        var R_commentRegExp = /(\/\*([\s\S]*?)\*\/|([^:]|^)\/\/(.*)$)/mg,
            R_deps = /\brequire\s*\(\s*('|")([^'"]*)\1\s*\)/g,
            R_ready = /loaded|complete/;

        function getByName(name) {
            return doc.getElementsByTagName(name);
        }

        function getExecuteScript() {
            if (currentScript) return currentScript; //fix for ie cache

            //accelerate for multi define in a single file
            if (interactiveScript && interactiveScript.readyState === 'interactive') {
                return interactiveScript;
            }
            var scripts = getByName('script');
            interactiveScript = null;
            each(scripts, function(script) {
                if (script.readyState === 'interactive') {
                    return (interactiveScript = script);
                }
            });
            return interactiveScript;
        }

        /**
         *模块定义函数，被映射成全局define函数
         */
        var define = global.define = function(name, deps, callback) {
            //Allow for anonymous functions
            if (typeof name !== 'string') {
                //Adjust args appropriately
                callback = deps;
                deps = name;
                name = null;
            }

            //This module may not have dependencies
            if (!isArray(deps)) {
                callback = deps;
                /*paramDeps = */
                deps = [];
            }

            //notice that, only CommonJS thing need parse "require" calls in source.
            if (!deps.length && isFunction(callback)) {
                deps = ["require", "exports", "module"];
                callback.length && callback.toString()
                    .replace(R_commentRegExp, "")
                    .replace(R_deps, function(match, quote, dep) {
                    deps.push(dep);
                });
            }

            var def = {
                "name": name,
                "deps": deps,
                "factory": callback //notice callback is not always function
            };
            if (useInteractive) {
                interactiveDefs.push({
                    e: getExecuteScript(),
                    def: def
                });
            } else {
                globalDefQueue.push(def);
            }
        };
        //The minimum amd definition
        define.amd = {};

        //将fetch开放给plguin，是因为defList的处理是很灵活的，插件有时候需要完全自己来处理加载的资源
        //最后再给到加载器的核心进行运行
        sys.fetch = fetch = function(url, extractor) {
            var node = doc.createElement("script");
            node.type = "text/javascript";
            node.charset = cfg.charset || "utf-8";

            function endCallback() {
                head.removeChild(node);
                node = node.onload = node.onerror = onreadystatechange = null;
                extractor(globalDefQueue);
                globalDefQueue = []; //重新建立空的defQueque
            }

            if (useInteractive) {
                node.onreadystatechange = function() {
                    var queue = [];
                    if (node && R_ready.test(node.readyState)) {
                        var i = interactiveDefs.length - 1,
                            mdef;
                        for (; i >= 0; i--) {
                            mdef = interactiveDefs[i];
                            if (mdef.e === node) { //important!
                                delete mdef.e;
                                //不能把interactiveDefs直接设为null，因为可能有别的模块define
                                interactiveDefs.splice(i, 1);
                                queue.unshift(mdef.def);
                            }
                        } //end for
                        globalDefQueue = queue;
                        endCallback();
                    }
                };
            } else {
                node.onload = node.onerror = endCallback;
            }
            node.src = url;

            currentScript = node; //fix for IE cache
            baseNode ? head.insertBefore(node, baseNode) : head.appendChild(node);
            currentScript = null;
        };

        useInteractive = !! (doc.attachEvent && !(global.opera && opera.toString() == '[object Opera]'))
        head = head && head[0] || doc.body;
        baseNode = baseNode && baseNode[0];
        // see http://msdn.microsoft.com/en-us/library/ms536429(VS.85).aspx
        baseNode && (baseUrl = baseNode.hasAttribute ? baseNode["href"] // non-IE6/7
        : baseNode.getAttribute(attr, 4));
        createBase(baseUrl || location.href);
    })();

    require.plugin = function(pluginConstructor) {
        isFunction(pluginConstructor) && pluginConstructor(sys, util)
    }

    global.require = require;
})(this);
