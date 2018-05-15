
// Licensed under the Apache License, Version 2.0 
// http://www.apache.org/licenses/LICENSE-2.0
// https://www.openstreetmap.org/api/0.6/way/73685445

// <osm version="0.6" generator="CGImap 0.6.0 (3789 thorn-03.openstreetmap.org)" copyright="OpenStreetMap and contributors" attribution="http://www.openstreetmap.org/copyright" license="http://opendatacommons.org/licenses/odbl/1-0/">
//   <way id="73685445" visible="true" version="2" changeset="11234790" timestamp="2012-04-09T09:09:39Z" user="MARTIN Yannick" uid="652329">
//     <nd ref="872700284"/>
//     <nd ref="872697431"/>
//     <nd ref="872703057"/>
//     <nd ref="872702535"/>
//     <nd ref="872697448"/>
//     <nd ref="872702458"/>
//     <nd ref="872703464"/>
//     <nd ref="872704415"/>
//     <nd ref="872704448"/>
//     <nd ref="872702962"/>
//     <nd ref="872699578"/>
//     <nd ref="872700572"/>
//     <nd ref="872700354"/>
//     <nd ref="872706869"/>
//     <nd ref="872700983"/>
//     <nd ref="872708342"/>
//     <nd ref="872708269"/>
//     <nd ref="872699706"/>
//     <nd ref="872704388"/>
//     <nd ref="872699318"/>
//     <nd ref="872703756"/>
//     <nd ref="872701555"/>
//     <nd ref="872700909"/>
//     <nd ref="872707541"/>
//     <nd ref="872701467"/>
//     <nd ref="872707288"/>
//     <nd ref="872697491"/>
//     <nd ref="872698850"/>
//     <nd ref="872704134"/>
//     <nd ref="872700646"/>
//     <nd ref="872698983"/>
//     <nd ref="872704580"/>
//     <nd ref="872702595"/>
//     <nd ref="872701441"/>
//     <nd ref="872705337"/>
//     <nd ref="872699658"/>
//     <nd ref="872699725"/>
//     <nd ref="872700284"/>
//     <tag k="historic" v="castle"/>
//     <tag k="source" v="cadastre-dgi-fr source : Direction G�n�rale des Imp�ts - Cadastre. Mise � jour : 2010"/>
//   </way>
// </osm>


namespace OSM.API.v0_6.XML
{
    
    
    [System.Xml.Serialization.XmlRoot(ElementName = "way")]
    public class Way
    {
        [System.Xml.Serialization.XmlElement(ElementName = "nd")]
        public System.Collections.Generic.List<Nd> Nd { get; set; }

        [System.Xml.Serialization.XmlElement(ElementName = "tag")]
        public System.Collections.Generic.List<Tag> Tag { get; set; }

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
    } // End Class Way 


    [System.Xml.Serialization.XmlRoot(ElementName = "osm")]
    public class OsmWayXml
    {
        [System.Xml.Serialization.XmlElement(ElementName = "way")]
        public Way Way { get; set; }

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

        public static OsmWayXml FromUrl(string url)
        {
            return Tools.XML.Serialization.DeserializeXmlFromUrl<OsmWayXml>(url);
        }

    } // End Class OsmWayXml 


} // End Namespace Xml2CSharp.OSM.Way 
