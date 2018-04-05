/// attach an event handler, now or in the future, 
/// for all elements which match childselector,
/// within the child tree of the element maching parentSelector.
function subscribeEvent(parentSelector, eventName, childSelector, eventCallback)
{
    if (parentSelector == null)
        throw new ReferenceError("Parameter parentSelector is NULL");
    if (childSelector == null)
        throw new ReferenceError("Parameter childSelector is NULL");
    // nodeToObserve: the node that will be observed for mutations
    let nodeToObserve = parentSelector;
    if (typeof (parentSelector) === 'string')
        nodeToObserve = document.querySelector(parentSelector);

    let eligibleChildren = nodeToObserve.querySelectorAll(childSelector);
    for (let i = 0; i < eligibleChildren.length; ++i)
    {
        eligibleChildren[i].addEventListener(eventName, eventCallback, false);
    } // Next i 

    // https://stackoverflow.com/questions/2712136/how-do-i-make-this-loop-all-children-recursively
    function allDescendants(node)
    {
        if (node == null)
            return;

        for (let i = 0; i < node.childNodes.length; i++)
        {
            let child = node.childNodes[i];
            allDescendants(child);
        } // Next i 

        // IE 11 Polyfill 
        if (!Element.prototype.matches)
            Element.prototype.matches = Element.prototype.msMatchesSelector;

        if (node.matches)
        {
            if (node.matches(childSelector))
            {
                // console.log("match");
                node.addEventListener(eventName, eventCallback, false);
            } // End if ((<Element>node).matches(childSelector))
            // else console.log("no match");

        } // End if ((<Element>node).matches) 

        // else console.log("no matchfunction");
    } // End Function allDescendants 


    // Callback function to execute when mutations are observed
    let callback = function (mutationsList, observer)
    {
        for (let i = 0; i < mutationsList.length; i++)
        {
            let mutation = mutationsList[i];
            // console.log("mutation.type", mutation.type);
            // console.log("mutation", mutation);
            if (mutation.type == 'childList')
            {
                for (let j = 0; j < mutation.addedNodes.length; ++j)
                {
                    let thisNode = mutation.addedNodes[j];
                    allDescendants(thisNode);
                } // Next j 

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
    let box = this;
    if (box.checked)
    {
        let name = box.getAttribute("name");
        let pos = name.lastIndexOf("_");
        if (pos !== -1)
            name = name.substr(0, pos);
        let group = 'input[type="checkbox"][name^="' + name + '"]';
        // console.log(group);

        let eles: NodeListOf<HTMLInputElement> = <NodeListOf<HTMLInputElement>>document.querySelectorAll(group);
        // console.log(eles);
        for (let j = 0; j < eles.length; ++j)
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
    let elements = document.querySelectorAll('input[type="checkbox"]');
    for (let i = 0; i < elements.length; ++i)
    {
        // console.log(elements[i]);
        elements[i].addEventListener("click", radioCheckbox_onClick, false);
    } // Next i 
} // End Function radioCheckbox 


function onDomReady()
{
    console.log("dom ready");
    subscribeEvent(document, "click", 'input[type="checkbox"]', radioCheckbox_onClick);
    // radioCheckbox();
}
