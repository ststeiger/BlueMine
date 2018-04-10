var lambdaQuery = (function () {
    function lambdaQuery(arg) {
        if (typeof (arg) === 'string') {
            this.element = document.querySelector(arg);
        }
        this.element = arg;
    }
    lambdaQuery.prototype.is = function (selector) {
        return this.element.matches(selector);
    };
    lambdaQuery.prototype.appendTo = function (targetSelector) {
        if (typeof targetSelector === 'string' || targetSelector instanceof String)
            document.querySelector(targetSelector).appendChild(this.element);
        else
            targetSelector.appendChild(this.element);
    };
    lambdaQuery.prototype.clone = function () {
        return this.element.cloneNode(true);
    };
    lambdaQuery.prototype.empty = function (e) {
        var elem = e || this.element;
        for (elem = elem.firstChild; elem; elem = elem.nextSibling) {
            if (elem.nodeType < 6) {
                return false;
            }
        }
        return true;
    };
    lambdaQuery.prototype.extend = function (deep, target, object1) {
        var objects = [];
        for (var _i = 3; _i < arguments.length; _i++) {
            objects[_i - 3] = arguments[_i];
        }
        var extended = {};
        var i = 0;
        var length = arguments.length;
        if (Object.prototype.toString.call(arguments[0]) === '[object Boolean]') {
            deep = arguments[0];
            i++;
        }
        var merge = function (obj) {
            for (var prop in obj) {
                if (Object.prototype.hasOwnProperty.call(obj, prop)) {
                    if (deep && Object.prototype.toString.call(obj[prop]) === '[object Object]') {
                        extended[prop] = this.extend(true, extended[prop], obj[prop]);
                    }
                    else {
                        extended[prop] = obj[prop];
                    }
                }
            }
        };
        for (; i < length; i++) {
            var obj = arguments[i];
            merge(obj);
        }
        return extended;
    };
    lambdaQuery.prototype.css = function (propertyName, value, priority) {
        this.element.style.setProperty(propertyName, value, priority);
    };
    lambdaQuery.prototype.delegate = function (selector, eventType, handler) {
    };
    lambdaQuery.prototype.addEvent = function (el, eventType, handler) {
        if (el.addEventListener) {
            el.addEventListener(eventType, handler, false);
        }
        else if (el.attachEvent) {
            el.attachEvent('on' + eventType, handler);
        }
        else {
            el['on' + eventType] = handler;
        }
    };
    lambdaQuery.prototype.bind = function (eventType, handler) {
        var events = eventType.split(' ');
        for (var i = 0; i < events.length; ++i) {
            this.addEvent(this.element, events[i], handler);
        }
        return this;
    };
    lambdaQuery.prototype.add = function (el) {
        this.element.appendChild(el);
    };
    lambdaQuery.prototype.hide = function () {
        if (this.element.style.display !== "none") {
            this.element.style.display = "none";
        }
    };
    lambdaQuery.prototype.addClass = function (className) {
        if (!this.element.classList.contains(className))
            this.element.classList.add(className);
    };
    lambdaQuery.prototype.grep = function (array, fn, invert) {
        var results = [], e;
        invert = !!invert;
        for (var i = 0, length_1 = array.length; i < length_1; i++)
            e = !!fn(array[i], i), invert !== e && results.push(array[i]);
        return results;
    };
    lambdaQuery.prototype.Event = function (src, props) {
        var ev = null;
        return ev;
    };
    lambdaQuery.prototype.each = function (obj, callback) {
        var length, i = 0;
        if (Array.isArray(obj)) {
            length = obj.length;
            for (; i < length; i++) {
                if (callback.call(obj[i], i, obj[i]) === false) {
                    break;
                }
            }
        }
        else {
            for (i in obj) {
                if (callback.call(obj[i], i, obj[i]) === false) {
                    break;
                }
            }
        }
        return obj;
    };
    lambdaQuery.prototype.map = function (array, callback) {
        var newArray = [];
        for (var i = 0; i < array.length; ++i) {
            newArray[i] = callback(array[i]);
        }
        return newArray;
    };
    return lambdaQuery;
}());
function λ(selector) {
    return new lambdaQuery(selector);
}
λ("body");
