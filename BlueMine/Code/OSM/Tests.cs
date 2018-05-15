
namespace OSM.API.v0_6
{


    public class Tests
    {


        public static void TestBoundingBox()
        {
            GeoBoundingBox bb = GeoBoundingBox.FromDistance(44.168278M, 4.445944M, 50);

            // https://www.openstreetmap.org/api/0.6/map?bbox=4.4453171181025916513184263020,44.167828339818137730558940096,4.4465708818974083486815736980,44.168727660181862269441059904
            XML.OsmBoundingBoxXml xml = XML.OsmBoundingBoxXml.FromUrl(bb.Url);
            System.Console.WriteLine(xml);
        }


        public static void TestPolygonPoints()
        {
            string polygonPoints = Polygon.GetPoints("73685445");
            System.Console.WriteLine(polygonPoints);
        }


    }


}
