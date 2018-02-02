
namespace BlueMine.Db 
{
    
    
    public partial class T_repositories
    {
         public long id; // int not null
         public long project_id; // int not null
         public string url; // nvarchar(4000) not null
         public string login; // nvarchar(60) NULL
         public string password; // nvarchar(4000) NULL
         public string root_url; // nvarchar(255) NULL
         public string type; // nvarchar(4000) NULL
         public string path_encoding; // nvarchar(64) NULL
         public string log_encoding; // nvarchar(64) NULL
         public string extra_info; // nvarchar(MAX) NULL
         public string identifier; // nvarchar(4000) NULL
         public bool? is_default; // bit NULL
         public System.DateTime? created_on; // datetime NULL
    }


}
