using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class import_items
    {
        public int Id { get; set; }
        public int ImportId { get; set; }
        public int Position { get; set; }
        public int? ObjId { get; set; }
        public string Message { get; set; }
    }
}
