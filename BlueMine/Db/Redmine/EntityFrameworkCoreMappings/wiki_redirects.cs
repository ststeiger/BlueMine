using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class wiki_redirects
    {
        public int Id { get; set; }
        public int WikiId { get; set; }
        public string Title { get; set; }
        public string RedirectsTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int RedirectsToWikiId { get; set; }
    }
}
