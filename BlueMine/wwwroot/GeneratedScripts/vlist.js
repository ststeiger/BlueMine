'use strict';
var aaa = {
    w: 300,
    h: 300,
    itemHeight: 31,
    totalRows: 10000,
    generatorFn: function (row) {
        var el = document.createElement("div");
        el.innerHTML = "ITEM " + row;
        el.style.borderBottom = "1px solid red";
        el.style.position = "absolute";
        return el;
    }
};
var VirtualList = (function () {
    function VirtualList(config) {
        var width = (config && config.w + 'px') || '100%';
        var height = (config && config.h + 'px') || '100%';
        var itemHeight = this.itemHeight = config.itemHeight;
        this.items = config.items;
        this.generatorFn = config.generatorFn;
        this.totalRows = config.totalRows || (config.items && config.items.length);
        var scroller = VirtualList.createScroller(itemHeight * this.totalRows);
        this.container = VirtualList.createContainer(width, height);
        this.container.appendChild(scroller);
        var screenItemsLen = Math.ceil(config.h / itemHeight);
        this.cachedItemsLen = screenItemsLen * 3;
        this._renderChunk(this.container, 0);
        var self = this;
        var lastRepaintY;
        var maxBuffer = screenItemsLen * itemHeight;
        var lastScrolled = 0;
        this.rmNodeInterval = setInterval(function () {
            if (Date.now() - lastScrolled > 100) {
                var badNodes = document.querySelectorAll('[data-rm="1"]');
                for (var i = 0, l = badNodes.length; i < l; i++) {
                    self.container.removeChild(badNodes[i]);
                }
            }
        }, 300);
        function onScroll(e) {
            var scrollTop = e.target.scrollTop;
            if (!lastRepaintY || Math.abs(scrollTop - lastRepaintY) > maxBuffer) {
                var first = parseInt((scrollTop / itemHeight)) - screenItemsLen;
                self._renderChunk(self.container, first < 0 ? 0 : first);
                lastRepaintY = scrollTop;
            }
            lastScrolled = Date.now();
            e.preventDefault && e.preventDefault();
        }
        this.container.addEventListener('scroll', onScroll);
    }
    VirtualList.prototype.createRow = function (i) {
        var item;
        if (this.generatorFn)
            item = this.generatorFn(i);
        else if (this.items) {
            if (typeof this.items[i] === 'string') {
                var itemText = document.createTextNode(this.items[i]);
                item = document.createElement('div');
                item.style.height = this.itemHeight + 'px';
                item.appendChild(itemText);
            }
            else {
                item = this.items[i];
            }
        }
        item.classList.add('vrow');
        item.style.position = 'absolute';
        item.style.top = (i * this.itemHeight) + 'px';
        return item;
    };
    VirtualList.prototype._renderChunk = function (node, from) {
        var finalItem = from + this.cachedItemsLen;
        if (finalItem > this.totalRows)
            finalItem = this.totalRows;
        var fragment = document.createDocumentFragment();
        for (var i = from; i < finalItem; i++) {
            fragment.appendChild(this.createRow(i));
        }
        for (var j = 1, l = node.childNodes.length; j < l; j++) {
            node.childNodes[j].style.display = 'none';
            node.childNodes[j].setAttribute('data-rm', '1');
        }
        node.appendChild(fragment);
    };
    VirtualList.createContainer = function (w, h) {
        var c = document.createElement('div');
        c.style.width = w;
        c.style.height = h;
        c.style.overflow = 'auto';
        c.style.position = 'relative';
        c.style.padding = "0";
        c.style.border = '1px solid black';
        return c;
    };
    VirtualList.createScroller = function (h) {
        var scroller = document.createElement('div');
        scroller.style.opacity = "0";
        scroller.style.position = 'absolute';
        scroller.style.top = "0";
        scroller.style.left = "0";
        scroller.style.width = '1px';
        scroller.style.height = h + 'px';
        return scroller;
    };
    return VirtualList;
}());
export { VirtualList };
