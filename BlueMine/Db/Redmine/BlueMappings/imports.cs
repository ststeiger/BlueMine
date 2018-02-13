
namespace BlueMine.Db 
{
    
    
    public partial class T_imports
    {
         public long id; // int not null
         public string type; // nvarchar(4000) NULL
         public long user_id; // int not null
         public string filename; // nvarchar(4000) NULL
         public string settings; // nvarchar(MAX) NULL
         public long? total_items; // int NULL
         public bool finished; // bit not null
         public System.DateTime created_at; // datetime not null
         public System.DateTime updated_at; // datetime not null
    }


}
