// Licensed under the Apache License, Version 2.0
// http://www.apache.org/licenses/LICENSE-2.0


using System.Xml.Serialization;


// Atom 1.0
// Atom is a relatively recent spec and is much more robust and feature-rich than RSS.
// https://www.saksoft.com/rss-vs-atom/
// http://www.intertwingly.net/wiki/pie/Rss20AndAtom10Compared
// https://problogger.com/rss-vs-atom-whats-the-big-deal/
// http://nullprogram.com/blog/2013/09/23/
// https://www.ibm.com/developerworks/library/x-atom10/index.html
// https://www.cambiaresearch.com/articles/71/easily-build-an-atom-or-rss-feed-with-csharp-and-the-syndication-namespace
// https://rehansaeed.com/building-rssatom-feeds-for-asp-net-mvc/



// https://www.nasa.gov/content/nasa-rss-feeds
// https://github.com/joeaudette/cloudscribe.Syndication/blob/master/src/cloudscribe.Syndication/Models/Rss/RssChannel.cs
namespace Xml2CSharp
{

    [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }

        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
    }


    [XmlRoot(ElementName = "cloud")]
    public class Cloud
    {
        [XmlAttribute(AttributeName = "domain")]
        public string Domain { get; set; }

        [XmlAttribute(AttributeName = "port")]
        public string Port { get; set; }

        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [XmlAttribute(AttributeName = "registerProcedure")]
        public string RegisterProcedure { get; set; }

        [XmlAttribute(AttributeName = "protocol")]
        public string Protocol { get; set; }
    }


    [XmlRoot(ElementName = "image")]
    public class Image
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link2 { get; set; }
    }

    [XmlRoot(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
    public class Thumbnail
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
    }


    [XmlRoot(ElementName = "category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    public class Category
    {
        [XmlAttribute(AttributeName = "text")]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    public class Image2
    {
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
    }

    [XmlRoot(ElementName = "owner", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    public class Owner
    {
        [XmlElement(ElementName = "email", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Email { get; set; }

        [XmlElement(ElementName = "name", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "enclosure")]
    public class Enclosure
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "guid")]
    public class Guid
    {
        [XmlAttribute(AttributeName = "isPermaLink")]
        public string IsPermaLink { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "source")]
    public class Source
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "title", Namespace = "http://search.yahoo.com/mrss/")]
    public class Title
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "content", Namespace = "http://search.yahoo.com/mrss/")]
    public class Content
    {
        [XmlElement(ElementName = "title", Namespace = "http://search.yahoo.com/mrss/")]
        public Title Title { get; set; }

        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        [XmlAttribute(AttributeName = "medium")]
        public string Medium { get; set; }

        [XmlAttribute(AttributeName = "fileSize")]
        public string FileSize { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "bitrate")]
        public string Bitrate { get; set; }

        [XmlAttribute(AttributeName = "isDefault")]
        public string IsDefault { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }

        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
    }


    [XmlRoot(ElementName = "rank", Namespace = "http://purl.org/atompub/rank/1.0")]
    public class Rank
    {
        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
        [XmlText]
        public string Text { get; set; }
    }


    [XmlRoot(ElementName = "rating", Namespace = "http://search.yahoo.com/mrss/")]
    public class Rating
    {
        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "player", Namespace = "http://search.yahoo.com/mrss/")]
    public class Player
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
    }

    [XmlRoot(ElementName = "group", Namespace = "http://search.yahoo.com/mrss/")]
    public class Group
    {
        [XmlElement(ElementName = "content", Namespace = "http://search.yahoo.com/mrss/")]
        public System.Collections.Generic.List<Content> Content { get; set; }

        [XmlElement(ElementName = "rating", Namespace = "http://search.yahoo.com/mrss/")]
        public Rating Rating { get; set; }

        [XmlElement(ElementName = "title", Namespace = "http://search.yahoo.com/mrss/")]
        public Title Title { get; set; }

        [XmlElement(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
        public Thumbnail Thumbnail { get; set; }

        [XmlElement(ElementName = "player", Namespace = "http://search.yahoo.com/mrss/")]
        public Player Player { get; set; }
    }


    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [XmlAttribute(AttributeName = "id", Namespace = "https://www.news.admin.ch/rss")]
        public string Id { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link2 { get; set; }

        [XmlElement(ElementName = "comments")]
        public System.Collections.Generic.List<string> Comments { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "enclosure")]
        public Enclosure Enclosure { get; set; }

        [XmlElement(ElementName = "guid")]
        public Guid Guid { get; set; }

        [XmlElement(ElementName = "pubDate")]
        //public System.DateTime? PubDate { get; set; }
        // public string PubDate { get; set; }
        public BlueMine.Data.General.RSS.CustomDate PubDate { get; set; }

        [XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Creator { get; set; }


        [XmlElement(ElementName = "category")]
        public System.Collections.Generic.List<string> Category { get; set; }

        [XmlElement(ElementName = "source")]
        public Source Source { get; set; }

        [XmlElement(ElementName = "identifier", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Identifier { get; set; }


        [XmlElement(ElementName = "encoded", Namespace = "http://purl.org/rss/1.0/modules/content/")]
        public string Encoded { get; set; }

        [XmlElement(ElementName = "commentRss", Namespace = "http://wellformedweb.org/CommentAPI/")]
        public string CommentRss { get; set; }

        [XmlElement(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
        public Thumbnail Thumbnail { get; set; }

        [XmlElement(ElementName = "content", Namespace = "http://search.yahoo.com/mrss/")]
        public System.Collections.Generic.List<Content> Content { get; set; }

        [XmlElement(ElementName = "group", Namespace = "http://search.yahoo.com/mrss/")]
        public Group Group { get; set; }
    }


    [XmlRoot(ElementName = "channel")]
    public class Channel
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public System.Collections.Generic.List<string> Link { get; set; }

        [XmlElement(ElementName = "link")]
        public System.Collections.Generic.List<string> Link2 { get; set; }

        [XmlElement(ElementName = "lastBuildDate")]
        public string LastBuildDate { get; set; }


        [XmlElement(ElementName = "language")]
        public string Language { get; set; }

        [XmlElement(ElementName = "updatePeriod", Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
        public string UpdatePeriod { get; set; }

        [XmlElement(ElementName = "updateFrequency", Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
        public string UpdateFrequency { get; set; }

        [XmlElement(ElementName = "generator")]
        public string Generator { get; set; }

        [XmlElement(ElementName = "cloud")]
        public Cloud Cloud { get; set; }

        [XmlElement(ElementName = "image")]
        public Image Image { get; set; }

        [XmlElement(ElementName = "copyright")]
        public string Copyright { get; set; }

        [XmlElement(ElementName = "managingEditor")]
        public string ManagingEditor { get; set; }

        [XmlElement(ElementName = "webMaster")]
        public string WebMaster { get; set; }


        // https://www.nasa.gov/rss/dyn/nasax_vodcast.rss
        [XmlElement(ElementName = "subtitle", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Subtitle { get; set; }

        [XmlElement(ElementName = "summary", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Summary { get; set; }

        [XmlElement(ElementName = "category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public Category Category { get; set; }

        [XmlElement(ElementName = "keywords", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Keywords { get; set; }

        [XmlElement(ElementName = "image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public Image2 Image2 { get; set; }

        [XmlElement(ElementName = "author", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Author { get; set; }

        [XmlElement(ElementName = "explicit", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Explicit { get; set; }

        [XmlElement(ElementName = "owner", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public Owner Owner { get; set; }



        // https://www.nasa.gov/rss/dyn/aeronautics.rss
        [XmlElement(ElementName = "docs")]
        public string Docs { get; set; }

        [XmlElement(ElementName = "item")]
        public System.Collections.Generic.List<Item> Items { get; set; }
            = new System.Collections.Generic.List<Item>();
    }


    //[XmlRoot(ElementName = "rss", Namespace = "urn:Abracadabra", IsNullable = true)]
    [XmlRoot(ElementName = "rss")]
    public class Rss 
        : Tools.XML.SerializableXml
    {


        public Rss()
        {
            this._namespaces = new XmlSerializerNamespaces(new System.Xml.XmlQualifiedName[] {
                // Don't do this!! Microsoft's documentation explicitly says it's not supported.
                // It doesn't throw any exceptions, but in my testing, it didn't always work.

                // new System.Xml.XmlQualifiedName(string.Empty, string.Empty),  // And don't do this:

                // DO THIS:
                // new System.Xml.XmlQualifiedName(string.Empty, "urn:Abracadabra"), // Default Namespace
                // Add any other namespaces, with prefixes, here.
                
                // new System.Xml.XmlQualifiedName("base", "http://www.nasa.gov/"), 
                // new System.Xml.XmlQualifiedName("base", "http://www.w3.org/XML/1998/namespace"), 
                new System.Xml.XmlQualifiedName("base", "https://www.nasa.gov/"), 

                // <feed xmlns="http://www.w3.org/2005/Atom" 
                new System.Xml.XmlQualifiedName("atom", "http://www.w3.org/2005/Atom"), 
                new System.Xml.XmlQualifiedName("dc", "http://purl.org/dc/elements/1.1/"), 
                new System.Xml.XmlQualifiedName("itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd"), 
                new System.Xml.XmlQualifiedName("media", "http://search.yahoo.com/mrss/"), 
                
                new System.Xml.XmlQualifiedName("wfw", "http://wellformedweb.org/CommentAPI/"), 
                new System.Xml.XmlQualifiedName("sy", "http://purl.org/rss/1.0/modules/syndication/"), 
                new System.Xml.XmlQualifiedName("slash", "http://purl.org/rss/1.0/modules/slash/"), 
                new System.Xml.XmlQualifiedName("content", "http://purl.org/rss/1.0/modules/content/"), 

                new System.Xml.XmlQualifiedName("creativeCommons", "http://backend.userland.com/creativeCommonsRssModule"), 
                new System.Xml.XmlQualifiedName("re", "http://purl.org/atompub/rank/1.0"), 

                new System.Xml.XmlQualifiedName("georss", "http://www.georss.org/georss"), 
                new System.Xml.XmlQualifiedName("geo", "http://www.w3.org/2003/01/geo/wgs84_pos#"), 
                new System.Xml.XmlQualifiedName("nsb", "https://www.news.admin.ch/rss") 
            });
        }
        
        
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }


        [XmlAttribute(AttributeName = "base", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Base { get; set; }

        [XmlAttribute(AttributeName = "atom", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Atom { get; set; }

        [XmlAttribute(AttributeName = "dc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Dc { get; set; }

        [XmlAttribute(AttributeName = "itunes", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Itunes { get; set; }

        [XmlAttribute(AttributeName = "media", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Media { get; set; }

        [XmlAttribute(AttributeName = "content", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Content { get; set; }

        [XmlAttribute(AttributeName = "wfw", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wfw { get; set; }

        [XmlAttribute(AttributeName = "sy", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sy { get; set; }

        [XmlAttribute(AttributeName = "slash", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Slash { get; set; }

        [XmlAttribute(AttributeName = "georss", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Georss { get; set; }

        [XmlAttribute(AttributeName = "geo", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Geo { get; set; }

        // https://www.news.admin.ch/rss
        [XmlAttribute(AttributeName = "nsb", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Nsb { get; set; }


        // https://en.wikipedia.org/wiki/Atom_(Web_standard)
        // https://en.wikipedia.org/wiki/RSS
        // https://nobillag.ch/feed/
        // https://en.blog.wordpress.com/feed/
        // https://www.newsd.admin.ch/newsd/feeds/rss?lang=en&org-nr=1&topic=&keyword=&offer-nr=&catalogueElement=&kind=M,R&start_date=2015-01-01&end_date=
        // https://stackoverflow.com/feeds/tag/+or+c+or+opengl+or+graphics+or+latex+or+android+or+wmii+or+glut+or+opencv+or+3d+or+3dgraphics+or+beamer+or+wiimote+or+beginner
        // xmlns:content="http://purl.org/rss/1.0/modules/content/"
        // xmlns:wfw="http://wellformedweb.org/CommentAPI/"
        // xmlns:dc="http://purl.org/dc/elements/1.1/"
        // xmlns:atom="http://www.w3.org/2005/Atom"
        // xmlns:sy="http://purl.org/rss/1.0/modules/syndication/"
        // xmlns:slash="http://purl.org/rss/1.0/modules/slash/"

    }




}
