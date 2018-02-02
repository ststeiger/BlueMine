
namespace BlueMine.Db 
{
    
    
    public partial class T_auth_sources
    {
         public long id; // int not null
         public string type; // nvarchar(30) not null
         public string name; // nvarchar(60) not null
         public string host; // nvarchar(60) NULL
         public long? port; // int NULL
         public string account; // nvarchar(4000) NULL
         public string account_password; // nvarchar(4000) NULL
         public string base_dn; // nvarchar(255) NULL
         public string attr_login; // nvarchar(30) NULL
         public string attr_firstname; // nvarchar(30) NULL
         public string attr_lastname; // nvarchar(30) NULL
         public string attr_mail; // nvarchar(30) NULL
         public bool onthefly_register; // bit not null
         public bool tls; // bit not null
         public string filter; // nvarchar(MAX) NULL
         public long? timeout; // int NULL
    }


}
