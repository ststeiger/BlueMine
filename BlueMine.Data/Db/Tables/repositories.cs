
namespace BlueMine.Db 
{
    
    
    public partial class T_repositories
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public string url { get; set; } // nvarchar(4000) not null
         public string login { get; set; } // nvarchar(60) NULL
         public string password { get; set; } // nvarchar(4000) NULL
         public string root_url { get; set; } // nvarchar(255) NULL
         public string type { get; set; } // nvarchar(4000) NULL
         public string path_encoding { get; set; } // nvarchar(64) NULL
         public string log_encoding { get; set; } // nvarchar(64) NULL
         public string extra_info { get; set; } // nvarchar(MAX) NULL
         public string identifier { get; set; } // nvarchar(4000) NULL
         public bool? is_default { get; set; } // bit NULL
         public System.DateTime? created_on { get; set; } // datetime NULL
    }


}
