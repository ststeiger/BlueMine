
namespace BlueMine.Db 
{
    
    
    public partial class T_changes
    {
         public long id; // int not null
         public long changeset_id; // int not null
         public string action; // nvarchar(1) not null
         public string path; // nvarchar(MAX) not null
         public string from_path; // nvarchar(MAX) NULL
         public string from_revision; // nvarchar(4000) NULL
         public string revision; // nvarchar(4000) NULL
         public string branch; // nvarchar(4000) NULL
    }


}
