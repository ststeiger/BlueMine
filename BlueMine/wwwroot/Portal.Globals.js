;'use strict';
var Portal = (Portal === undefined) ? {} : Portal;

Portal.Global = {
	_windowTop: null, //REM: window.top des Portals

	_getWindowTop: function(){
		if(!this._windowTop){
			var tWindow = window;
			while(!tWindow.Settings) tWindow = tWindow.parent;

			this._windowTop = tWindow
		};

		return this._windowTop
	},

    Waiting: {
        Start: function(){
            window.top.Portal.Skin.Waiting()
        },

        Stop: function(){
            window.top.Portal.Skin.Waiting(true)
        }
    },

    //t:=frame, f:=event function, cn:=classname, r:=resetbox
    addActionBox: function(t, f, cn, r){
        (typeof window.top.Portal.Skin !== 'undefined') && window.top.Portal.Skin.addActionBox(t, f, cn, r)
    },

    //a:=classes to remove
    removeActionBoxes: function(a){
        (typeof window.top.Portal.Skin !== 'undefined') && window.top.Portal.Skin.removeActionBoxes(a)
    },

    rootLink: function(){
        return window.top.Settings.rootLink
    },

    Settings: function(){
        return window.top.Settings || {}
    },

	receiveMessage: function(event){
		if(event && event.data){
			//REM: Die automatische Object-Konvertierung gibt Probleme im IE8/9 :p
			//https://stackoverflow.com/questions/13830480/ie8-9-window-postmessage-not-working-but-why
			var tData = (typeof event.data === 'string') ? JSON.parse(event.data) : event.data;

			//REM: Führt das Vererben wird eine Handlung gefordert
			if(tData && tData.Action){
				//REM: Vererbt die Daten als String an die iframes weiter
				for(var tL=document.querySelectorAll('iframe'), i=0, j=tL.length; i<j; i++){
					try{
						(typeof tL[i].contentWindow.postMessage === 'function') && tL[i].contentWindow.postMessage(JSON.stringify(tData), '*')
					}
					catch(err){
						//CORS
					}
				}
			}
		}
	},

	spreadMessage: function(object){
		var tWindow = this._getWindowTop();

		if(tWindow.addEventListener){
			tWindow.top.removeEventListener('message', Portal.Global.receiveMessage.bind(tWindow), false);
			tWindow.top.addEventListener('message', Portal.Global.receiveMessage.bind(tWindow), false)
		}
		else if(tWindow.attachEvent){
			tWindow.top.detachEvent('onmessage', Portal.Global.receiveMessage.bind(tWindow));
			tWindow.top.attachEvent('onmessage', Portal.Global.receiveMessage.bind(tWindow))
		};

		tWindow.top.postMessage(JSON.stringify(object), '*')
	}
};

/************************************************************************************
* Polyfills (IE8-)
* In progress..
************************************************************************************/
if(typeof String.prototype.trim !== 'function'){
    String.prototype.trim = function(){
        return this.replace(/^\s+|\s+$/g, ''); 
    }
}

// From https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Object/keys
if(!Object.keys){
  Object.keys = (function () {
    'use strict';
    var hasOwnProperty = Object.prototype.hasOwnProperty,
        hasDontEnumBug = !({toString: null}).propertyIsEnumerable('toString'),
        dontEnums = [
          'toString',
          'toLocaleString',
          'valueOf',
          'hasOwnProperty',
          'isPrototypeOf',
          'propertyIsEnumerable',
          'constructor'
        ],
        dontEnumsLength = dontEnums.length;

    return function (obj) {
      if (typeof obj !== 'object' && (typeof obj !== 'function' || obj === null)) {
        throw new TypeError('Object.keys called on non-object');
      }

      var result = [], prop, i;

      for (prop in obj) {
        if (hasOwnProperty.call(obj, prop)) {
          result.push(prop);
        }
      }

      if (hasDontEnumBug) {
        for (i = 0; i < dontEnumsLength; i++) {
          if (hasOwnProperty.call(obj, dontEnums[i])) {
            result.push(dontEnums[i]);
          }
        }
      }
      return result;
    };
  }());
}

if (!window.getComputedStyle) {
    window.getComputedStyle = function(el, pseudo) {
        this.el = el;
        this.getPropertyValue = function(prop) {
            var re = /(\-([a-z]){1})/g;
            if (prop == 'float') prop = 'styleFloat';
            if (re.test(prop)) {
                prop = prop.replace(re, function () {
                    return arguments[2].toUpperCase();
                });
            }
            return el.currentStyle[prop] ? el.currentStyle[prop] : null;
        }
        return this;
    }
}

if (!Function.prototype.bind) {
  Function.prototype.bind = function(oThis) {
    if (typeof this !== 'function') {
      throw new TypeError('Function.prototype.bind - what is trying to be bound is not callable');
    }

    var aArgs   = Array.prototype.slice.call(arguments, 1),
        fToBind = this,
        fNOP    = function() {},
        fBound  = function() {
          return fToBind.apply(this instanceof fNOP
                 ? this
                 : oThis,
                 aArgs.concat(Array.prototype.slice.call(arguments)));
        };

    if (this.prototype) {
      fNOP.prototype = this.prototype; 
    }
    fBound.prototype = new fNOP();

    return fBound;
  };
}

/**
 * Shim for "fixing" IE's lack of support (IE < 9) for applying slice
 * on host objects like NamedNodeMap, NodeList, and HTMLCollection
 * (technically, since host objects have been implementation-dependent,
 * at least before ES6, IE hasn't needed to work this way).
 * Also works on strings, fixes IE < 9 to allow an explicit undefined
 * for the 2nd argument (as in Firefox), and prevents errors when
 * called on other DOM objects.
 */
(function () {
  'use strict';
  var _slice = Array.prototype.slice;

  try {
    // Can't be used with DOM elements in IE < 9
    _slice.call(document.documentElement);
  } catch (e) { // Fails in IE < 9
    Array.prototype.slice = function(begin, end) {
      end = (typeof end !== 'undefined') ? end : this.length;

      if (Object.prototype.toString.call(this) === '[object Array]'){
        return _slice.call(this, begin, end); 
      }

      var i, cloned = [],
        size, len = this.length;

      var start = begin || 0;
      start = (start >= 0) ? start : Math.max(0, len + start);

      // Handle negative value for "end"
      var upTo = (typeof end == 'number') ? Math.min(end, len) : len;
      if (end < 0) {
        upTo = len + end;
      }

      size = upTo - start;

      if (size > 0) {
        cloned = new Array(size);
        if (this.charAt) {
          for (i = 0; i < size; i++) {
            cloned[i] = this.charAt(start + i);
          }
        } else {
          for (i = 0; i < size; i++) {
            cloned[i] = this[start + i];
          }
        }
      }

      return cloned;
    };
  }
}());

if (!Array.prototype.filter) {
  Array.prototype.filter = function(fun/*, thisArg*/) {
    'use strict';

    if (this === void 0 || this === null) {
      throw new TypeError();
    }

    var t = Object(this);
    var len = t.length >>> 0;
    if (typeof fun !== 'function') {
      throw new TypeError();
    }

    var res = [];
    var thisArg = arguments.length >= 2 ? arguments[1] : void 0;
    for (var i = 0; i < len; i++) {
      if (i in t) {
        var val = t[i];
        if (fun.call(thisArg, val, i, t)) {
          res.push(val);
        }
      }
    }

    return res;
  };
}

// Production steps of ECMA-262, Edition 5, 15.4.4.14
// Reference: http://es5.github.io/#x15.4.4.14
if (!Array.prototype.indexOf) {
  Array.prototype.indexOf = function(searchElement, fromIndex) {
    var k;

    if (this == null) {
      throw new TypeError('"this" is null or not defined');
    }

    var o = Object(this);
    var len = o.length >>> 0;

    if (len === 0) {
      return -1;
    }

    var n = +fromIndex || 0;
    if (Math.abs(n) === Infinity) {
      n = 0;
    }

    if (n >= len) {
      return -1;
    }

    k = Math.max(n >= 0 ? n : len - Math.abs(n), 0);

    while (k < len) {
      if (k in o && o[k] === searchElement) {
        return k;
      }
      k++;
    }
    return -1;
  };
}