/// attach an event handler, now or in the future, 
/// for all elements which match childselector,
/// within the child tree of the element maching parentSelector.
function subscribeEvent(parentSelector, eventName, childSelector, eventCallback) {
    if (parentSelector == null)
        throw new ReferenceError("Parameter parentSelector is NULL");
    if (childSelector == null)
        throw new ReferenceError("Parameter childSelector is NULL");
    // nodeToObserve: the node that will be observed for mutations
    var nodeToObserve = parentSelector;
    if (typeof (parentSelector) === 'string')
        nodeToObserve = document.querySelector(parentSelector);
    var eligibleChildren = nodeToObserve.querySelectorAll(childSelector);
    for (var i = 0; i < eligibleChildren.length; ++i) {
        eligibleChildren[i].addEventListener(eventName, eventCallback, false);
    } // Next i 
    // https://stackoverflow.com/questions/2712136/how-do-i-make-this-loop-all-children-recursively
    function allDescendants(node) {
        if (node == null)
            return;
        for (var i = 0; i < node.childNodes.length; i++) {
            var child = node.childNodes[i];
            allDescendants(child);
        } // Next i 
        // IE 11 Polyfill 
        if (!Element.prototype.matches)
            Element.prototype.matches = Element.prototype.msMatchesSelector;
        if (node.matches) {
            if (node.matches(childSelector)) {
                // console.log("match");
                node.addEventListener(eventName, eventCallback, false);
            } // End if ((<Element>node).matches(childSelector))
            // else console.log("no match");
        } // End if ((<Element>node).matches) 
        // else console.log("no matchfunction");
    } // End Function allDescendants 
    // Callback function to execute when mutations are observed
    var callback = function (mutationsList, observer) {
        for (var _i = 0, mutationsList_1 = mutationsList; _i < mutationsList_1.length; _i++) {
            var mutation = mutationsList_1[_i];
            // console.log("mutation.type", mutation.type);
            // console.log("mutation", mutation);
            if (mutation.type == 'childList') {
                for (var i = 0; i < mutation.addedNodes.length; ++i) {
                    var thisNode = mutation.addedNodes[i];
                    allDescendants(thisNode);
                } // Next i 
            } // End if (mutation.type == 'childList') 
            // else if (mutation.type == 'attributes') { console.log('The ' + mutation.attributeName + ' attribute was modified.');
        } // Next mutation 
    }; // End Function callback 
    // Options for the observer (which mutations to observe)
    var config = { attributes: false, childList: true, subtree: true };
    // Create an observer instance linked to the callback function
    var observer = new MutationObserver(callback);
    // Start observing the target node for configured mutations
    observer.observe(nodeToObserve, config);
} // End Function subscribeEvent 
function radioCheckbox_onClick() {
    // console.log("click", this);
    var box = this;
    if (box.checked) {
        var name_1 = box.getAttribute("name");
        var pos = name_1.lastIndexOf("_");
        if (pos !== -1)
            name_1 = name_1.substr(0, pos);
        var group = 'input[type="checkbox"][name^="' + name_1 + '"]';
        // console.log(group);
        var eles = document.querySelectorAll(group);
        // console.log(eles);
        for (var j = 0; j < eles.length; ++j) {
            eles[j].checked = false;
        }
        box.checked = true;
    }
    else
        box.checked = false;
}
// https://stackoverflow.com/questions/9709209/html-select-only-one-checkbox-in-a-group
function radioCheckbox() {
    // on instead of document...
    var elements = document.querySelectorAll('input[type="checkbox"]');
    for (var i = 0; i < elements.length; ++i) {
        // console.log(elements[i]);
        elements[i].addEventListener("click", radioCheckbox_onClick, false);
    } // Next i 
} // End Function radioCheckbox 
function onDomReady() {
    console.log("dom ready");
    subscribeEvent(document, "click", 'input[type="checkbox"]', radioCheckbox_onClick);
    // radioCheckbox();
}
