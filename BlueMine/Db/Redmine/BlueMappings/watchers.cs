
namespace BlueMine.Db 
{
    
    
    public partial class T_watchers
    {
         public long id; // int not null
         public string watchable_type; // nvarchar(4000) not null
         public long watchable_id; // int not null
         public long? user_id; // int NULL
    }


}
