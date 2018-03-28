
// Licensed under the Apache License, Version 2.0 
// http://www.apache.org/licenses/LICENSE-2.0

// RDF Site Summary (RSS) is a
// RSS 2.0 SPECIFICATION
// RSS as of the Fall of 2002, version 2.0.1. 

// basic spec for RSS 0.91 (June 2000) 
// new features introduced in RSS 0.92 (December 2000) 
// and RSS 0.94 (August 2002). 



// The Atom syndication format was published as an IETF proposed standard in RFC 4287 (December 2005), 
// and the Atom Publishing Protocol was published as RFC 5023 (October 2007).
// When Atom emerged as a format intended to rival or replace RSS

// https://en.wikipedia.org/wiki/Comparison_of_feed_aggregators
// https://validator.w3.org/feed/check.cgi

// RSS
// https://validator.w3.org/feed/docs/rss2.html
// https://tools.ietf.org/html/rfc5005
// http://cyber.harvard.edu/rss/rss.html
// http://web.resource.org/rss/1.0/spec



// https://tools.ietf.org/html/rfc4287
// https://validator.w3.org/feed/docs/atom.html


// https://www.w3.org/standards/techs/rdf#w3c_all
// https://www.w3.org/TR/2014/REC-rdf-schema-20140225/


using System.Xml.Serialization;
using System.Collections.Generic;


namespace BlueMine.Syndication.Atom
{


    [XmlRoot(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
    public class Title
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "rank", Namespace = "http://purl.org/atompub/rank/1.0")]
    public class Rank
    {
        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
    public class Category
    {
        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
        [XmlAttribute(AttributeName = "term")]
        public string Term { get; set; }
    }

    [XmlRoot(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
    public class Author
    {
        [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Name { get; set; }
        [XmlElement(ElementName = "uri", Namespace = "http://www.w3.org/2005/Atom")]
        public string Uri { get; set; }
    }

    [XmlRoot(ElementName = "summary", Namespace = "http://www.w3.org/2005/Atom")]
    public class Summary
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
    public class Entry
    {
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; } 

        [XmlElement(ElementName = "rank", Namespace = "http://purl.org/atompub/rank/1.0")]
        public Rank Rank { get; set; }

        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public Title Title { get; set; }

        [XmlElement(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Category> Category { get; set; }

        [XmlElement(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
        public Author Author { get; set; }

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public Link Link { get; set; }

        [XmlElement(ElementName = "published", Namespace = "http://www.w3.org/2005/Atom")]
        public string Published { get; set; }

        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }

        [XmlElement(ElementName = "summary", Namespace = "http://www.w3.org/2005/Atom")]
        public Summary Summary { get; set; }
    }

    [XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class Feed
    {
        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public Title Title { get; set; }

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Link { get; set; }

        [XmlElement(ElementName = "subtitle", Namespace = "http://www.w3.org/2005/Atom")]
        public string Subtitle { get; set; }

        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }

        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; }

        [XmlElement(ElementName = "license", Namespace = "http://backend.userland.com/creativeCommonsRssModule")]
        public string License { get; set; }

        [XmlElement(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Entry> Entry { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "creativeCommons", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string CreativeCommons { get; set; }

        [XmlAttribute(AttributeName = "re", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Re { get; set; }


        // feed xmlns = "http://www.w3.org/2005/Atom" 
        // xmlns:creativeCommons="http://backend.userland.com/creativeCommonsRssModule" 
        // xmlns:re="http://purl.org/atompub/rank/1.0"
    
    }


}
