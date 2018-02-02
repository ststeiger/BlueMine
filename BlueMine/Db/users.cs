
namespace BlueMine.Db 
{
    
    
    public partial class T_users
    {
         public long id; // int not null
         public string login; // nvarchar(4000) not null
         public string hashed_password; // nvarchar(40) not null
         public string firstname; // nvarchar(30) not null
         public string lastname; // nvarchar(255) not null
         public bool admin; // bit not null
         public long status; // int not null
         public System.DateTime? last_login_on; // datetime NULL
         public string language; // nvarchar(5) NULL
         public long? auth_source_id; // int NULL
         public System.DateTime? created_on; // datetime NULL
         public System.DateTime? updated_on; // datetime NULL
         public string type; // nvarchar(4000) NULL
         public string identity_url; // nvarchar(4000) NULL
         public string mail_notification; // nvarchar(4000) not null
         public string salt; // nvarchar(64) NULL
         public bool must_change_passwd; // bit not null
         public System.DateTime? passwd_changed_on; // datetime NULL
    }


}
