'use strict';
if (![].fill) {
    Array.prototype.fill = function (value) {
        var O = Object(this);
        var len = parseInt(O.length, 10);
        var start = arguments[1];
        var relativeStart = parseInt(start, 10) || 0;
        var k = relativeStart < 0
            ? Math.max(len + relativeStart, 0)
            : Math.min(relativeStart, len);
        var end = arguments[2];
        var relativeEnd = end === undefined
            ? len
            : (parseInt(end) || 0);
        var final = relativeEnd < 0
            ? Math.max(len + relativeEnd, 0)
            : Math.min(relativeEnd, len);
        for (; k < final; k++) {
            O[k] = value;
        }
        return O;
    };
}
var defaultConfig = {
    width: '100%',
    height: '100%'
};
var isNumber = function (input) { return Number(input) === Number(input); };
var addClass = 'classList' in document.documentElement
    ? function (element, className) {
        element.classList.add(className);
    }
    : function (element, className) {
        var oldClass = element.getAttribute('class') || '';
        element.setAttribute('class', oldClass + " " + className);
    };
var HyperList = (function () {
    function HyperList(element, userProvidedConfig) {
        var _this = this;
        this._config = {};
        this._lastRepaint = null;
        this._maxElementHeight = HyperList.getMaxBrowserHeight();
        this.refresh(element, userProvidedConfig);
        var config = this._config;
        var render = function () {
            var scrollTop = _this._getScrollPosition();
            var lastRepaint = _this._lastRepaint;
            _this._renderAnimationFrame = window.requestAnimationFrame(render);
            if (scrollTop === lastRepaint) {
                return;
            }
            var diff = lastRepaint ? scrollTop - lastRepaint : 0;
            if (!lastRepaint || diff < 0 || diff > _this._averageHeight) {
                var rendered = _this._renderChunk();
                _this._lastRepaint = scrollTop;
                if (rendered !== false && typeof config.afterRender === 'function') {
                    config.afterRender();
                }
            }
        };
        render();
    }
    HyperList.create = function (element, userProvidedConfig) {
        return new HyperList(element, userProvidedConfig);
    };
    HyperList.mergeStyle = function (element, style) {
        for (var i in style) {
            if (element.style[i] !== style[i]) {
                element.style[i] = style[i];
            }
        }
    };
    HyperList.getMaxBrowserHeight = function () {
        var wrapper = document.createElement('div');
        var fixture = document.createElement('div');
        HyperList.mergeStyle(wrapper, { position: 'absolute', height: '1px', opacity: 0 });
        HyperList.mergeStyle(fixture, { height: '1e7px' });
        wrapper.appendChild(fixture);
        document.body.appendChild(wrapper);
        var maxElementHeight = fixture.offsetHeight;
        document.body.removeChild(wrapper);
        return maxElementHeight;
    };
    HyperList.prototype.destroy = function () {
        window.cancelAnimationFrame(this._renderAnimationFrame);
    };
    HyperList.prototype.refresh = function (element, userProvidedConfig) {
        Object.assign(this._config, defaultConfig, userProvidedConfig);
        if (!element || element.nodeType !== 1) {
            throw new Error('HyperList requires a valid DOM Node container');
        }
        this._element = element;
        var config = this._config;
        var scroller = this._scroller || config.scroller ||
            document.createElement(config.scrollerTagName || 'tr');
        if (typeof config.useFragment !== 'boolean') {
            this._config.useFragment = true;
        }
        if (!config.generate) {
            throw new Error('Missing required `generate` function');
        }
        if (!isNumber(config.total)) {
            throw new Error('Invalid required `total` value, expected number');
        }
        if (!Array.isArray(config.itemHeight) && !isNumber(config.itemHeight)) {
            throw new Error("\n        Invalid required `itemHeight` value, expected number or array\n      ".trim());
        }
        else if (isNumber(config.itemHeight)) {
            this._itemHeights = Array(config.total).fill(config.itemHeight);
        }
        else {
            this._itemHeights = config.itemHeight;
        }
        Object.keys(defaultConfig).filter(function (prop) { return prop in config; }).forEach(function (prop) {
            var value = config[prop];
            var isValueNumber = isNumber(value);
            if (value && typeof value !== 'string' && typeof value !== 'number') {
                var msg = "Invalid optional `" + prop + "`, expected string or number";
                throw new Error(msg);
            }
            else if (isValueNumber) {
                config[prop] = value + "px";
            }
        });
        var isHoriz = Boolean(config.horizontal);
        var value = config[isHoriz ? 'width' : 'height'];
        if (value) {
            var isValueNumber = isNumber(value);
            var isValuePercent = isValueNumber ? false : value.slice(-1) === '%';
            var numberValue = isValueNumber ? value : parseInt(value.replace(/px|%/, ''), 10);
            var innerSize = window[isHoriz ? 'innerWidth' : 'innerHeight'];
            if (isValuePercent) {
                this._containerSize = (innerSize * numberValue) / 100;
            }
            else {
                this._containerSize = isNumber(value) ? value : numberValue;
            }
        }
        var scrollContainer = config.scrollContainer;
        var scrollerHeight = config.itemHeight * config.total;
        var maxElementHeight = this._maxElementHeight;
        if (scrollerHeight > maxElementHeight) {
            console.warn([
                'HyperList: The maximum element height', maxElementHeight + 'px has',
                'been exceeded; please reduce your item height.'
            ].join(' '));
        }
        var elementStyle = {
            width: "" + config.width,
            height: scrollContainer ? scrollerHeight + "px" : "" + config.height,
            overflow: scrollContainer ? 'none' : 'auto',
            position: 'relative'
        };
        HyperList.mergeStyle(element, elementStyle);
        if (scrollContainer) {
            HyperList.mergeStyle(config.scrollContainer, { overflow: 'auto' });
        }
        var scrollerStyle = (_a = {
                opacity: '0',
                position: 'absolute'
            },
            _a[isHoriz ? 'height' : 'width'] = '1px',
            _a[isHoriz ? 'width' : 'height'] = scrollerHeight + "px",
            _a);
        HyperList.mergeStyle(scroller, scrollerStyle);
        if (!this._scroller) {
            element.appendChild(scroller);
        }
        this._scroller = scroller;
        this._scrollHeight = this._computeScrollHeight();
        this._itemPositions = this._itemPositions || Array(config.total).fill(0);
        this._computePositions(0);
        this._renderChunk(this._lastRepaint !== null);
        if (typeof config.afterRender === 'function') {
            config.afterRender();
        }
        var _a;
    };
    HyperList.prototype._getRow = function (i) {
        var config = this._config;
        var item = config.generate(i);
        var height = item.height;
        if (height !== undefined && isNumber(height)) {
            item = item.element;
            if (height !== this._itemHeights[i]) {
                this._itemHeights[i] = height;
                this._computePositions(i);
                this._scrollHeight = this._computeScrollHeight(i);
            }
        }
        else {
            height = this._itemHeights[i];
        }
        if (!item || item.nodeType !== 1) {
            throw new Error("Generator did not return a DOM Node for index: " + i);
        }
        addClass(item, config.rowClassName || 'vrow');
        var top = this._itemPositions[i];
        HyperList.mergeStyle(item, (_a = {
                position: 'absolute'
            },
            _a[config.horizontal ? 'left' : 'top'] = top + "px",
            _a));
        return item;
        var _a;
    };
    HyperList.prototype._getScrollPosition = function () {
        var config = this._config;
        if (typeof config.overrideScrollPosition === 'function') {
            return config.overrideScrollPosition();
        }
        return this._element[config.horizontal ? 'scrollLeft' : 'scrollTop'];
    };
    HyperList.prototype._renderChunk = function (force) {
        var config = this._config;
        var element = this._element;
        var scrollTop = this._getScrollPosition();
        var total = config.total;
        var from = config.reverse ? this._getReverseFrom(scrollTop) : this._getFrom(scrollTop) - 1;
        if (from < 0 || from - this._screenItemsLen < 0) {
            from = 0;
        }
        if (!force && this._lastFrom === from) {
            return false;
        }
        this._lastFrom = from;
        var to = from + this._cachedItemsLen;
        if (to > total || to + this._cachedItemsLen > total) {
            to = total;
        }
        var fragment = config.useFragment ? document.createDocumentFragment() : [];
        var scroller = this._scroller;
        fragment[config.useFragment ? 'appendChild' : 'push'](scroller);
        for (var i = from; i < to; i++) {
            var row = this._getRow(i);
            fragment[config.useFragment ? 'appendChild' : 'push'](row);
        }
        if (config.applyPatch) {
            return config.applyPatch(element, fragment);
        }
        element.innerHTML = '';
        element.appendChild(fragment);
    };
    HyperList.prototype._computePositions = function (from) {
        if (from === void 0) { from = 1; }
        var config = this._config;
        var total = config.total;
        var reverse = config.reverse;
        if (from < 1 && !reverse) {
            from = 1;
        }
        for (var i = from; i < total; i++) {
            if (reverse) {
                if (i === 0) {
                    this._itemPositions[0] = this._scrollHeight - this._itemHeights[0];
                }
                else {
                    this._itemPositions[i] = this._itemPositions[i - 1] - this._itemHeights[i];
                }
            }
            else {
                this._itemPositions[i] = this._itemHeights[i - 1] + this._itemPositions[i - 1];
            }
        }
    };
    HyperList.prototype._computeScrollHeight = function (someArgumentThatIsNeverUsed) {
        var _this = this;
        var config = this._config;
        var isHoriz = Boolean(config.horizontal);
        var total = config.total;
        var scrollHeight = this._itemHeights.reduce(function (a, b) { return a + b; }, 0);
        HyperList.mergeStyle(this._scroller, (_a = {
                opacity: 0,
                position: 'absolute'
            },
            _a[isHoriz ? 'height' : 'width'] = '1px',
            _a[isHoriz ? 'width' : 'height'] = scrollHeight + "px",
            _a));
        var sortedItemHeights = this._itemHeights.slice(0).sort(function (a, b) { return a - b; });
        var middle = Math.floor(total / 2);
        var averageHeight = total % 2 === 0 ? (sortedItemHeights[middle] + sortedItemHeights[middle - 1]) / 2 : sortedItemHeights[middle];
        var clientProp = isHoriz ? 'clientWidth' : 'clientHeight';
        var element = config.scrollContainer ? config.scrollContainer : this._element;
        var containerHeight = element[clientProp] ? element[clientProp] : this._containerSize;
        this._screenItemsLen = Math.ceil(containerHeight / averageHeight);
        this._containerSize = containerHeight;
        this._cachedItemsLen = Math.max(this._cachedItemsLen || 0, this._screenItemsLen * 3);
        this._averageHeight = averageHeight;
        if (config.reverse) {
            window.requestAnimationFrame(function () {
                if (isHoriz) {
                    _this._element.scrollLeft = scrollHeight;
                }
                else {
                    _this._element.scrollTop = scrollHeight;
                }
            });
        }
        return scrollHeight;
        var _a;
    };
    HyperList.prototype._getFrom = function (scrollTop) {
        var i = 0;
        while (this._itemPositions[i] < scrollTop) {
            i++;
        }
        return i;
    };
    HyperList.prototype._getReverseFrom = function (scrollTop) {
        var i = this._config.total - 1;
        while (i > 0 && this._itemPositions[i] < scrollTop + this._containerSize) {
            i--;
        }
        return i;
    };
    return HyperList;
}());
function basicExample() {
    var container = document.createElement('div');
    var list = HyperList.create(container, {
        itemHeight: 30,
        total: 10000,
        generate: function (index) {
            var el = document.createElement('div');
            el.innerHTML = "ITEM " + (index + 1);
            return el;
        },
    });
    document.body.appendChild(container);
}
basicExample();
