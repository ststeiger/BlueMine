
namespace BlueMine.Db 
{
    
    
    public partial class T_users
    {
         public int id { get; set; } // int not null
         public string login { get; set; } // nvarchar(4000) not null
         public string hashed_password { get; set; } // nvarchar(40) not null
         public string firstname { get; set; } // nvarchar(30) not null
         public string lastname { get; set; } // nvarchar(255) not null
         public bool admin { get; set; } // bit not null
         public int status { get; set; } // int not null
         public System.DateTime? last_login_on { get; set; } // datetime NULL
         public string language { get; set; } // nvarchar(5) NULL
         public int? auth_source_id { get; set; } // int NULL
         public System.DateTime? created_on { get; set; } // datetime NULL
         public System.DateTime? updated_on { get; set; } // datetime NULL
         public string type { get; set; } // nvarchar(4000) NULL
         public string identity_url { get; set; } // nvarchar(4000) NULL
         public string mail_notification { get; set; } // nvarchar(4000) not null
         public string salt { get; set; } // nvarchar(64) NULL
         public bool must_change_passwd { get; set; } // bit not null
         public System.DateTime? passwd_changed_on { get; set; } // datetime NULL
    }


}
