using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class time_entries
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int? IssueId { get; set; }
        public double Hours { get; set; }
        public string Comments { get; set; }
        public int ActivityId { get; set; }
        public DateTime SpentOn { get; set; }
        public int Tyear { get; set; }
        public int Tmonth { get; set; }
        public int Tweek { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
