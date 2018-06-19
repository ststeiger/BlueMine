export var maps;
(function (maps) {
    var map = null;
    var polygons = [];
    var markers = [];
    function polyFills() {
        Math.trunc = Math.trunc || function (x) {
            var n = x - x % 1;
            return n === 0 && (x < 0 || (x === 0 && (1 / x !== 1 / 0))) ? -0 : n;
        };
        Math.radians = function (degrees) {
            return degrees * Math.PI / 180.0;
        };
    }
    maps.polyFills = polyFills;
    function SetDefaultVariables(url) {
        if (window.parent.Settings) {
            url = url.replace("{@basic}", window.parent.Settings.basicLink);
        }
        if (window.top && window.top.Portal && window.top.Portal.Session && window.top.Portal.Session.ID) {
            url = url.replace("{@BE_Hash}", window.top.Portal.Session.ID());
        }
        else
            url = url.replace("{@BE_Hash}", "200CEB26807D6BF99FD6F4F0D1CA54D4");
        return url;
    }
    function spreadMessage(object) {
        var inFrame = (function () {
            try {
                return window.self !== window.top;
            }
            catch (e) {
                return true;
            }
        })();
        console.log("inFrame", inFrame);
        if (inFrame)
            Portal.Global.spreadMessage(object);
        else {
            window.postMessage(JSON.stringify(object), '*');
        }
    }
    function testNaviSO() {
        var msg = {
            "Action": "VWS.Tree.onAfterSelectionChange",
            "Param": {
                "Action": "",
                "Data": {
                    "Type": "SO",
                    "Value": "c38860a1-1c61-4590-9410-9fa1ab8586b1",
                    "Text": "0006 Althardstrasse",
                    "Parent": "31bfa452-e97d-475a-ac65-cf4d885fcd5c",
                    "ApertureObjID": "0000000002GQ0000C2",
                    "_hasPRT": 1,
                    "_hasInsert": 1,
                    "_hasDelete": 1
                }
            }
        };
        spreadMessage(msg);
    }
    function testNaviGB() {
        var msg = {
            "Action": "VWS.Tree.onAfterSelectionChange",
            "Param": {
                "Action": "",
                "Data": {
                    "Type": "GB",
                    "Value": "e79223ff-02a8-4a7a-b148-e1fbafa8d934",
                    "Text": "GB01 Althardstrasse 10",
                    "Background": "#00FF00",
                    "Parent": "c38860a1-1c61-4590-9410-9fa1ab8586b1",
                    "ApertureObjID": "0000000002GQ0000FQ",
                    "_hasPRT": 1,
                    "_hasInsert": 1,
                    "_hasDelete": 1
                }
            }
        };
        spreadMessage(msg);
    }
    function testFilterChange() {
        var msg = {
            "Action": "VWS.Tree.onAfterFilterChange",
            "Param": {
                "Datum": "",
                "LD": "",
                "RG": "",
                "ORT": ""
            }
        };
        spreadMessage(msg);
    }
    function navigateTo(uuid) {
        spreadMessage({
            Action: 'vws.tree.navigateto',
            Param: {
                navigateTo: uuid
            }
        });
    }
})(maps || (maps = {}));
