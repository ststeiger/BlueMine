using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class watchers
    {
        public int Id { get; set; }
        public string WatchableType { get; set; }
        public int WatchableId { get; set; }
        public int? UserId { get; set; }
    }
}
