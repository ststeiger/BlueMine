export var maps;
(function (maps) {
    var map = null;
    var polygons = [];
    var markers = [];
    var Table = (function () {
        function Table(rows, columnNames) {
            this.data = rows;
            this.m_i = 0;
            this.columns = {};
            for (var i = 0; i < columnNames.length; ++i) {
                this.columns[columnNames[i]] = i;
            }
            this.row = this.row.bind(this);
            var handlerPropertyAccess = {
                get: function (obj, prop, receiver) {
                    return this.obj[this.i][this.columns[prop]];
                }
            };
            handlerPropertyAccess.get = handlerPropertyAccess.get.bind(this);
            this.m_accessor = new Proxy(this.data, handlerPropertyAccess);
            var handlerIndex = {
                get: function (obj, prop, receiver) {
                    return this.row(prop);
                }
            };
            handlerIndex.get = handlerIndex.get.bind(this);
            this.rows = new Proxy(this.data, handlerIndex);
        }
        Table.prototype.row = function (index) {
            this.m_i = index;
            return this.m_accessor;
        };
        return Table;
    }());
    maps.Table = Table;
    function testTable() {
        var columns = ["col1", "col2", "col3"];
        var rows = [
            ["row 1 col 1", "row 1 col 2", "row 1 col 3"],
            ["row 2 col 1", "row 2 col 2", "row 2 col 3"],
            ["row 3 col 1", "row 3 col 2", "row 3 col 3"],
            ["row 4 col 1", "row 4 col 2", "row 4 col 3"],
            ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];
        var x = new Table(rows, columns);
        console.log(x.rows[0].col1);
    }
    maps.testTable = testTable;
    function proxy() {
        var columns = ["col1", "col2", "col3"];
        var rows = [
            ["row 1 col 1", "row 1 col 2", "row 1 col 3"],
            ["row 2 col 1", "row 2 col 2", "row 2 col 3"],
            ["row 3 col 1", "row 3 col 2", "row 3 col 3"],
            ["row 4 col 1", "row 4 col 2", "row 4 col 3"],
            ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];
        var cols = {};
        for (var i = 0; i < columns.length; ++i) {
            cols[columns[i]] = i;
        }
        var handler2 = {
            get: function (obj, prop, receiver) {
                return obj[cols[prop]];
            }
        };
        var handler = {
            get: function (obj, prop, receiver) {
                console.log("obj:", obj, "prop:", prop, "receiver :", receiver);
                return new Proxy(obj[prop], handler2);
            },
            set: function (obj, key, value) {
                console.log(obj, key, value);
            }
        };
        var p = new Proxy(rows, handler);
    }
    maps.proxy = proxy;
    function tableTest1() {
        var columns = ["col1", "col2", "col3"];
        var rows = [
            ["row 1 col 1", "row 1 col 2", "row 1 col 3"],
            ["row 2 col 1", "row 2 col 2", "row 2 col 3"],
            ["row 3 col 1", "row 3 col 2", "row 3 col 3"],
            ["row 4 col 1", "row 4 col 2", "row 4 col 3"],
            ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];
        var cols = {};
        for (var i_1 = 0; i_1 < columns.length; ++i_1) {
            cols[columns[i_1]] = i_1;
        }
        var index_col1 = cols["col1"];
        var index_col2 = cols["col2"];
        var index_col3 = cols["col3"];
        for (var i = 0; i < rows.length; ++i) {
            console.log("col1:", rows[i][index_col1], "col2:", rows[i][index_col2], "col3:", rows[i][index_col3]);
        }
    }
    maps.tableTest1 = tableTest1;
    function tableTest() {
        var columns = ["col1", "col2", "col3"];
        var data = [
            ["row 1 col 1", "row 1 col 2", "row 1 col 3"],
            ["row 2 col 1", "row 2 col 2", "row 2 col 3"],
            ["row 3 col 1", "row 3 col 2", "row 3 col 3"],
            ["row 4 col 1", "row 4 col 2", "row 4 col 3"],
            ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];
        var arr = [];
        for (var j = 0; j < data.length; ++j) {
            var obj = {};
            for (var i = 0; i < columns.length; ++i) {
                obj[columns[i]] = data[j][i];
            }
            arr.push(obj);
        }
        var b = [
            { "col1": "row 1 col 1", "col2": "row 1 col 2", "col3": "row 1 col 3" },
            { "col1": "row 2 col 1", "col2": "row 2 col 2", "col3": "row 2 col 3" },
            { "col1": "row 3 col 1", "col2": "row 3 col 2", "col3": "row 3 col 3" },
            { "col1": "row 4 col 1", "col2": "row 4 col 2", "col3": "row 4 col 3" },
            { "col1": "row 5 col 1", "col2": "row 5 col 2", "col3": "row 5 col 3" }
        ];
        var dataJSON = "[\n            [\n                \"row 1 col 1\",\n                \"row 1 col 2\",\n                \"row 1 col 3\"\n            ],\n            [\n                \"row 2 col 1\",\n                \"row 2 col 2\",\n                \"row 2 col 3\"\n            ],\n            [\n                \"row 3 col 1\",\n                \"row 3 col 2\",\n                \"row 3 col 3\"\n            ],\n            [\n                \"row 4 col 1\",\n                \"row 4 col 2\",\n                \"row 4 col 3\"\n            ],\n            [\n                \"row 5 col 1\",\n                \"row 5 col 2\",\n                \"row 5 col 3\"\n            ]\n]";
        var bb = "[\n  {\n    \"col1\": \"row 1 col 1\",\n    \"col2\": \"row 1 col 2\",\n    \"col3\": \"row 1 col 3\"\n  },\n  {\n    \"col1\": \"row 2 col 1\",\n    \"col2\": \"row 2 col 2\",\n    \"col3\": \"row 2 col 3\"\n  },\n  {\n    \"col1\": \"row 3 col 1\",\n    \"col2\": \"row 3 col 2\",\n    \"col3\": \"row 3 col 3\"\n  },\n  {\n    \"col1\": \"row 4 col 1\",\n    \"col2\": \"row 4 col 2\",\n    \"col3\": \"row 4 col 3\"\n  },\n  {\n    \"col1\": \"row 5 col 1\",\n    \"col2\": \"row 5 col 2\",\n    \"col3\": \"row 5 col 3\"\n  }\n]";
    }
    maps.tableTest = tableTest;
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
    function polygonArea(poly2) {
        var poly = JSON.parse(JSON.stringify(poly2));
        var p1, p2, i;
        var area = 0.0;
        var len = poly.length;
        if (len > 2) {
            for (i = 0; i < len; i++) {
                poly[i] = poly[i].map(Math.radians);
            }
            for (i = 0; i < len - 1; i++) {
                p1 = poly[i];
                p2 = poly[i + 1];
                area += (p2[0] - p1[0]) *
                    (2
                        + Math.sin(p1[1])
                        + Math.sin(p2[1]));
            }
            var equatorial_radius = 6378137;
            var polar_radius = 6356752.3142;
            var mean_radius = 6371008.8;
            var authalic_radius = 6371007.2;
            var volumetric_radius = 6371000.8;
            var radius = mean_radius;
            area = area * radius * radius / 2.0;
        }
        return Math.abs(area).toFixed(0);
    }
    function latLongToString(latlng) {
        var x = latlng.lat;
        var y = latlng.lng;
        var prefix1 = x < 0 ? "S" : "N";
        var prefix2 = y < 0 ? "W" : "E";
        x = Math.abs(x);
        y = Math.abs(y);
        var grad1 = Math.trunc(x);
        x = (x - grad1) * 60;
        var grad2 = Math.trunc(y);
        y = (y - grad2) * 60;
        var min1 = Math.trunc(x);
        var min2 = Math.trunc(y);
        var sec1 = ((x - min1) * 60).toFixed(1);
        var sec2 = ((y - min2) * 60).toFixed(1);
        min1 = (min1 < 10 ? "0" : "") + min1;
        min2 = (min2 < 10 ? "0" : "") + min2;
        sec1 = (sec1 < 10 ? "0" : "") + sec1;
        sec2 = (sec2 < 10 ? "0" : "") + sec2;
        var res = grad1 + "°" + min1 + "'" + sec1 + '"' + prefix1 + " " + grad2 + "°" + min2 + "'" + sec2 + '"' + prefix2;
        return res;
    }
    function numberWithCommas(x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, "'");
    }
})(maps || (maps = {}));
