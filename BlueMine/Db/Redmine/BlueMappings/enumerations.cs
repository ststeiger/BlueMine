
namespace BlueMine.Db 
{
    
    
    public partial class T_enumerations
    {
         public long id; // int not null
         public string name; // nvarchar(30) not null
         public long? position; // int NULL
         public bool is_default; // bit not null
         public string type; // nvarchar(4000) NULL
         public bool active; // bit not null
         public long? project_id; // int NULL
         public long? parent_id; // int NULL
         public string position_name; // nvarchar(30) NULL
    }


}
