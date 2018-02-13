using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class member_roles
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public int? InheritedFrom { get; set; }
    }
}
