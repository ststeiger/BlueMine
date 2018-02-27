using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class documents
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
