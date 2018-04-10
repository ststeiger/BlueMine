
// export
 class lambdaQuery
{

    public element: HTMLElement;

    constructor(arg)
    {
        // html: create element
        if (typeof (arg) === 'string')
        {
            this.element = <HTMLElement>document.querySelector(arg);
        }

        this.element = arg;
    }


    // .is("input,textarea");
    public is(selector: string): boolean
    {
        return this.element.matches(selector);
    }

    
    public appendTo(targetSelector: string | HTMLElement)
    {
        if(typeof targetSelector === 'string' || targetSelector instanceof String)
            document.querySelector(<string><any>targetSelector).appendChild(this.element);
        else
            (<HTMLElement>targetSelector).appendChild(this.element);
    }


    public clone()
    {
        // clone events etc.
        return this.element.cloneNode(true);
    }


    public empty(e?)
    {
        let elem = e || this.element;
        for (elem = elem.firstChild; elem; elem = elem.nextSibling)
        {
            if (elem.nodeType < 6)
            {
                return false;
            }
        }
        return true;
    }

    //extend(true, [], treeColumns);
    // https://gomakethings.com/vanilla-javascript-version-of-jquery-extend/
    public extend(deep: boolean, target: any, object1: any, ...objects: any[]): any
    {
        // Variables
        let extended = {};
        // let deep = false;
        let i = 0;
        let length = arguments.length;

        // Check if a deep merge
        if (Object.prototype.toString.call(arguments[0]) === '[object Boolean]')
        {
            deep = arguments[0];
            i++;
        }

        // Merge the object into the extended object
        let merge = function (obj)
        {
            for (let prop in obj)
            {
                if (Object.prototype.hasOwnProperty.call(obj, prop))
                {
                    // If deep merge and property is an object, merge properties
                    if (deep && Object.prototype.toString.call(obj[prop]) === '[object Object]')
                    {
                        extended[prop] = this.extend(true, extended[prop], obj[prop]);
                    } else
                    {
                        extended[prop] = obj[prop];
                    }
                }
            }
        }

        // Loop through each object and conduct a merge
        for (; i < length; i++)
        {
            let obj = arguments[i];
            merge(obj);
        }

        return extended;
    }



    // parseFloat($.css($container[0], "paddingTop", true)) 
    // css(elem: Element, unknown: any, x?:boolean): any;
    public css(propertyName: string, value: string, priority?: string)
    {
        this.element.style.setProperty(propertyName, value, priority);
        //return this.element.style[propertyName]
    }


    public delegate(selector: string, eventType: string, handler: any)
    {
        // ON....
    }

    // .delegate(".slick-cell", "mouseenter", handleMouseEnter)
    //{return this;}


    private addEvent(el, eventType, handler)
    {
        if (el.addEventListener)
        {
            // DOM Level 2 browsers
            el.addEventListener(eventType, handler, false);
        }
        else if (el.attachEvent)
        {
            // IE <= 8
            el.attachEvent('on' + eventType, handler);
        }
        else
        {
            // ancient browsers
            el['on' + eventType] = handler;
        }
    }


    // $viewport.bind('selectstart.ui', function (event)
    // bind( "click", function() {
    // bind( "mouseenter mouseleave", function()
    // Attach a handler to an event for the elements.
    public bind<TData>(eventType: string,
        //eventData: TData,
        handler: any): this
    {
        let events: string[] = eventType.split(' ');

        for (let i = 0; i < events.length; ++i)
        {
            this.addEvent(this.element, events[i], handler);
        }

        return this;
    }
        

    //mousewheel:boolean
    // $viewport.on("mousewheel", handleMouseWheel);


    //  $focusSink.add($focusSink2)
    public add(el)
    {
        this.element.appendChild(el);
    }

    //  Hide the matched elements.
    public hide()
    {
        if (this.element.style.display !== "none")
        {
            this.element.style.display = "none";
        }
    }


    public addClass(className: string)
    {
        if (!this.element.classList.contains(className))
            this.element.classList.add(className);
    }

    // public grep(selectedRowIds: string, callback) //function (id)
    public grep<T>(array: T[],
        fn: (elementOfArray: T, indexInArray: number) => boolean,
        invert?: boolean
    ): T[]
    {
        // ArrayLike
        // return array.filter(fn);
        // Faster: 
        let results:any[] = [], e;
        invert = !!invert;
        for (let i = 0, length = array.length; i < length; i++)
            e = !!fn(array[i], i), invert !== e && results.push(array[i]);

        return results;
    }

    
    // $.fn.mousewheel

    // https://stackoverflow.com/questions/19035250/check-if-jquery-version-is-greater-than-1-8-3
    // $.fn.jquery.split('.');
    // var vernums = $.fn.jquery.split('.');


    //Create a new jQuery.Event object without the "new" operator.
    //$.Event(interactEvent.originalEvent.type, interactEvent.originalEvent);
    public Event(src, props)
    //public Event(type, props)
    {
        
        // https://caniuse.com/#feat=customevent
        // let ev = new CustomEvent('build', { detail: elem.dataset.time });
        //let ev = new CustomEvent(type, { detail: props });

        let ev = null;
        return ev;
        

        /*
        // Allow instantiation without the 'new' keyword
        if (!(this instanceof jQuery.Event))
        {
            return new jQuery.Event(src, props);
        }

        // Event object
        if (src && src.type)
        {
            this.originalEvent = src;
            this.type = src.type;

            // Events bubbling up the document may have been marked as prevented
            // by a handler lower down the tree; reflect the correct value.
            this.isDefaultPrevented = src.defaultPrevented ||
                src.defaultPrevented === undefined &&

                // Support: Android <=2.3 only
                src.returnValue === false ?
                returnTrue :
                returnFalse;

            // Create target properties
            // Support: Safari <=6 - 7 only
            // Target should not be a text node (#504, #13143)
            this.target = (src.target && src.target.nodeType === 3) ?
                src.target.parentNode :
                src.target;

            this.currentTarget = src.currentTarget;
            this.relatedTarget = src.relatedTarget;

            // Event type
        } else
        {
            this.type = src;
        }

        // Put explicitly provided properties onto the event object
        if (props)
        {
            jQuery.extend(this, props);
        }

        // Create a timestamp if incoming event doesn't have one
        this.timeStamp = src && src.timeStamp || Date.now();

        // Mark it as fixed
        // this[jQuery.expando] = true;
        */
    }



    //  Iterate over a jQuery object, executing a function for each matched element.
    // $.each(p, function (n, val)
    public each(obj, callback) 
    {
        let length, i: any = 0;

        if (Array.isArray(obj))
        {
            length = obj.length;
            for (; i < length; i++)
            {
                if (callback.call(obj[i], i, obj[i]) === false)
                {
                    break;
                }
            }
        }
        else
        {
            for (i in obj)
            {
                if (callback.call(obj[i], i, obj[i]) === false)
                {
                    break;
                }
            }
        }

        return obj;
    }

    public map<T>(array:any[], callback): any[]
    {
        let newArray:any[] = [];
        for (let i = 0; i < array.length; ++i)
        {
            newArray[i] = callback(array[i]);
        }

        return newArray
    }


}

function λ(selector: string)
{
    return new lambdaQuery(selector);
}
λ("body");
