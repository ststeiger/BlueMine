using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class wikis
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string StartPage { get; set; }
        public int Status { get; set; }
    }
}
