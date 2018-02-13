using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class wiki_pages
    {
        public int Id { get; set; }
        public int WikiId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Protected { get; set; }
        public int? ParentId { get; set; }
    }
}
