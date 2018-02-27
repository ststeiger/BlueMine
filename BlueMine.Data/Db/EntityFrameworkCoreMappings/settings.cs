using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class settings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
