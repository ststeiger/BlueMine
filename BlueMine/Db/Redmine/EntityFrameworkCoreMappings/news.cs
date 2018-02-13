using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class news
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CommentsCount { get; set; }
    }
}
