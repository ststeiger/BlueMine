using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class imports
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public string Filename { get; set; }
        public string Settings { get; set; }
        public int? TotalItems { get; set; }
        public bool Finished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
