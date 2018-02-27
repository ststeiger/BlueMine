using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class enabled_modules
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
    }
}
