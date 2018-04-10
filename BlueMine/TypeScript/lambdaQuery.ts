
export class lambdaQuery
{
    
    public element: HTMLElement;
    
    constructor(arg)
    {
        // html: create element
        if (typeof (arg) === 'string')
        {
            this.element = document.querySelector(arg);
        }
        
        this.element = arg;
    }
    
    
    // .is("input,textarea");
    public is(selector:string) : boolean
    {
        return this.element.matches(selector);
    }
    
    
    public appendTo(el)
    {}
    
    public clone()
    {}
    
    
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
    public extend(deep: true, target: any, object1: any, ...objects: any[]): any
    {
        return this;
    }
    
    
    public css(propertyName:string, value:string, priority?:string)
    {
        this.element.style.setProperty(propertyName, value, priority);
    }

    // .delegate(".slick-cell", "mouseenter", handleMouseEnter)
    //{return this;}
    
    /*
    // Attach a handler to an event for the elements.
    bind<TData>(eventType: string,
                eventData: TData,
                handler: ): this;
        { return this;}
        */
        
    //mousewheel:boolean
    // $viewport.on("mousewheel", handleMouseWheel);
    public hide()
    {
    }

    public add(html:string)
    {
    }
    
    public addClass(className:string)
    {
    }
    
}