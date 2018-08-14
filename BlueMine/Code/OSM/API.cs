
namespace OSM.API.v0_6
{


    // http://nominatim.openstreetmap.org/search?format=xml&q=44.168278,4.445944
    public class Polygon
    {

        // 44.1683810, 4.4460592
        // 44.1683805, 4.4459657
        // 44.1683790, 4.4459432
        // 44.1683733, 4.4458814
        // 44.1683669, 4.4458296
        // 44.1683617, 4.4457856
        // 44.1683591, 4.4457662
        // 44.1683644, 4.4457620
        // 44.1683701, 4.4457513
        // 44.1683717, 4.4457414
        // 44.1683693, 4.4457264
        // 44.1683641, 4.4457168
        // 44.1683579, 4.4457114
        // 44.1683500, 4.4457026
        // 44.1683423, 4.4456513
        // 44.1683320, 4.4456555
        // 44.1683273, 4.4456644
        // 44.1683220, 4.4456807
        // 44.1683197, 4.4457058
        // 44.1683171, 4.4457334
        // 44.1682368, 4.4457636
        // 44.1682231, 4.4457730
        // 44.1682109, 4.4457845
        // 44.1682283, 4.4458652
        // 44.1682552, 4.4458584
        // 44.1682988, 4.4460135
        // 44.1682068, 4.4460419
        // 44.1682026, 4.4460260
        // 44.1680923, 4.4460372
        // 44.1682116, 4.4458787
        // 44.1681763, 4.4458272
        // 44.1680972, 4.4459363
        // 44.1680783, 4.4460028
        // 44.1680590, 4.4460501
        // 44.1680488, 4.4461376
        // 44.1682454, 4.4460895
        // 44.1682537, 4.4461225
        // 44.1683810, 4.4460592


        public static string GetPoints(string wayId)
        {
            string polygonPoints = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // https://www.openstreetmap.org/api/0.6/way/73685445
            OSM.API.v0_6.XML.OsmWayXml osm = OSM.API.v0_6.XML.OsmWayXml.FromUrl($"https://www.openstreetmap.org/api/0.6/way/{wayId}");

            foreach (GeoPoint geopoint in GetPointList(wayId))
            {
                sb.Append(geopoint.Latitude);
                sb.Append(", ");
                sb.Append(geopoint.Longitude);
                sb.AppendLine();
            } // Next geopoint 

            polygonPoints = sb.ToString();
            sb.Clear();
            sb = null;

            return polygonPoints;
        } // End Function GetPoints 


        public static System.Collections.Generic.List<GeoPoint> GetPointList(string wayId)
        {
            System.Collections.Generic.List<GeoPoint> ls = new System.Collections.Generic.List<GeoPoint>();

            // https://www.openstreetmap.org/api/0.6/way/73685445
            OSM.API.v0_6.XML.OsmWayXml osm = OSM.API.v0_6.XML.OsmWayXml.FromUrl($"https://www.openstreetmap.org/api/0.6/way/{wayId}");

            foreach (OSM.API.v0_6.XML.Nd node in osm.Way.Nd)
            {
                string @ref = node.Ref;

                // https://www.openstreetmap.org/api/0.6/node/872697431
                OSM.API.v0_6.XML.OsmNodeXml nodeOSM = OSM.API.v0_6.XML.OsmNodeXml.FromUrl($"https://www.openstreetmap.org/api/0.6/node/{@ref}");
                System.Threading.Thread.Sleep(5000);
                decimal lat = nodeOSM.Node.Lat;
                decimal lon = nodeOSM.Node.Lon;

                ls.Add(new GeoPoint(lat, lon));
            } // Next node 

            return ls;
        } // End Function GetPointList 


        // string sql_insert = OSM.API.v0_6.Polygon.GetPointsInsert("66824085", "42A88938-AF0A-401A-8645-C16911B07CD1"); 
        // System.Console.WriteLine(sql_insert);
        public static string GetPointsInsert(string wayId, string gb_uid)
        {
            string insert = null;
            gb_uid = gb_uid.Replace("'", "''");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine();
            sb.AppendLine(@"DELETE FROM T_ZO_Objekt_Wgs84Polygon WHERE ZO_OBJ_WGS84_GB_UID = '"+ gb_uid + "'; ");
            sb.AppendLine();
            sb.AppendLine();


            // System.Collections.Generic.List<GeoPoint> ls = new System.Collections.Generic.List<GeoPoint>();
            // ls.Add(new GeoPoint(47.3965285M, 8.5977877M));
            // ls.Add(new GeoPoint(57.3965285M, 18.5977877M));
            // ls.Add(new GeoPoint(67.3965285M, 28.5977877M));

            // foreach (GeoPoint geopoint in ls)
            int i = 0;
            foreach (GeoPoint geopoint in GetPointList(wayId))
            {
                sb.AppendLine(@"INSERT INTO T_ZO_Objekt_Wgs84Polygon 
( 
     ZO_OBJ_WGS84_UID, ZO_OBJ_WGS84_GB_UID, ZO_OBJ_WGS84_SO_UID 
    ,ZO_OBJ_WGS84_Sort ,ZO_OBJ_WGS84_GM_Lat, ZO_OBJ_WGS84_GM_Lng 
) 
VALUES 
( ");

                sb.Append("      '");
                sb.Append(System.Guid.NewGuid().ToString());
                sb.Append("', '");
                sb.Append(gb_uid);
                sb.Append("', NULL, ");
                sb.Append(i);
                sb.AppendLine(" ");
                sb.Append("    , CAST(");
                sb.Append(geopoint.Latitude);
                sb.Append(@" AS decimal(23,20) ), ");
                sb.Append("CAST(");
                sb.Append(geopoint.Longitude);
                sb.AppendLine(@" AS decimal(23,20) ) ");
                sb.AppendLine("); ");
                sb.AppendLine(System.Environment.NewLine);
                i++;
            } // Next geopoint 

            insert = sb.ToString();
            sb.Clear();
            sb = null;

            return insert;
        } // End Function GetPointsInsert 


    } // End Class Polygon 



    public class GeoPoint
    {
        public decimal Latitude;
        public decimal Longitude;


        public GeoPoint()
        { } // End Constructor 


        public GeoPoint(decimal latitude, decimal longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        } // End Constructor 

    } // End Class GeoPoint 


    public class GeoBoundingBox
    {
        public GeoPoint TopLeft;
        public GeoPoint BottomRight;


        public GeoBoundingBox()
        { } // End Constructor GeoBoundingBox


        public GeoBoundingBox(GeoPoint topLeft, GeoPoint bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        } // End Constructor GeoBoundingBox


        public GeoBoundingBox(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            this.TopLeft = new GeoPoint(lat1, lon1);
            this.BottomRight = new GeoPoint(lat2, lon2);
        } // End Constructor GeoBoundingBox


        // left is the longitude of the left (westernmost) side of the bounding box.
        public decimal Left
        {
            get { return this.TopLeft.Longitude; }
        } // End Property Left 


        // bottom is the latitude of the bottom (southernmost) side of the bounding box.
        public decimal Bottom
        {
            get { return this.BottomRight.Latitude; }
        } // End Property Bottom 


        // right is the longitude of the right (easternmost) side of the bounding box.
        public decimal Right
        {
            get { return this.BottomRight.Longitude; }
        } // End Property Right 

        // top is the latitude of the top (northernmost) side of the bounding box.
        public decimal Top
        {
            get { return this.TopLeft.Latitude; }
        } // End Property Top 


        public string Url
        {
            get
            {
                return $"https://www.openstreetmap.org/api/0.6/map?bbox={Left},{Bottom},{Right},{Top}";
            }
        } // End Property Url 


        // https://wiki.openstreetmap.org/wiki/API_v0.6#Retrieving_map_data_by_bounding_box:_GET_.2Fapi.2F0.6.2Fmap
        // https://stackoverflow.com/questions/1689096/calculating-bounding-box-a-certain-distance-away-from-a-lat-long-coordinate-in-j
        public static GeoBoundingBox FromDistance(decimal lat, decimal lon, decimal distanceInMeters)
        {

            double ToRadians(decimal val)
            {
                return (double)(val / 180.0M * (decimal)System.Math.PI);
            }

            decimal ToDegrees(decimal val)
            {
                return val / (decimal)System.Math.PI * 180.0M;
            }

            // https://en.wikipedia.org/wiki/Earth_radius
            // For Earth, the mean radius is 6,371.0088 km
            decimal R = 6371.0088M;  // earth radius in km
            decimal radius = distanceInMeters * 0.001M; // km

            decimal lon1 = lon - ToDegrees(radius / R / (decimal)System.Math.Cos(ToRadians(lat)));
            decimal lon2 = lon + ToDegrees(radius / R / (decimal)System.Math.Cos(ToRadians(lat)));

            decimal lat1 = lat + ToDegrees(radius / R);
            decimal lat2 = lat - ToDegrees(radius / R);

            return new GeoBoundingBox(lat1, lon1, lat2, lon2);
        } // End Function FromDistance 


        public static GeoBoundingBox CorrectBoundingBoxFromDistance(decimal a, decimal b, decimal r)
        {

            // degrees to radians
            decimal deg2rad(decimal degrees)
            {
                return System.DecimalMath.PI * degrees / 180.0M;
            };

            // radians to degrees
            decimal rad2deg(decimal radians)
            {
                return 180.0M * radians / System.DecimalMath.PI;
            }


            // Earth radius at a given latitude, according to the WGS- 84 ellipsoid [m]
            decimal WGS84EarthRadius(decimal lat)
            {
                // Semi - axes of WGS- 84 geoidal reference
                const decimal WGS84_a = 6378137.0M;  // Major semiaxis [m]
                const decimal WGS84_b = 6356752.3M;  // Minor semiaxis [m]

                // http://en.wikipedia.org/wiki/Earth_radius
                // decimal An = WGS84_a * WGS84_a * ((decimal)System.Math.Cos((double)lat));
                // decimal Bn = WGS84_b * WGS84_b * ((decimal)System.Math.Sin((double)lat));
                // decimal Ad = WGS84_a * (decimal)System.Math.Cos((double)lat);
                // decimal Bd = WGS84_b * (decimal)System.Math.Sin((double)lat);

                decimal An = WGS84_a * WGS84_a * System.DecimalMath.Cos(lat);
                decimal Bn = WGS84_b * WGS84_b * System.DecimalMath.Sin(lat);
                decimal Ad = WGS84_a * System.DecimalMath.Cos(lat);
                decimal Bd = WGS84_b * System.DecimalMath.Sin(lat);

                //return (decimal) System.Math.Sqrt( (double)( (An * An + Bn * Bn) / (Ad * Ad + Bd * Bd) ) );
                return System.DecimalMath.Sqrt((An * An + Bn * Bn) / (Ad * Ad + Bd * Bd));
            }

            // Bounding box surrounding the point at given coordinates,
            // assuming local approximation of Earth surface as a sphere
            // of radius given by WGS84
            // https://stackoverflow.com/questions/238260/how-to-calculate-the-bounding-box-for-a-given-lat-lng-location
            (decimal, decimal, decimal, decimal) boundingBox(decimal latitudeInDegrees, decimal longitudeInDegrees, decimal halfSideInKm)
            {
                decimal lat = deg2rad(latitudeInDegrees);
                decimal lon = deg2rad(longitudeInDegrees);
                decimal halfSide = 1000.0M * halfSideInKm;

                // Radius of Earth at given latitude
                decimal radius = WGS84EarthRadius(lat);
                // Radius of the parallel at given latitude
                //decimal pradius = radius * (decimal)System.Math.Cos((double)lat);
                decimal pradius = radius * System.DecimalMath.Cos(lat);

                decimal latMin = lat - halfSide / radius;
                decimal latMax = lat + halfSide / radius;
                decimal lonMin = lon - halfSide / pradius;
                decimal lonMax = lon + halfSide / pradius;

                // GeoBoundingBox xa = new GeoBoundingBox(rad2deg(latMin), rad2deg(lonMin), rad2deg(latMax), rad2deg(lonMax));
                return (rad2deg(latMin), rad2deg(lonMin), rad2deg(latMax), rad2deg(lonMax));
            }

            (decimal, decimal, decimal, decimal) xxx = boundingBox(a, b, r/1000.0M);
            return new GeoBoundingBox(xxx.Item1, xxx.Item2, xxx.Item3, xxx.Item4);
        }


    } // End Class GeoBoundingBox 


} // End Namespace BlueMine.OSM.Polygon 
