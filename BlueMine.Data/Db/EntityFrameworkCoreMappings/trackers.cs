using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class trackers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInChlog { get; set; }
        public int? Position { get; set; }
        public bool? IsInRoadmap { get; set; }
        public int? FieldsBits { get; set; }
        public int? DefaultStatusId { get; set; }
    }
}
