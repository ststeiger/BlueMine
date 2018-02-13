using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class issues
    {
        public int Id { get; set; }
        public int TrackerId { get; set; }
        public int ProjectId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CategoryId { get; set; }
        public int StatusId { get; set; }
        public int? AssignedToId { get; set; }
        public int PriorityId { get; set; }
        public int? FixedVersionId { get; set; }
        public int AuthorId { get; set; }
        public int LockVersion { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? StartDate { get; set; }
        public int DoneRatio { get; set; }
        public double? EstimatedHours { get; set; }
        public int? ParentId { get; set; }
        public int? RootId { get; set; }
        public int? Lft { get; set; }
        public int? Rgt { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime? ClosedOn { get; set; }
    }
}
