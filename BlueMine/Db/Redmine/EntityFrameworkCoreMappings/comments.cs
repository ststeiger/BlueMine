using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class comments
    {
        public int Id { get; set; }
        public string CommentedType { get; set; }
        public int CommentedId { get; set; }
        public int AuthorId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
