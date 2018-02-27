using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class journal_details
    {
        public int Id { get; set; }
        public int JournalId { get; set; }
        public string Property { get; set; }
        public string PropKey { get; set; }
        public string OldValue { get; set; }
        public string Value { get; set; }
    }
}
