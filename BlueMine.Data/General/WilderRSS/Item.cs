
using System.Collections.Generic;

namespace WilderMinds.RssSyndication
{
    public class Item
    {
        public Author Author { get; set; }
        public string Body { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public System.Uri Comments { get; set; }
        public System.Uri Link { get; set; }
        public string Permalink { get; set; }
        public System.DateTime PublishDate { get; set; }
        public string Title { get; set; }
    }
}
