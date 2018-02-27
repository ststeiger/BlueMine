using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public bool? IsPublic { get; set; }
        public int? ParentId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Identifier { get; set; }
        public int Status { get; set; }
        public int? Lft { get; set; }
        public int? Rgt { get; set; }
        public bool InheritMembers { get; set; }
        public int? DefaultVersionId { get; set; }
    }
}
