using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class boards
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Position { get; set; }
        public int TopicsCount { get; set; }
        public int MessagesCount { get; set; }
        public int? LastMessageId { get; set; }
        public int? ParentId { get; set; }
    }
}
