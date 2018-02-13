using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class journals
    {
        public int Id { get; set; }
        public int JournalizedId { get; set; }
        public string JournalizedType { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool PrivateNotes { get; set; }
    }
}
