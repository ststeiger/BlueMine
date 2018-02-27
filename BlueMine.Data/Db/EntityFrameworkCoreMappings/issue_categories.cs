using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class issue_categories
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int? AssignedToId { get; set; }
    }
}
