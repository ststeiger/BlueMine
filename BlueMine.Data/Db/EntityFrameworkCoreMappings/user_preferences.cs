using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class user_preferences
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Others { get; set; }
        public bool? HideMail { get; set; }
        public string TimeZone { get; set; }
    }
}
