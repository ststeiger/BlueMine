/**
 * The MIT License (MIT)
 *
 * Copyright (C) 2013 Sergi Mansilla
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the 'Software'), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

'use strict';




interface IConfig 
{
    w: number; // 300
    h: number; // 300
    itemHeight: number; // 31
    totalRows: number; // 10000
    items?: any[];
    generatorFn: (row: any) => Element;
}



let aaa: IConfig = 
    {
        w: 300,
        h: 300,
        itemHeight: 31,
        totalRows: 10000,
        generatorFn: function (row)
        {
            var el = document.createElement("div");
            el.innerHTML = "ITEM " + row;
            el.style.borderBottom = "1px solid red";
            el.style.position = "absolute"
            return el;
        }
    };




// https://github.com/sergi/virtual-list
export class VirtualList
{
    
    protected itemHeight;
    protected items;
    protected generatorFn;
    protected container;
    protected totalRows;
    protected cachedItemsLen;
    
    protected rmNodeInterval;// setInterval
    

    
    /**
     * Creates a virtually-rendered scrollable list.
     * @param {object} config
     * @constructor
     */
    constructor(config:IConfig)
    {
        let width = (config && config.w + 'px') || '100%';
        let height = (config && config.h + 'px') || '100%';
        let itemHeight = this.itemHeight = config.itemHeight;
        
        this.items = config.items;
        this.generatorFn = config.generatorFn;
        this.totalRows = config.totalRows || (config.items && config.items.length);
        
        let scroller = VirtualList.createScroller(itemHeight * this.totalRows);
        this.container = VirtualList.createContainer(width, height);
        this.container.appendChild(scroller);

        let screenItemsLen = Math.ceil(config.h / itemHeight);
        // Cache 4 times the number of items that fit in the container viewport
        this.cachedItemsLen = screenItemsLen * 3;
        this._renderChunk(this.container, 0);

        let self = this;
        let lastRepaintY;
        let maxBuffer = screenItemsLen * itemHeight;
        let lastScrolled = 0;
        
        // As soon as scrolling has stopped, this interval asynchronouslyremoves all
        // the nodes that are not used anymore
        this.rmNodeInterval = setInterval(
            function ()
            {
                if (Date.now() - lastScrolled > 100)
                {
                    let badNodes = document.querySelectorAll('[data-rm="1"]');
                    for (let i = 0, l = badNodes.length; i < l; i++)
                    {
                        self.container.removeChild(badNodes[i]);
                    }
                }
            }, 300
        );
        
        
        function onScroll(e)
        {
            let scrollTop = e.target.scrollTop; // Triggers reflow
            if (!lastRepaintY || Math.abs(scrollTop - lastRepaintY) > maxBuffer)
            {
                let first:number = parseInt(<string><any>(scrollTop / itemHeight)) - screenItemsLen;
                self._renderChunk(self.container, first < 0 ? 0 : first);
                lastRepaintY = scrollTop;
            }
            
            lastScrolled = Date.now();
            e.preventDefault && e.preventDefault();
        }
        
        this.container.addEventListener('scroll', onScroll);
    }
    
    
    public createRow(i) 
    {
        let item;
        
        if (this.generatorFn)
            item = this.generatorFn(i);
        else if (this.items)
        {
            if (typeof this.items[i] === 'string')
            {
                let itemText = document.createTextNode(this.items[i]);
                item = document.createElement('div');
                item.style.height = this.itemHeight + 'px';
                item.appendChild(itemText);
            } 
            else
            {
                item = this.items[i];
            }
        }
        
        item.classList.add('vrow');
        item.style.position = 'absolute';
        item.style.top = (i * this.itemHeight) + 'px';
        return item;
    }
    
    
    /**
     * Renders a particular, consecutive chunk of the total rows in the list. To
     * keep acceleration while scrolling, we mark the nodes that are candidate for
     * deletion instead of deleting them right away, which would suddenly stop the
     * acceleration. We delete them once scrolling has finished.
     *
     * @param {Node} node Parent node where we want to append the children chunk.
     * @param {Number} from Starting position, i.e. first children index.
     * @return {void}
     */
    private _renderChunk(node, from)
    {
        let finalItem = from + this.cachedItemsLen;
        if (finalItem > this.totalRows)
            finalItem = this.totalRows;
        
        // Append all the new rows in a document fragment that we will later append to
        // the parent node
        let fragment = document.createDocumentFragment();
        for (let i = from; i < finalItem; i++)
        {
            fragment.appendChild(this.createRow(i));
        }
        
        // Hide and mark obsolete nodes for deletion.
        for (let j = 1, l = node.childNodes.length; j < l; j++)
        {
            node.childNodes[j].style.display = 'none';
            node.childNodes[j].setAttribute('data-rm', '1');
        }
        
        node.appendChild(fragment);
    }
    
    
    public static createContainer(w, h)
    {
        let c:HTMLElement = document.createElement('div');
        c.style.width = w;
        c.style.height = h;
        c.style.overflow = 'auto';
        c.style.position = 'relative';
        c.style.padding = "0";
        c.style.border = '1px solid black';
        return c;
    }
    
    
    public static createScroller(h)
    {
        let scroller:HTMLElement = document.createElement('div');
        scroller.style.opacity = "0";
        scroller.style.position = 'absolute';
        scroller.style.top = "0";
        scroller.style.left = "0";
        scroller.style.width = '1px';
        scroller.style.height = h + 'px';
        return scroller;
    }
    
    
}
