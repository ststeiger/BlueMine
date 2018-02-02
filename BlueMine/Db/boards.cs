
namespace BlueMine.Db 
{
    
    
    public partial class T_boards
    {
         public long id; // int not null
         public long project_id; // int not null
         public string name; // nvarchar(4000) not null
         public string description; // nvarchar(4000) NULL
         public long? position; // int NULL
         public long topics_count; // int not null
         public long messages_count; // int not null
         public long? last_message_id; // int NULL
         public long? parent_id; // int NULL
    }


}
