
namespace OSM.API.v0_6.XML
{
    
    [System.Xml.Serialization.XmlRoot(ElementName = "tag")]
    public class Tag
    {
        [System.Xml.Serialization.XmlAttribute(AttributeName = "k")]
        public string K { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    } // End Class Tag 
    
}