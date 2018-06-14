var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Http;
(function (Http) {
    var RequestBase = (function () {
        function RequestBase() {
            this.Complete = false;
            this.m_SuccessCallbacks = [];
            this.m_CancelCallbacks = [];
            this.m_NetworkFailureCallbacks = [];
            this.m_FailureCallbacks = [];
            this.m_CompleteCallbacks = [];
            this.sendPromise.bind(this);
            this.sendRequest.bind(this);
            this.failureDefault.bind(this);
            this.successCallback.bind(this);
            this.cancelCallback.bind(this);
            this.networkFailureCallback.bind(this);
            this.failureCallback.bind(this);
            this.alwaysCallback.bind(this);
            this.success.bind(this);
            this.cancel.bind(this);
            this.networkFailure.bind(this);
            this.failure.bind(this);
            this.always.bind(this);
        }
        RequestBase.prototype.successCallback = function () {
            this.Complete = true;
            for (var i = 0; i < this.m_SuccessCallbacks.length; ++i) {
                this.m_SuccessCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.failureDefault = function (r) {
            console.log("failure");
            console.log(r);
            var msg = "Error " + r.status + " (" + r.statusText + "): \n\n";
            msg += "URL: \n" + r.responseURL + "\n\n";
            msg += r.responseText;
            alert(msg);
        };
        RequestBase.prototype.failureCallback = function () {
            this.Complete = true;
            if (this.m_FailureCallbacks.length === 0)
                this.failureDefault.apply(this, arguments);
            for (var i = 0; i < this.m_FailureCallbacks.length; ++i) {
                this.m_FailureCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.cancelCallback = function () {
            this.Complete = true;
            for (var i = 0; i < this.m_CancelCallbacks.length; ++i) {
                this.m_CancelCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.networkFailureCallback = function () {
            this.Complete = true;
            if (this.m_NetworkFailureCallbacks.length === 0)
                this.failureDefault.apply(this, arguments);
            for (var i = 0; i < this.m_NetworkFailureCallbacks.length; ++i) {
                this.m_NetworkFailureCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.alwaysCallback = function () {
            this.Complete = true;
            for (var i = 0; i < this.m_CompleteCallbacks.length; ++i) {
                this.m_CompleteCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.success = function (cb) {
            if (cb != null)
                this.m_SuccessCallbacks.push(cb);
            else
                Error("Success-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.cancel = function (cb) {
            if (cb != null)
                this.m_CancelCallbacks.push(cb);
            else
                Error("Cancel-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.networkFailure = function (cb) {
            if (cb != null)
                this.m_NetworkFailureCallbacks.push(cb);
            else
                Error("Network-failure-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.failure = function (cb) {
            if (cb != null)
                this.m_FailureCallbacks.push(cb);
            else
                Error("Failure-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.always = function (cb) {
            if (cb != null)
                this.m_CompleteCallbacks.push(cb);
            else
                Error("Always-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.sendRequest = function (options) {
            this.Complete = false;
            options.onSuccess = this.successCallback.bind(this);
            options.onError = this.failureCallback.bind(this);
            options.onCancel = this.cancelCallback.bind(this);
            options.onAlways = this.alwaysCallback.bind(this);
            var postData = null, url = options.url;
            if (options.method == null)
                options.method = "GET";
            if (options.contentType == null)
                options.contentType = 'application/urlencode';
            if (options.cache == null || options.cache == false) {
                url += ((options.url.indexOf('?') === -1) ? "?" : "&") + "no_cache=" + (new Date()).getTime();
            }
            if (options.queryData != null) {
                if (!(typeof options.queryData == 'string' || options.queryData instanceof String)) {
                    var query = [];
                    for (var i = 0, keys = Object.keys(options.queryData); i < keys.length; i++) {
                        query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(options.queryData[keys[i]]));
                    }
                    url += ((options.url.indexOf('?') === -1) ? "?" : "&") + query.join('&');
                }
                else {
                    var qs = options.queryData;
                    if (qs.substr(0, 1) === "?" || qs.substr(0, 1) === "&") {
                        qs = qs.substr(1);
                    }
                    url += ((options.url.indexOf('?') === -1) ? "?" : "&") + qs;
                }
            }
            if (options.postData != null) {
                if (options.method != null && options.method.toLowerCase() !== "post") {
                    Error("Can't have postData when method is not POST.");
                }
                if (options.method == null) {
                    options.method = "POST";
                }
                if (options.contentType == null)
                    options.contentType = 'application/x-www-form-urlencoded';
                if (options.contentType.indexOf("application/json") != -1) {
                    if (!(typeof options.postData == 'string' || options.postData instanceof String))
                        postData = JSON.stringify(postData);
                    else
                        postData = options.postData;
                }
                else {
                    if (!(typeof options.postData == 'string' || options.postData instanceof String)) {
                        var query = [];
                        for (var i = 0, keys = Object.keys(options.postData); i < keys.length; i++) {
                            query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(options.postData[keys[i]]));
                        }
                        postData = query.join('&');
                    }
                    else
                        postData = options.postData;
                }
            }
            var req = new XMLHttpRequest();
            if (req == null) {
                if (options.onError != null)
                    options.onCancel(req);
                else
                    alert("Browser doesn't support XmlHttpRequest...");
                return;
            }
            req.onerror = function (ev) {
                console.log("req.onerror:", this, ev);
                if (options.onNetworkError != null)
                    options.onNetworkError(req);
                else if (options.onError != null)
                    options.onError(req);
                else
                    alert("There was an unexpected network error.\nSee the console log.\nError Details:\n" + ev);
                return false;
            };
            if (options.user != null && options.password != null)
                req.open(options.method, url, true, options.user, options.password);
            else
                req.open(options.method, url, true);
            if (options.cache !== true)
                req.setRequestHeader('cache-control', 'no-cache');
            if (options.contentType != null)
                req.setRequestHeader('Content-type', options.contentType);
            req.onreadystatechange = function () {
                if (req.readyState != 4)
                    return;
                if (!(req.status != 200 && req.status != 304 && req.status != 0)) {
                    if (options.onSuccess != null) {
                        if (options.contentType.toLowerCase().indexOf("application/json") !== -1) {
                            var jsonParseSuccessful = false;
                            try {
                                var obj = JSON.parse(req.responseText);
                                jsonParseSuccessful = true;
                            }
                            catch (e) {
                                console.log(e.name);
                                console.log(e.message);
                                console.log(e.stack);
                                console.log(e);
                                console.log(req);
                                console.log(req.responseText);
                                if (options.onError != null)
                                    options.onError(req);
                                else {
                                    alert('HTTP error ' + req.status);
                                    console.log(req);
                                }
                            }
                            if (jsonParseSuccessful) {
                                if (obj.error == null) {
                                    if (options.onSuccess != null) {
                                        if (obj.data != null)
                                            options.onSuccess(obj.data);
                                        else
                                            options.onSuccess(obj);
                                    }
                                }
                                else {
                                    if (options.onError != null) {
                                        console.log('JSON-parsing successful - but result indicates "server error": ', JSON.stringify(obj.error, null, 2));
                                        options.onError(obj.error);
                                    }
                                    else {
                                        console.log('JSON-parsing successful - but result indicates "server error": ', JSON.stringify(obj.error, null, 2));
                                        alert('Server error:\n' + JSON.stringify(obj.error, null, 2));
                                    }
                                }
                            }
                        }
                        else {
                            if (options.onSuccess != null)
                                options.onSuccess(req.responseText);
                        }
                    }
                }
                if (req.status != 200 && req.status != 304 && req.status != 0) {
                    if (options.onError != null)
                        options.onError(req);
                    else {
                        alert('HTTP error ' + req.status);
                    }
                }
                if (req.status === 304 || req.status === 0) {
                    if (options.onCancel != null)
                        options.onCancel(req);
                    else {
                        alert('HTTP request cancelled.');
                    }
                }
                if (options.onAlways)
                    options.onAlways(req);
            };
            if (req.readyState == 4)
                return;
            req.send(postData);
            return this;
        };
        RequestBase.prototype.sendPromise = function (options) {
            return new Promise(function (resolve, reject) {
                options.onSuccess = function (result) {
                    resolve(result);
                };
                options.onError = function (xhr) {
                    console.log("onError", arguments);
                    reject(arguments);
                };
                options.onCancel = function (xhr) {
                    console.log("onCancel", arguments);
                    reject(arguments);
                };
                options.onAlways = function (xhr) { };
                this.sendRequest(options);
            }.bind(this));
        };
        RequestBase.prototype.send = function (params) {
            return this.sendRequest(params || this.m_params);
        };
        RequestBase.prototype.sendAsync = function (params) {
            return this.sendPromise(params || this.m_params);
        };
        return RequestBase;
    }());
    Http.RequestBase = RequestBase;
    var Get = (function (_super) {
        __extends(Get, _super);
        function Get(url, data, success) {
            var _this = _super.call(this) || this;
            _this.send.bind(_this);
            _this.sendAsync.bind(_this);
            if (success != null)
                _this.success(success);
            _this.m_params = {
                url: url,
                queryData: data,
                method: "GET"
            };
            return _this;
        }
        return Get;
    }(RequestBase));
    Http.Get = Get;
    var Post = (function (_super) {
        __extends(Post, _super);
        function Post(url, data, success) {
            var _this = _super.call(this) || this;
            _this.send.bind(_this);
            _this.sendAsync.bind(_this);
            if (success != null)
                _this.success(success);
            _this.m_params = {
                url: url,
                postData: data,
                method: "POST",
                contentType: "application/x-www-form-urlencoded"
            };
            return _this;
        }
        return Post;
    }(RequestBase));
    Http.Post = Post;
    var Json = (function (_super) {
        __extends(Json, _super);
        function Json(url, data, success) {
            var _this = _super.call(this) || this;
            _this.send.bind(_this);
            _this.sendAsync.bind(_this);
            if (success != null)
                _this.success(success);
            _this.m_params = {
                url: url,
                method: "POST",
                contentType: "application/json; charset=UTF-8",
                postData: data
            };
            return _this;
        }
        return Json;
    }(RequestBase));
    Http.Json = Json;
    var Ajax = (function (_super) {
        __extends(Ajax, _super);
        function Ajax(params) {
            var _this = _super.call(this) || this;
            _this.send.bind(_this);
            _this.sendAsync.bind(_this);
            _this.m_params = params;
            if (params.onSuccess != null)
                _this.success(params.onSuccess);
            if (params.onError != null)
                _this.failure(params.onError);
            if (params.onCancel != null)
                _this.cancel(params.onCancel);
            if (params.onAlways != null)
                _this.always(params.onAlways);
            return _this;
        }
        return Ajax;
    }(RequestBase));
    Http.Ajax = Ajax;
    var JSONP = (function (_super) {
        __extends(JSONP, _super);
        function JSONP(params) {
            var _this = _super.call(this) || this;
            _this.uuid.bind(_this);
            _this.removeElement.bind(_this);
            _this.sendRequest.bind(_this);
            _this.sendPromise.bind(_this);
            if (params != null)
                _this.m_params = params;
            return _this;
        }
        JSONP.prototype.uuid = function () {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                s4() + '-' + s4() + s4() + s4();
        };
        JSONP.prototype.removeElement = function (id) {
            var el = document.getElementById(id);
            if (el != null)
                el.parentElement.removeChild(el);
        };
        JSONP.prototype.sendRequest = function (options) {
            this.Complete = false;
            var id = this.uuid();
            if (options == null || options.url == null)
                Error("options or options.url not defined.");
            if (options.onSuccess != null)
                this.success(options.onSuccess);
            if (options.onError != null)
                this.failure(options.onError);
            else {
                this.failure(function (args) {
                    alert('JsonP-request to "' + options.url + '" failed.');
                    console.log(args);
                });
            }
            if (options.onAlways != null)
                this.always(options.onAlways);
            if (options.onCancel != null)
                this.cancel(options.onCancel);
            else {
                this.cancel(function (args) {
                    alert('JsonP-request to "' + options.url + '" timed out...');
                    console.log(args);
                });
            }
            options.onSuccess = this.successCallback.bind(this);
            options.onError = this.failureCallback.bind(this);
            options.onAlways = this.alwaysCallback.bind(this);
            options.onCancel = this.cancelCallback.bind(this);
            var url = options.url, callback_name = options.callbackName || 'callback', on_success = options.onSuccess || function (data) { }, on_error = options.onError || function (args) { }, on_always = options.onAlways || function (args) { }, on_cancel = options.onCancel || function (args) { }, timeout = options.timeout || 10;
            if (url.indexOf('?') == -1)
                url += "?callback={@callback}";
            else
                url += "&callback={@callback}";
            url = url.replace("{@callback}", encodeURIComponent(callback_name));
            if (options.queryData != null) {
                if (!(typeof options.queryData == 'string' || options.queryData instanceof String)) {
                    var query = [];
                    for (var i = 0, keys = Object.keys(options.queryData); i < keys.length; i++) {
                        query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(options.queryData[keys[i]]));
                    }
                    url += "&" + query.join('&');
                }
                else {
                    var qs = options.queryData;
                    if (qs.substr(0, 1) === "?" || qs.substr(0, 1) === "&") {
                        qs = qs.substr(1);
                    }
                    url += "&" + qs;
                }
            }
            var timeout_trigger = window.setTimeout(function () {
                window[callback_name] = function () { };
                console.log("JSONP: script.ontimeout", arguments);
                on_cancel(arguments);
                on_always(arguments);
                this.removeElement(id);
            }.bind(this), timeout * 1000);
            window[callback_name] = function (data) {
                window.clearTimeout(timeout_trigger);
                on_success(data);
                on_always(data);
                this.removeElement(id);
            }.bind(this);
            var script = document.createElement('script');
            script.id = id;
            script.type = 'application/javascript';
            script.async = true;
            script.src = url;
            script.onerror = function (that, ev) {
                window.clearTimeout(timeout_trigger);
                console.log("JSONP: script.onerror", arguments);
                on_error(arguments);
                on_always(arguments);
                this.removeElement(id);
            }.bind(this);
            document.getElementsByTagName('head')[0].appendChild(script);
            return this;
        };
        JSONP.prototype.sendPromise = function (options) {
            if (typeof options === 'string' || options instanceof String) {
                options = { url: options };
            }
            if (options == null || options.url == null)
                Error("options or options.url not defined.");
            return new Promise(function (resolve, reject) {
                options.onSuccess = function (result) {
                    resolve(result);
                };
                options.onError = function () {
                    console.log("onError", arguments);
                    reject(arguments);
                };
                options.onCancel = function () {
                    console.log("onCancel", arguments);
                    reject(arguments);
                };
                this.sendRequest(options);
            }.bind(this));
        };
        return JSONP;
    }(RequestBase));
    Http.JSONP = JSONP;
})(Http || (Http = {}));
