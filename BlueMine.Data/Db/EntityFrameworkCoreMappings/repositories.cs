using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class repositories
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RootUrl { get; set; }
        public string Type { get; set; }
        public string PathEncoding { get; set; }
        public string LogEncoding { get; set; }
        public string ExtraInfo { get; set; }
        public string Identifier { get; set; }
        public bool? IsDefault { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
