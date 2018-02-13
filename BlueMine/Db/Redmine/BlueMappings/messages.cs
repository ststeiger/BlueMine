
namespace BlueMine.Db 
{
    
    
    public partial class T_messages
    {
         public long id; // int not null
         public long board_id; // int not null
         public long? parent_id; // int NULL
         public string subject; // nvarchar(4000) not null
         public string content; // nvarchar(MAX) NULL
         public long? author_id; // int NULL
         public long replies_count; // int not null
         public long? last_reply_id; // int NULL
         public System.DateTime created_on; // datetime not null
         public System.DateTime updated_on; // datetime not null
         public bool? locked; // bit NULL
         public long? sticky; // int NULL
    }


}
