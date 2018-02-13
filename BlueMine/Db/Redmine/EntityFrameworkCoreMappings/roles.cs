using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }
        public bool? Assignable { get; set; }
        public int Builtin { get; set; }
        public string Permissions { get; set; }
        public string IssuesVisibility { get; set; }
        public string UsersVisibility { get; set; }
        public string TimeEntriesVisibility { get; set; }
        public bool? AllRolesManaged { get; set; }
    }
}
