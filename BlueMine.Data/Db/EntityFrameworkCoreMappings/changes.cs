using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class changes
    {
        public int Id { get; set; }
        public int ChangesetId { get; set; }
        public string Action { get; set; }
        public string Path { get; set; }
        public string FromPath { get; set; }
        public string FromRevision { get; set; }
        public string Revision { get; set; }
        public string Branch { get; set; }
    }
}
