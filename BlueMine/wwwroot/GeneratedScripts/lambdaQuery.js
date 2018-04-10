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
    lambdaQuery.prototype.appendTo = function (el) { };
    lambdaQuery.prototype.clone = function () { };
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
        return this;
    };
    lambdaQuery.prototype.css = function (propertyName, value, priority) {
        this.element.style.setProperty(propertyName, value, priority);
    };
    lambdaQuery.prototype.delegate = function (selector, eventType, handler) {
    };
    lambdaQuery.prototype.hide = function () {
    };
    lambdaQuery.prototype.add = function (html) {
    };
    lambdaQuery.prototype.addClass = function (className) {
    };
    return lambdaQuery;
}());
export { lambdaQuery };
