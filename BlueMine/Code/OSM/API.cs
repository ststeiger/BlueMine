
namespace BlueMine.OSM.API 
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

        public static void Test()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // https://www.openstreetmap.org/api/0.6/way/73685445
            Xml2CSharp.OSM.Way.OsmWayXml osm = Xml2CSharp.OSM.Way.OsmWayXml.FromUrl("https://www.openstreetmap.org/api/0.6/way/73685445");

            foreach (Xml2CSharp.OSM.Way.Nd node in osm.Way.Nd)
            {
                string @ref = node.Ref;

                // https://www.openstreetmap.org/api/0.6/node/872697431
                Xml2CSharp.OSM.Node.OsmNodeXml nodeOSM = Xml2CSharp.OSM.Node.OsmNodeXml.FromUrl($"https://www.openstreetmap.org/api/0.6/node/{@ref}");
                System.Threading.Thread.Sleep(5000);
                decimal lat = nodeOSM.Node.Lat;
                decimal lon = nodeOSM.Node.Lon;

                sb.Append(lat);
                sb.Append(", ");
                sb.Append(lon);
                sb.AppendLine();
            } // Next node 

            string str = sb.ToString();
            sb.Clear();
            sb = null;
            System.Console.WriteLine(str);
        } // End Sub Test 


    } // End Class API 


} // End Namespace BlueMine.OSM.Polygon 
