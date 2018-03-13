// using System.Collections.Generic;

using System.Xml.Linq;


namespace WilderMinds.RssSyndication
{
    public class Feed
    {
        public string Description { get; set; }
        public System.Uri Link { get; set; }
        public string Title { get; set; }
        public string Copyright { get; set; }
        public System.Collections.Generic.List<Item> Items { get; set; }


        public Feed()
        {
            Items = new System.Collections.Generic.List<Item>();
        }


        public string Serialize()
        {
            XDocument doc = new XDocument(new XElement("rss"));
            doc.Root.Add(new XAttribute("version", "2.0"));
            
            XElement channel = new XElement("channel");
            channel.Add(new XElement("title", this.Title));
            channel.Add(new XElement("link", this.Link.AbsoluteUri));
            channel.Add(new XElement("description", this.Description));
            channel.Add(new XElement("copyright", this.Copyright));
            doc.Root.Add(channel);
            
            foreach (var item in Items)
            {
                XElement itemElement = new XElement("item");
                itemElement.Add(new XElement("title", item.Title));
                itemElement.Add(new XElement("link", item.Link.AbsoluteUri));
                itemElement.Add(new XElement("description", item.Body));
                
                if (item.Author != null)
                    itemElement.Add(new XElement("author", $"{item.Author.Email} ({item.Author.Name})"));
                
                foreach (string c in item.Categories)
                    itemElement.Add(new XElement("category", c));
                
                if (item.Comments != null)
                    itemElement.Add(new XElement("comments", item.Comments.AbsoluteUri));
                
                if (!string.IsNullOrWhiteSpace(item.Permalink))
                    itemElement.Add(new XElement("guid", item.Permalink));
                
                string dateFmt = item.PublishDate.ToString("r");
                
                if (item.PublishDate != System.DateTime.MinValue)
                    itemElement.Add(new XElement("pubDate", dateFmt));
                
                channel.Add(itemElement);
            }
            
            return doc.ToString();
        }
        
        
    }
    
    
}
