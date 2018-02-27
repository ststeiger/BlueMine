using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class versions
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string WikiPageTitle { get; set; }
        public string Status { get; set; }
        public string Sharing { get; set; }
    }
}
