
namespace BlueMine.Db 
{
    
    
    public partial class T_projects
    {
         public long id; // int not null
         public string name; // nvarchar(4000) not null
         public string description; // nvarchar(MAX) NULL
         public string homepage; // nvarchar(4000) NULL
         public bool is_public; // bit not null
         public long? parent_id; // int NULL
         public System.DateTime? created_on; // datetime NULL
         public System.DateTime? updated_on; // datetime NULL
         public string identifier; // nvarchar(4000) NULL
         public long status; // int not null
         public long? lft; // int NULL
         public long? rgt; // int NULL
         public bool inherit_members; // bit not null
         public long? default_version_id; // int NULL
    }


}
