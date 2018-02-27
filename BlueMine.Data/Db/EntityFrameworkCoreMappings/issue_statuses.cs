using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class issue_statuses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsClosed { get; set; }
        public int? Position { get; set; }
        public int? DefaultDoneRatio { get; set; }
    }
}
