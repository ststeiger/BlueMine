
// Licensed under the Apache License, Version 2.0 
// http://www.apache.org/licenses/LICENSE-2.0 

namespace OSM.API.v0_6.XML
{ 
    
    
    // https://www.openstreetmap.org/api/0.6/node/872697431

    // <? xml version="1.0" encoding="UTF-8"?>
    // <osm version = "0.6" generator="CGImap 0.6.0 (21476 thorn-03.openstreetmap.org)" copyright="OpenStreetMap and contributors" attribution="http://www.openstreetmap.org/copyright" license="http://opendatacommons.org/licenses/odbl/1-0/">
    //  <node id = "872702458" visible="true" version="2" changeset="31695615" timestamp="2015-06-03T15:27:59Z" user="Sebastien Dinot" uid="13384" lat="44.1683617" lon="4.4457856"/>
    // </osm>

    [System.Xml.Serialization.XmlRoot(ElementName = "node")]
    public class Node
    {
        [System.Xml.Serialization.XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "visible")]
        public string Visible { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "changeset")]
        public string Changeset { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "user")]
        public string User { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "uid")]
        public string Uid { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "lat")]
        public decimal Lat { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "lon")]
        public decimal Lon { get; set; }
    } // End Class Node 


    [System.Xml.Serialization.XmlRoot(ElementName = "osm")]
    public class OsmNodeXml 
    {
        [System.Xml.Serialization.XmlElement(ElementName = "node")]
        public Node Node { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "generator")]
        public string Generator { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "copyright")]
        public string Copyright { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "attribution")]
        public string Attribution { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "license")]
        public string License { get; set; }
        

        public static OsmNodeXml FromUrl(string url)
        {
            return Tools.XML.Serialization.DeserializeXmlFromUrl<OsmNodeXml>(url);
        }

    } // End Class OsmNodeXml 


} // End Namespace Xml2CSharp.OSM.Node 
