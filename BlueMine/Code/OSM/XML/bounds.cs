
// Licensed under the Apache License, Version 2.0
// http://www.apache.org/licenses/LICENSE-2.0

using System.Xml.Serialization;
using System.Collections.Generic;

// https://wiki.openstreetmap.org/wiki/API_v0.6#Retrieving_map_data_by_bounding_box:_GET_.2Fapi.2F0.6.2Fmap

// $"https://www.openstreetmap.org/api/0.6/map?bbox={Left},{Bottom},{Right},{Top}";
// where:
// left is the longitude of the left (westernmost) side of the bounding box.
// bottom is the latitude of the bottom (southernmost) side of the bounding box.
// right is the longitude of the right (easternmost) side of the bounding box.
// top is the latitude of the top (northernmost) side of the bounding box.


namespace OSM.API.v0_6.XML
{
    
    
    [XmlRoot(ElementName = "bounds")]
    public class Bounds
    {
        [XmlAttribute(AttributeName = "minlat")]
        public string Minlat { get; set; }

        [XmlAttribute(AttributeName = "minlon")]
        public string Minlon { get; set; }

        [XmlAttribute(AttributeName = "maxlat")]
        public string Maxlat { get; set; }

        [XmlAttribute(AttributeName = "maxlon")]
        public string Maxlon { get; set; }
    }
    
    
    [XmlRoot(ElementName = "osm")]
    public class OsmBoundingBoxXml
    {
        [XmlElement(ElementName = "bounds")] 
        public Bounds Bounds { get; set; }
        
        [XmlElement(ElementName = "node")] 
        public List<Node> Node { get; set; }
        
        [XmlElement(ElementName = "way")] 
        public List<Way> Way { get; set; }
        
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [XmlAttribute(AttributeName = "generator")]
        public string Generator { get; set; }

        [XmlAttribute(AttributeName = "copyright")]
        public string Copyright { get; set; }

        [XmlAttribute(AttributeName = "attribution")]
        public string Attribution { get; set; }

        [XmlAttribute(AttributeName = "license")]
        public string License { get; set; }


        public static OsmBoundingBoxXml FromUrl(string url)
        {
            return Tools.XML.Serialization.DeserializeXmlFromUrl<OsmBoundingBoxXml>(url);
        }
    }
    
}
