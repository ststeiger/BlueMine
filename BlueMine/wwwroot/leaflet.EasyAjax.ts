
namespace Http
{


    interface IAjaxParameters
    {
        url: string,
        callbackName?: string,
        timeout?: number,

        method?: string,
        contentType?: string,
        queryData?: string | object,
        postData?: string | object,
        cache?: boolean

        user?: string,
        password?: string,

        onSuccess?(r: any): any,
        onError?(req: XMLHttpRequest | any): any,
        onNetworkError?(req: XMLHttpRequest): any,
        onCancel?(req: XMLHttpRequest): any,
        onAlways?(req: XMLHttpRequest | any): any,
        // onTimeout?(): any 
    }


    export abstract class RequestBase 
    {
        public Complete: boolean;
        protected m_url: string;
        protected m_data: string | object;
        protected m_params: IAjaxParameters;


        protected m_SuccessCallbacks: ((r) => any)[];
        protected m_CancelCallbacks: ((req: XMLHttpRequest) => any)[];
        protected m_NetworkFailureCallbacks: ((req: XMLHttpRequest) => any)[];
        protected m_FailureCallbacks: ((req: XMLHttpRequest) => any)[];
        protected m_CompleteCallbacks: ((req: XMLHttpRequest) => any)[];

        public constructor()
        {
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


        protected successCallback(): void
        {
            // console.log("successCallback");
            this.Complete = true;

            for (let i: number = 0; i < this.m_SuccessCallbacks.length; ++i)
            {
                this.m_SuccessCallbacks[i].apply(this, arguments);
            }
        }


        protected failureDefault(r): void
        {
            console.log("failure");
            console.log(r);
            let msg: string = "Error " + r.status + " (" + r.statusText + "): \n\n";
            msg += "URL: \n" + r.responseURL + "\n\n";
            msg += r.responseText;
            alert(msg);
        }


        protected failureCallback(): void
        {
            this.Complete = true;

            if (this.m_FailureCallbacks.length === 0)
                this.failureDefault.apply(this, arguments);

            for (let i: number = 0; i < this.m_FailureCallbacks.length; ++i)
            {
                this.m_FailureCallbacks[i].apply(this, arguments);
            }
        }


        protected cancelCallback(): void
        {
            this.Complete = true;

            for (let i: number = 0; i < this.m_CancelCallbacks.length; ++i)
            {
                this.m_CancelCallbacks[i].apply(this, arguments);
            }
        }


        protected networkFailureCallback(): void
        {
            this.Complete = true;

            if (this.m_NetworkFailureCallbacks.length === 0)
                this.failureDefault.apply(this, arguments);

            for (let i: number = 0; i < this.m_NetworkFailureCallbacks.length; ++i)
            {
                this.m_NetworkFailureCallbacks[i].apply(this, arguments);
            }
        }


        protected alwaysCallback(): void
        {
            this.Complete = true;

            for (let i: number = 0; i < this.m_CompleteCallbacks.length; ++i)
            {
                this.m_CompleteCallbacks[i].apply(this, arguments);
            }
        }


        public success(cb: (r: any) => any): RequestBase
        {
            if (cb != null)
                this.m_SuccessCallbacks.push(cb);
            else
                Error("Success-callback is NULL or UNDEFINED.");

            return this;
        }

        public cancel(cb: (req: XMLHttpRequest) => any): RequestBase
        {
            if (cb != null)
                this.m_CancelCallbacks.push(cb);
            else
                Error("Cancel-callback is NULL or UNDEFINED.");

            return this;
        }

        public networkFailure(cb: (req: XMLHttpRequest) => any): RequestBase
        {
            if (cb != null)
                this.m_NetworkFailureCallbacks.push(cb);
            else
                Error("Network-failure-callback is NULL or UNDEFINED.");

            return this;
        }


        public failure(cb: (req: XMLHttpRequest) => any): RequestBase
        {
            if (cb != null)
                this.m_FailureCallbacks.push(cb);
            else
                Error("Failure-callback is NULL or UNDEFINED.");

            return this;
        }


        public always(cb: (req: XMLHttpRequest) => any): RequestBase
        {
            if (cb != null)
                this.m_CompleteCallbacks.push(cb);
            else
                Error("Always-callback is NULL or UNDEFINED.");

            return this;
        }


        protected sendRequest(options: IAjaxParameters): RequestBase
        {
            this.Complete = false;

            options = options || <IAjaxParameters>{};

            if (options.onNetworkError != null)
                this.networkFailure(options.onNetworkError);

            if (options.onError != null)
                this.failure(options.onError);

            if (options.onCancel != null)
                this.cancel(options.onCancel);

            if (options.onSuccess != null)
                this.success(options.onSuccess);

            if (options.onAlways != null)
                this.always(options.onAlways);


            options.onSuccess = this.successCallback.bind(this);
            options.onError = this.failureCallback.bind(this);
            options.onCancel = this.cancelCallback.bind(this);
            options.onAlways = this.alwaysCallback.bind(this);

            let postData: string = null, url: string = options.url;

            if (options.method == null)
                options.method = "GET";

            if (options.contentType == null)
                options.contentType = 'application/urlencode';

            if (options.cache == null || options.cache == false)
            {
                url += ((options.url.indexOf('?') === -1) ? "?" : "&") + "no_cache=" + (new Date()).getTime();
            }

            if (options.queryData != null)
            {
                if (!(typeof options.queryData == 'string' || options.queryData instanceof String))
                {
                    let query = [];
                    for (let i = 0, keys = Object.keys(options.queryData); i < keys.length; i++)
                    {
                        query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(options.queryData[keys[i]]));
                    }

                    url += ((options.url.indexOf('?') === -1) ? "?" : "&") + query.join('&');
                }
                else
                {
                    let qs: string = <string>options.queryData;
                    if (qs.substr(0, 1) === "?" || qs.substr(0, 1) === "&")
                    {
                        qs = qs.substr(1);
                    }

                    url += ((options.url.indexOf('?') === -1) ? "?" : "&") + qs;
                }

            } // End if (options.queryData != null) 

            if (options.postData != null)
            {
                if (options.method != null && options.method.toLowerCase() !== "post")
                {
                    Error("Can't have postData when method is not POST.");
                }

                if (options.method == null)
                {
                    options.method = "POST";
                }

                if (options.contentType == null)
                    options.contentType = 'application/x-www-form-urlencoded';


                if (options.contentType.indexOf("application/json") != -1)
                {
                    if (!(typeof options.postData == 'string' || options.postData instanceof String))
                        postData = JSON.stringify(postData);
                    else
                        postData = <string>options.postData;
                }
                else
                {
                    if (!(typeof options.postData == 'string' || options.postData instanceof String))
                    {
                        let query = [];
                        for (let i = 0, keys = Object.keys(options.postData); i < keys.length; i++)
                        {
                            query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(options.postData[keys[i]]));
                        }
                        postData = query.join('&');
                    }
                    else
                        postData = <string>options.postData;
                }

            } // End if (options.postData != null) 

            let req: XMLHttpRequest = new XMLHttpRequest();

            if (req == null)
            {
                if (options.onError != null)
                    options.onCancel(req);
                else
                    alert("Browser doesn't support XmlHttpRequest...");

                return;
            } // End if (req == null) 


            // var online = navigator.onLine;
            // Being online only entails that you are connected to a network, 
            // not the availability nor reachability of the services you are trying to connect to.

            // onerror fires when there is a failure on the network level.
            // If the error only exists on the application level, e.g., an HTTP error code is sent,
            // then onload still fires.
            // You need to test the returned status code explicitly in your onreadystatechange handler.
            req.onerror = function (this: XMLHttpRequestEventTarget, ev: ErrorEvent): any
            {
                console.log("req.onerror:", this, ev);

                if (options.onNetworkError != null)
                    options.onNetworkError(req);
                else if (options.onError != null)
                    options.onError(req);
                else
                    alert("There was an unexpected network error.\nSee the console log.\nError Details:\n" + ev);

                options.onAlways(req);

                return false;
            };

            if (options.user != null && options.password != null)
                req.open(options.method, url, true, options.user, options.password);
            else
                req.open(options.method, url, true);

            // req.setRequestHeader('User-Agent', 'XMLHTTP/1.0');
            if (options.cache !== true)
                req.setRequestHeader('cache-control', 'no-cache');

            if (options.contentType != null)
                req.setRequestHeader('Content-type', options.contentType);

            req.onreadystatechange = function ()
            {
                if (req.readyState != 4) return;

                if (!(req.status != 200 && req.status != 304 && req.status != 0))
                {
                    if (options.onSuccess != null)
                    {
                        if (options.contentType.toLowerCase().indexOf("application/json") !== -1)
                        {
                            let obj = null, jsonParseSuccessful = false;

                            try
                            {
                                obj = JSON.parse(req.responseText);
                                jsonParseSuccessful = true;
                            }
                            catch (e)
                            {
                                console.log(e.name);
                                console.log(e.message);
                                console.log(e.stack);
                                console.log(e);
                                console.log(req);
                                console.log(req.responseText);

                                if (options.onError != null)
                                    options.onError(req);
                                else
                                {
                                    alert('HTTP error ' + req.status);
                                    console.log(req);
                                }

                            } // End Catch 

                            if (jsonParseSuccessful)
                            {
                                if (obj.error == null)
                                {
                                    if (options.onSuccess != null)
                                    {
                                        if (obj.data != null)
                                            options.onSuccess(obj.data);
                                        else
                                            options.onSuccess(obj);
                                    } // End if (options.onSuccess != null) 
                                }
                                else
                                {
                                    if (options.onError != null)
                                    {
                                        console.log('JSON-parsing successful - but result indicates "server error": ', JSON.stringify(obj.error, null, 2))
                                        options.onError(obj.error);
                                    }
                                    else
                                    {
                                        console.log('JSON-parsing successful - but result indicates "server error": ', JSON.stringify(obj.error, null, 2));
                                        alert('Server error:\n' + JSON.stringify(obj.error, null, 2));
                                    }
                                }

                            } // End if (jsonParseSuccessful) 

                        } // End if (contentType.toLowerCase().indexOf("application/json") !== -1) 
                        else
                        {
                            if (options.onSuccess != null)
                                options.onSuccess(req.responseText);
                        }

                    } // End if (onSuccess)

                } // End if (!(req.status != 200 && req.status != 304 && req.status != 0)) 

                if (req.status != 200 && req.status != 304 && req.status != 0)
                {
                    if (options.onError != null)
                        options.onError(req);
                    else
                    {
                        alert('HTTP error ' + req.status);
                        // return;
                    }
                }

                if (req.status === 304 || req.status === 0)
                {
                    if (options.onCancel != null)
                        options.onCancel(req);
                    else
                    {
                        alert('HTTP request cancelled.');
                    }
                }

                if (options.onAlways)
                    options.onAlways(req);
            } // End req.onreadystatechange =  function() {

            if (req.readyState == 4) return;

            req.send(postData);

            return this;
        } // End Function sendRequest 


        protected sendPromise(options: IAjaxParameters): Promise<any>
        {
            return new Promise(
                function (resolve, reject)
                {
                    options.onSuccess = function (result: any)
                    {
                        // console.log("onSuccess:", arguments);

                        resolve(result);
                    };

                    options.onError = function (xhr: XMLHttpRequest)
                    {
                        console.log("onError", arguments);

                        reject(arguments);
                    };

                    options.onCancel = function (xhr: XMLHttpRequest)
                    {
                        console.log("onCancel", arguments);

                        reject(arguments);
                    };

                    options.onAlways = function (xhr: XMLHttpRequest) { };

                    this.sendRequest(options);
                }.bind(this)
            );
        } // End Function sendPromise 


        public send(params?: IAjaxParameters): RequestBase
        {
            return this.sendRequest(params || this.m_params);
        }


        public sendAsync(params?: IAjaxParameters): Promise<any>
        {
            return this.sendPromise(params || this.m_params);
        }


    } // End abstract class RequestBase 


    export class Get extends RequestBase
    {
        constructor(url: string, data?: string | object, success?: any)
        {
            super();
            this.send.bind(this);
            this.sendAsync.bind(this);

            if (success != null)
                this.success(success);

            this.m_params = {
                url: url,
                queryData: data,
                method: "GET"
            };
        }

    } // End Class Get


    export class Post extends RequestBase
    {

        constructor(url: string, data?: string | object, success?: any)
        {
            super();

            this.send.bind(this);
            this.sendAsync.bind(this);

            if (success != null)
                this.success(success);

            this.m_params = {
                url: url,
                postData: data,
                method: "POST",
                contentType: "application/x-www-form-urlencoded"
            };
        }

    } // End Class Post


    export class Json extends RequestBase
    {
        constructor(url: string, data?: string | object, success?: any)
        {
            super();
            this.send.bind(this);
            this.sendAsync.bind(this);

            if (success != null)
                this.success(success);

            this.m_params = {
                url: url,
                method: "POST",
                contentType: "application/json; charset=UTF-8",
                postData: data
            };
        }

    } // End Class Json 

    export class Ajax extends RequestBase
    {

        constructor(params: IAjaxParameters)
        {
            super();
            this.send.bind(this);
            this.sendAsync.bind(this);
            this.m_params = params;
        }

    } // End Class Ajax 


    export class JSONP extends RequestBase
    {

        constructor(params?: IAjaxParameters)
        {
            super();
            this.uuid.bind(this);
            this.removeElement.bind(this);
            this.sendRequest.bind(this);
            this.sendPromise.bind(this);

            if (params != null)
                this.m_params = params;
        }


        public uuid(): string
        {
            function s4(): string
            {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                s4() + '-' + s4() + s4() + s4();
        }

        public removeElement(id: string)
        {
            let el = document.getElementById(id);
            if (el != null)
                el.parentElement.removeChild(el);
        }

        protected sendRequest(options: IAjaxParameters)
        {
            this.Complete = false;
            let id: string = this.uuid();

            if (options == null || options.url == null)
                Error("options or options.url not defined.");

            // Bind for RequestChain
            if (options.onSuccess != null)
                this.success(options.onSuccess);

            // Bind for RequestChain
            if (options.onError != null)
                this.failure(options.onError);
            else
            {
                this.failure(function (args)
                {
                    alert('JsonP-request to "' + options.url + '" failed.');
                    console.log(args);
                });
            }

            // Bind for RequestChain
            if (options.onAlways != null)
                this.always(options.onAlways);

            // Bind for RequestChain
            if (options.onCancel != null)
                this.cancel(options.onCancel);
            else
            {
                this.cancel(function (args)
                {
                    alert('JsonP-request to "' + options.url + '" timed out...');
                    console.log(args);
                });
            }

            options.onSuccess = this.successCallback.bind(this);
            options.onError = this.failureCallback.bind(this);
            options.onAlways = this.alwaysCallback.bind(this);
            options.onCancel = this.cancelCallback.bind(this);

            // replace first occurence only...
            // 

            let url: string = options.url,
                callback_name: string = options.callbackName || 'callback',
                on_success: (data: any) => void = options.onSuccess || function (data) { },
                on_error: (args: any) => void = options.onError || function (args: any) { },
                on_always: (args: any) => void = options.onAlways || function (args: any) { },
                on_cancel: (args: any) => void = options.onCancel || function (args: any) { },
                timeout: number = options.timeout || 10; // sec

            if (url.indexOf('?') == -1)
                url += "?callback={@callback}";
            else
                url += "&callback={@callback}";

            url = url.replace("{@callback}", encodeURIComponent(callback_name));

            if (options.queryData != null)
            {
                if (!(typeof options.queryData == 'string' || options.queryData instanceof String))
                {
                    let query = [];
                    for (let i = 0, keys = Object.keys(options.queryData); i < keys.length; i++)
                    {
                        query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(options.queryData[keys[i]]));
                    }

                    url += "&" + query.join('&');
                }
                else
                {
                    let qs: string = <string>options.queryData;
                    if (qs.substr(0, 1) === "?" || qs.substr(0, 1) === "&")
                    {
                        qs = qs.substr(1);
                    }

                    url += "&" + qs;
                }

            } // End if (options.queryData != null) 



            let timeout_trigger: number = window.setTimeout(function ()
            {
                window[callback_name] = function () { };
                console.log("JSONP: script.ontimeout", arguments);
                on_cancel(arguments); // timeout 
                on_always(arguments);
                this.removeElement(id);
            }.bind(this), timeout * 1000);

            window[callback_name] = function (data)
            {
                window.clearTimeout(timeout_trigger);
                on_success(data);
                on_always(data);
                this.removeElement(id);
            }.bind(this);

            let script: HTMLScriptElement = document.createElement('script');
            script.id = id;
            script.type = 'application/javascript';
            script.async = true;
            script.src = url;
            script.onerror = function (that: HTMLElement, ev: ErrorEvent)
            {
                window.clearTimeout(timeout_trigger);
                console.log("JSONP: script.onerror", arguments);
                on_error(arguments);
                on_always(arguments);
                this.removeElement(id);
            }.bind(this);

            document.getElementsByTagName('head')[0].appendChild(script);

            return this;
        }


        protected sendPromise(options?: IAjaxParameters): Promise<any>
        {
            if (typeof options === 'string' || options instanceof String)
            {
                options = { url: <string><any>options };
            }

            if (options == null || options.url == null)
                Error("options or options.url not defined.");

            return new Promise(
                function (resolve, reject)
                {
                    options.onSuccess = function (result: any)
                    {
                        // console.log("onSuccess:", arguments);
                        resolve(result);
                    };

                    options.onError = function ()
                    {
                        console.log("onError", arguments);
                        reject(arguments);
                    };

                    options.onCancel = function ()
                    {
                        console.log("onCancel", arguments);
                        reject(arguments);
                    };

                    // options.timeout = 10;
                    // options.callbackName = "callback";

                    this.sendRequest(options);
                }.bind(this)
            );
        } // End Function sendPromise 


    } // End Class JSONP  


} // End Namespace HTTP 
