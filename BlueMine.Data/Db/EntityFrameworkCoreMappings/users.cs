using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Admin { get; set; }
        public int Status { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public string Language { get; set; }
        public int? AuthSourceId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Type { get; set; }
        public string IdentityUrl { get; set; }
        public string MailNotification { get; set; }
        public string Salt { get; set; }
        public bool MustChangePasswd { get; set; }
        public DateTime? PasswdChangedOn { get; set; }
    }
}
