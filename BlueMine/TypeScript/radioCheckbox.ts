
/// attach an event handler, now or in the future, 
/// for all elements which match childselector,
/// within the child tree of the element maching parentSelector.
export function subscribeEvent(parentSelector: string | Node & NodeSelector 
    , eventName: string
    , childSelector: string
    , eventCallback)
{
    if (parentSelector == null)
        throw new ReferenceError("Parameter parentSelector is NULL");

    if (childSelector == null)
        throw new ReferenceError("Parameter childSelector is NULL");

    // nodeToObserve: the node that will be observed for mutations
    let nodeToObserve: NodeSelector & Node = <NodeSelector & Node>parentSelector;
    if (typeof (parentSelector) === 'string')
        nodeToObserve = document.querySelector(<string>parentSelector);


    let eligibleChildren: NodeListOf<Element> = nodeToObserve.querySelectorAll(childSelector);

    for (let i = 0; i < eligibleChildren.length; ++i)
    {
        eligibleChildren[i].addEventListener(eventName, eventCallback, false);
    } // Next i 

    // https://stackoverflow.com/questions/2712136/how-do-i-make-this-loop-all-children-recursively
    function allDescendants(node: Node)
    {
        if (node == null)
            return;

        for (let i = 0; i < node.childNodes.length; i++)
        {
            let child = node.childNodes[i];
            allDescendants(child);
        } // Next i 

        // IE 11 Polyfill 
        if (!Element.prototype.matches) Element.prototype.matches = Element.prototype.msMatchesSelector;

        if ((<Element>node).matches)
        {
            if ((<Element>node).matches(childSelector))
            {
                // console.log("match");
                node.addEventListener(eventName, eventCallback, false);
            } // End if ((<Element>node).matches(childSelector))
            // else console.log("no match");

        } // End if ((<Element>node).matches) 
        // else console.log("no matchfunction");

    } // End Function allDescendants 


    // Callback function to execute when mutations are observed
    let callback: MutationCallback = function (mutationsList: MutationRecord[], observer: MutationObserver)
    {
        for (let mutation of mutationsList)
        {
            // console.log("mutation.type", mutation.type);
            // console.log("mutation", mutation);

            if (mutation.type == 'childList')
            {
                for (let i = 0; i < mutation.addedNodes.length; ++i)
                {
                    let thisNode: Node = mutation.addedNodes[i];
                    allDescendants(thisNode);
                } // Next i 

            } // End if (mutation.type == 'childList') 
            // else if (mutation.type == 'attributes') { console.log('The ' + mutation.attributeName + ' attribute was modified.');

        } // Next mutation 

    }; // End Function callback 

    // Options for the observer (which mutations to observe)
    let config = { attributes: false, childList: true, subtree: true };

    // Create an observer instance linked to the callback function
    let observer = new MutationObserver(callback);

    // Start observing the target node for configured mutations
    observer.observe(nodeToObserve, config);
} // End Function subscribeEvent 


function radioCheckbox_onClick()
{
    // console.log("click", this);
    let box: HTMLInputElement = this;
    if (box.checked)
    {
        let name: string = box.getAttribute("name");
        let pos: number = name.lastIndexOf("_");
        if (pos !== -1) name = name.substr(0, pos);

        let group: string = 'input[type="checkbox"][name^="' + name + '"]';
        // console.log(group);
        let eles: NodeListOf<HTMLInputElement> = <NodeListOf<HTMLInputElement>>document.querySelectorAll(group);
        // console.log(eles);
        for (let j: number = 0; j < eles.length; ++j)
        {
            eles[j].checked = false;
        }
        box.checked = true;
    }
    else
        box.checked = false;
}


// https://stackoverflow.com/questions/9709209/html-select-only-one-checkbox-in-a-group
function radioCheckbox()
{
    // on instead of document...
    let elements: NodeListOf<Element> = document.querySelectorAll('input[type="checkbox"]');

    for (let i: number = 0; i < elements.length; ++i)
    {
        // console.log(elements[i]);
        elements[i].addEventListener("click", radioCheckbox_onClick, false);
    } // Next i 

} // End Function radioCheckbox 


function onDomReady(): void
{
    var a: Document;
    var e: Element;
    var h: HTMLElement;
    var n: Node;
    var z1: Node & NodeSelector = a;
    var z2: Node & NodeSelector = e;
    var z3: Node & NodeSelector = h;
    // var z4: Node & NodeSelector = n; // Boom! Node is not NodeSelector 
    let z5: Node & NodeSelector = document;

    console.log("dom ready");
    subscribeEvent(document,
        "click",
        'input[type="checkbox"]',
        radioCheckbox_onClick
    );

    // radioCheckbox();
} // End Sub onDomReady 
