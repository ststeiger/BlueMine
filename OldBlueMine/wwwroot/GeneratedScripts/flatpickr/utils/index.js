export var pad = function (number) { return ("0" + number).slice(-2); };
export var int = function (bool) { return (bool === true ? 1 : 0); };
export function debounce(func, wait, immediate) {
    if (immediate === void 0) { immediate = false; }
    var timeout;
    return function () {
        var context = this, args = arguments;
        timeout !== null && clearTimeout(timeout);
        timeout = window.setTimeout(function () {
            timeout = null;
            if (!immediate)
                func.apply(context, args);
        }, wait);
        if (immediate && !timeout)
            func.apply(context, args);
    };
}
export var arrayify = function (obj) {
    return obj instanceof Array ? obj : [obj];
};
