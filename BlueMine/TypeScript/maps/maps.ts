
declare global
{

    interface ISettings
    {
        basicLink:string;
    }
    
    interface IPortalSession
    {
        ID:() => string;
    }
    
    interface IPortalGlobal
    {
        spreadMessage:(object:any) => void;
    }
    
    interface IPortal
    {
        basicLink:string;
        Session:IPortalSession;
        Global:IPortalGlobal;
    }
    
    interface Window
    {
        Settings:ISettings;
        Portal:IPortal;
    }
    
    interface Math
    {
        trunc:(x:number) => number;
        radians:(x:number) => number;
    }

    // declare var Proxy: any;

}



interface IProxyHandler
{
    get(obj, prop, receiver);
}


// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Proxy
declare class Proxy
{
    public property: string;
    public method(): string;
    public constructor(obj, handler_callback: IProxyHandler);
}


declare var Portal: any;


export module maps
{
    let map = null;
    let polygons = [];
    let markers = [];


    interface SomeTable
    {
        col1: string;
        col2: number;
        col3: Date;
    }

    export class Table<T>
    {
        protected m_i: number;
        protected m_accessor: Proxy;

        public data: T[];
        //public columns: map<string, number>;
        public columns: { [key: string]: number };
        
        public rows: T[];

        //get bar(): boolean
        //{
        //    return null; // this._bar;
        //}
        //set bar(theBar: boolean)
        //{
        //    //this._bar = theBar;
        //}

        protected row(index:number): T
        {
            this.m_i = index;
            return <T><any>this.m_accessor;
        }


        constructor(rows: any[][], columnNames:string[])
        {
            this.data = <any>rows;
            this.m_i = 0;
            
            // this.columns = columnNames;
            this.columns = {}; // "associative array" or Object
            for (let i = 0; i < columnNames.length; ++i)
            {
                this.columns[columnNames[i]] = i;
            }


            this.row = this.row.bind(this);

            let handlerPropertyAccess: IProxyHandler = {
                get: function (obj, prop, receiver)
                {
                    return this.obj[this.i][this.columns[prop]];
                }
            };
            handlerPropertyAccess.get = handlerPropertyAccess.get.bind(this);
            this.m_accessor = new Proxy(this.data, handlerPropertyAccess);


            let handlerIndex: IProxyHandler = {
                get: function (obj, prop, receiver)
                {
                    return this.row(prop);
                }
            };
            handlerIndex.get = handlerIndex.get.bind(this);
            this.rows = <any>new Proxy(this.data, handlerIndex);
        }

    }


    export function testTable()
    {
        let columns = ["col1", "col2", "col3"];
        let rows = [
            ["row 1 col 1", "row 1 col 2", "row 1 col 3"]
            , ["row 2 col 1", "row 2 col 2", "row 2 col 3"]
            , ["row 3 col 1", "row 3 col 2", "row 3 col 3"]
            , ["row 4 col 1", "row 4 col 2", "row 4 col 3"]
            , ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];

        let x = new Table<SomeTable>(rows, columns);
        
        console.log(x.rows[0].col1);
        // console.log(x.row(1).col1);
        //console.log(x.obj[0][0]);
    }

    export function proxy()
    {
        let columns = ["col1", "col2", "col3"];
        let rows = [
              ["row 1 col 1", "row 1 col 2", "row 1 col 3"]
            , ["row 2 col 1", "row 2 col 2", "row 2 col 3"]
            , ["row 3 col 1", "row 3 col 2", "row 3 col 3"]
            , ["row 4 col 1", "row 4 col 2", "row 4 col 3"]
            , ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];

        let cols = {}; // "associative array" or Object

        for (let i = 0; i < columns.length; ++i)
        {
            cols[columns[i]] = i;
        }


        let handler2 = {
            get: function (obj, prop, receiver)
            {
                return obj[cols[prop]];
            }
        };

        // https://www.sitepoint.com/es6-proxies/
        let handler = {
            get: function (obj, prop, receiver)
            {
                console.log("obj:", obj, "prop:", prop, "receiver :", receiver);
                //return obj[prop];
                //return obj[cols[prop]];
                return new Proxy(obj[prop], handler2);
            }

            , set: function (obj, key, value)
            {
                console.log(obj, key, value);
            }

        };

        let p = new Proxy(rows, handler);
        // p[0].col1
        // p[0].col2
        // p[1].col2
    }

    // https://caniuse.com/#feat=proxy
    // Sorry, your browser is no longer supported. 
    // If you want this program to support IE11, develop a proxy-polyfill for IE11. 
    // Hint from Babel-docs: ES2015-Proxies requires support on the engine level; it is thus not possible to polyfill Proxy on ES5.
    export function tableTest1()
    {
        let columns = ["col1", "col2", "col3"];
        let rows = [
              ["row 1 col 1", "row 1 col 2", "row 1 col 3"]
            , ["row 2 col 1", "row 2 col 2", "row 2 col 3"]
            , ["row 3 col 1", "row 3 col 2", "row 3 col 3"]
            , ["row 4 col 1", "row 4 col 2", "row 4 col 3"]
            , ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];

        let cols = {}; // "associative array" or Object

        for (let i = 0; i < columns.length; ++i)
        {
            cols[columns[i]] = i;
        }

        let index_col1 = cols["col1"];
        let index_col2 = cols["col2"];
        let index_col3 = cols["col3"];


        for (var i = 0; i < rows.length; ++i)
        {
            console.log("col1:", rows[i][index_col1], "col2:", rows[i][index_col2], "col3:", rows[i][index_col3]);
        }

    }


    export function tableTest()
    {
        let columns = ["col1", "col2", "col3"];

        let data = [
              ["row 1 col 1", "row 1 col 2", "row 1 col 3"]
            , ["row 2 col 1", "row 2 col 2", "row 2 col 3"]
            , ["row 3 col 1", "row 3 col 2", "row 3 col 3"]
            , ["row 4 col 1", "row 4 col 2", "row 4 col 3"]
            , ["row 5 col 1", "row 5 col 2", "row 5 col 3"]
        ];



        let arr = [];


        for (let j = 0; j < data.length; ++j)
        {
            let obj = {}; // "associative array" or Object

            for (let i = 0; i < columns.length; ++i)
            {
                obj[columns[i]] = data[j][i];
            }
            arr.push(obj);
        }



        let b = [
              { "col1": "row 1 col 1", "col2": "row 1 col 2", "col3": "row 1 col 3" }
            , { "col1": "row 2 col 1", "col2": "row 2 col 2", "col3": "row 2 col 3" }
            , { "col1": "row 3 col 1", "col2": "row 3 col 2", "col3": "row 3 col 3" }
            , { "col1": "row 4 col 1", "col2": "row 4 col 2", "col3": "row 4 col 3" }
            , { "col1": "row 5 col 1", "col2": "row 5 col 2", "col3": "row 5 col 3" }
        ];



        // JSON.stringify(data, null, 2)
        let dataJSON = `[
            [
                "row 1 col 1",
                "row 1 col 2",
                "row 1 col 3"
            ],
            [
                "row 2 col 1",
                "row 2 col 2",
                "row 2 col 3"
            ],
            [
                "row 3 col 1",
                "row 3 col 2",
                "row 3 col 3"
            ],
            [
                "row 4 col 1",
                "row 4 col 2",
                "row 4 col 3"
            ],
            [
                "row 5 col 1",
                "row 5 col 2",
                "row 5 col 3"
            ]
]`;


        // JSON.stringify(b, null, 2)

        let bb = `[
  {
    "col1": "row 1 col 1",
    "col2": "row 1 col 2",
    "col3": "row 1 col 3"
  },
  {
    "col1": "row 2 col 1",
    "col2": "row 2 col 2",
    "col3": "row 2 col 3"
  },
  {
    "col1": "row 3 col 1",
    "col2": "row 3 col 2",
    "col3": "row 3 col 3"
  },
  {
    "col1": "row 4 col 1",
    "col2": "row 4 col 2",
    "col3": "row 4 col 3"
  },
  {
    "col1": "row 5 col 1",
    "col2": "row 5 col 2",
    "col3": "row 5 col 3"
  }
]`;

    }

    
    export function polyFills()
    {
        Math.trunc = Math.trunc || function (x)
        {
            var n = x - x % 1;
            return n === 0 && (x < 0 || (x === 0 && (1 / x !== 1 / 0))) ? -0 : n;
        };

        Math.radians = function (degrees)
        {
            return degrees * Math.PI / 180.0;
        };
        
    }


    function SetDefaultVariables(url)
    {
        if (window.parent.Settings)
        {
            url = url.replace("{@basic}", window.parent.Settings.basicLink);
        }

        if (window.top && window.top.Portal && window.top.Portal.Session && window.top.Portal.Session.ID)
        {
            url = url.replace("{@BE_Hash}", window.top.Portal.Session.ID());
        }
        else
            url = url.replace("{@BE_Hash}", "200CEB26807D6BF99FD6F4F0D1CA54D4");

        return url;
    }


    function spreadMessage(object)
    {
        var inFrame = (function ()
        {
            try
            {
                return window.self !== window.top;
            } catch (e)
            {
                return true;
            }
        })();
        console.log("inFrame", inFrame);
        
        if (inFrame)
            Portal.Global.spreadMessage(object);
        else
        {
            //window.postMessage(JSON.stringify({ "msg": "Hello world" }), '*');
            window.postMessage(JSON.stringify(object), '*');
        }
    }



    function testNaviSO()
    {
        let msg =
            {
                "Action": "VWS.Tree.onAfterSelectionChange"
                , "Param": {
                    "Action": ""
                    , "Data": {
                        "Type": "SO"
                        , "Value": "c38860a1-1c61-4590-9410-9fa1ab8586b1"
                        , "Text": "0006 Althardstrasse"
                        , "Parent": "31bfa452-e97d-475a-ac65-cf4d885fcd5c"
                        , "ApertureObjID": "0000000002GQ0000C2"
                        , "_hasPRT": 1
                        , "_hasInsert": 1
                        , "_hasDelete": 1
                    }
                }
            };

        spreadMessage(msg);
    }
    
    
    function testNaviGB()
    {
        let msg =
            {
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
            }
        ;

        spreadMessage(msg);
    }
    
    
    function testFilterChange()
    {
        let msg =
            {
                "Action": "VWS.Tree.onAfterFilterChange",
                "Param": {
                    "Datum": "",
                    "LD": "",
                    "RG": "",
                    "ORT": ""
                }
            }
        ;
        
        spreadMessage(msg);
    }
    
    
    function navigateTo(uuid)
    {
        spreadMessage(
            {
                Action: 'vws.tree.navigateto',
                Param: {
                    navigateTo: uuid // '9dc95c1c-4830-4b01-85b5-593b6ea5e44b'
                }
            }
        );
    }


    // https://gis.stackexchange.com/a/816/3997
    // https://jsfiddle.net/xwaocc00/
    function polygonArea(poly2)
    {
        let poly = JSON.parse(JSON.stringify(poly2));
        let p1, p2, i;
        let area = 0.0;
        let len = poly.length;
        
        if (len > 2)
        {

            for (i = 0; i < len; i++)
            {
                poly[i] = poly[i].map(Math.radians)
            }

            for (i = 0; i < len - 1; i++)
            {
                p1 = poly[i];
                p2 = poly[i + 1];

                area += (p2[0] - p1[0]) *
                    (
                        2
                        + Math.sin(p1[1])
                        + Math.sin(p2[1])
                    );
            }

            // https://en.wikipedia.org/wiki/Earth_radius#Equatorial_radius
            // https://en.wikipedia.org/wiki/Earth_ellipsoid
            // The radius you are using, 6378137.0 m corresponds to the equatorial radius of the Earth.
            let equatorial_radius = 6378137; // m
            let polar_radius = 6356752.3142; // m
            let mean_radius = 6371008.8; // m
            let authalic_radius = 6371007.2; // m (radius of perfect sphere with same surface as reference ellipsoid)
            let volumetric_radius = 6371000.8 // m (radius of a sphere of volume equal to the ellipsoid)
            
            let radius = mean_radius;
            
            area = area * radius * radius / 2.0;
        } // End if len > 0

        // equatorial_radius: 6391.565558418869 m2
        // mean_radius:       6377.287126172337m2
        // authalic_radius:   6377.283923019292 m2
        // volumetric_radius: 6377.271110415153 m2
        // merid_radius:      6375.314923754325 m2
        // polar_radius:      6348.777989748668 m2
        // R:                 6368.48180842528 m2
        // hrad:              6391.171919886588 m2

        // http://postgis.net/docs/doxygen/2.2/dc/d52/geography__measurement_8c_a1a7c48d59bcf4ed56522ab26c142f61d.html
        // ST_Area(g)               5.21556075001092E-07
        // ST_Area(g, false)     6379.25032051953
        // ST_Area(g, true)      6350.65051177517

        // return area;
        // return area.toFixed(2);
        return Math.abs(area).toFixed(0);
    }


    function latLongToString(latlng)
    {
        let x = latlng.lat;
        let y = latlng.lng;

        let prefix1 = x < 0 ? "S" : "N";
        let prefix2 = y < 0 ? "W" : "E";

        x = Math.abs(x);
        y = Math.abs(y);

        let grad1 = Math.trunc(x);
        x = (x - grad1) * 60;
        let grad2 = Math.trunc(y);
        y = (y - grad2) * 60;

        let min1:any = Math.trunc(x);
        let min2:any = Math.trunc(y);

        let sec1:any = ((x - min1) * 60).toFixed(1);
        let sec2:any = ((y - min2) * 60).toFixed(1);

        min1 = (min1 < 10 ? "0" : "") + min1;
        min2 = (min2 < 10 ? "0" : "") + min2;

        sec1 = (sec1 < 10 ? "0" : "") + sec1;
        sec2 = (sec2 < 10 ? "0" : "") + sec2;

        let res = grad1 + "°" + min1 + "'" + sec1 + '"' + prefix1 + " " + grad2 + "°" + min2 + "'" + sec2 + '"' + prefix2;
        return res;
    }


    function numberWithCommas(x)
    {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, "'");
    }
    

}