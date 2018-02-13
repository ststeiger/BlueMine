using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class issue_relations
    {
        public int Id { get; set; }
        public int IssueFromId { get; set; }
        public int IssueToId { get; set; }
        public string RelationType { get; set; }
        public int? Delay { get; set; }
    }
}
