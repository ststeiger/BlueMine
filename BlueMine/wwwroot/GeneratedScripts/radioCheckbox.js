export function subscribeEvent(parentSelector, eventName, childSelector, eventCallback) {
    if (parentSelector == null)
        throw new ReferenceError("Parameter parentSelector is NULL");
    if (childSelector == null)
        throw new ReferenceError("Parameter childSelector is NULL");
    var nodeToObserve = parentSelector;
    if (typeof (parentSelector) === 'string')
        nodeToObserve = document.querySelector(parentSelector);
    var eligibleChildren = nodeToObserve.querySelectorAll(childSelector);
    for (var i = 0; i < eligibleChildren.length; ++i) {
        eligibleChildren[i].addEventListener(eventName, eventCallback, false);
    }
    function allDescendants(node) {
        if (node == null)
            return;
        for (var i = 0; i < node.childNodes.length; i++) {
            var child = node.childNodes[i];
            allDescendants(child);
        }
        if (!Element.prototype.matches)
            Element.prototype.matches = Element.prototype.msMatchesSelector;
        if (node.matches) {
            if (node.matches(childSelector)) {
                node.addEventListener(eventName, eventCallback, false);
            }
        }
    }
    var callback = function (mutationsList, observer) {
        for (var _i = 0, mutationsList_1 = mutationsList; _i < mutationsList_1.length; _i++) {
            var mutation = mutationsList_1[_i];
            if (mutation.type == 'childList') {
                for (var i = 0; i < mutation.addedNodes.length; ++i) {
                    var thisNode = mutation.addedNodes[i];
                    allDescendants(thisNode);
                }
            }
        }
    };
    var config = { attributes: false, childList: true, subtree: true };
    var observer = new MutationObserver(callback);
    observer.observe(nodeToObserve, config);
}
function radioCheckbox_onClick() {
    var box = this;
    if (box.checked) {
        var name_1 = box.getAttribute("name");
        var pos = name_1.lastIndexOf("_");
        if (pos !== -1)
            name_1 = name_1.substr(0, pos);
        var group = 'input[type="checkbox"][name^="' + name_1 + '"]';
        var eles = document.querySelectorAll(group);
        for (var j = 0; j < eles.length; ++j) {
            eles[j].checked = false;
        }
        box.checked = true;
    }
    else
        box.checked = false;
}
function radioCheckbox() {
    var elements = document.querySelectorAll('input[type="checkbox"]');
    for (var i = 0; i < elements.length; ++i) {
        elements[i].addEventListener("click", radioCheckbox_onClick, false);
    }
}
function onDomReady() {
    var a;
    var e;
    var h;
    var n;
    var z1 = a;
    var z2 = e;
    var z3 = h;
    var z5 = document;
    console.log("dom ready");
    subscribeEvent(document, "click", 'input[type="checkbox"]', radioCheckbox_onClick);
}
