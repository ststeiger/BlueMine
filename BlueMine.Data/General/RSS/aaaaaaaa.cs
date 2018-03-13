
// Licensed under the Apache License, Version 2.0
// http://www.apache.org/licenses/LICENSE-2.0


using System.Xml.Serialization;
using System.Collections.Generic;

// https://stackoverflow.com/feeds/tag?tagnames=tags&sort=newest 
namespace Xml2CSharp.Atom
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
    }

}
