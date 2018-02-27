using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class workflows
    {
        public int Id { get; set; }
        public int TrackerId { get; set; }
        public int OldStatusId { get; set; }
        public int NewStatusId { get; set; }
        public int RoleId { get; set; }
        public bool Assignee { get; set; }
        public bool Author { get; set; }
        public string Type { get; set; }
        public string FieldName { get; set; }
        public string Rule { get; set; }
    }
}
