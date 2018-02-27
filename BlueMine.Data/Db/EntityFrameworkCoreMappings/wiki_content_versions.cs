using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class wiki_content_versions
    {
        public int Id { get; set; }
        public int WikiContentId { get; set; }
        public int PageId { get; set; }
        public int? AuthorId { get; set; }
        public byte[] Data { get; set; }
        public string Compression { get; set; }
        public string Comments { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int Version { get; set; }
    }
}
