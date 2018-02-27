using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class tokens
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
