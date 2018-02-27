
namespace BlueMine.Db 
{
    
    
    public partial class T_auth_sources
    {
         public int id { get; set; } // int not null
         public string type { get; set; } // nvarchar(30) not null
         public string name { get; set; } // nvarchar(60) not null
         public string host { get; set; } // nvarchar(60) NULL
         public int? port { get; set; } // int NULL
         public string account { get; set; } // nvarchar(4000) NULL
         public string account_password { get; set; } // nvarchar(4000) NULL
         public string base_dn { get; set; } // nvarchar(255) NULL
         public string attr_login { get; set; } // nvarchar(30) NULL
         public string attr_firstname { get; set; } // nvarchar(30) NULL
         public string attr_lastname { get; set; } // nvarchar(30) NULL
         public string attr_mail { get; set; } // nvarchar(30) NULL
         public bool onthefly_register { get; set; } // bit not null
         public bool tls { get; set; } // bit not null
         public string filter { get; set; } // nvarchar(MAX) NULL
         public int? timeout { get; set; } // int NULL
    }


}
