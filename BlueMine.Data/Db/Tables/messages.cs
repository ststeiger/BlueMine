
namespace BlueMine.Db 
{
    
    
    public partial class T_messages
    {
         public int id { get; set; } // int not null
         public int board_id { get; set; } // int not null
         public int? parent_id { get; set; } // int NULL
         public string subject { get; set; } // nvarchar(4000) not null
         public string content { get; set; } // nvarchar(MAX) NULL
         public int? author_id { get; set; } // int NULL
         public int replies_count { get; set; } // int not null
         public int? last_reply_id { get; set; } // int NULL
         public System.DateTime created_on { get; set; } // datetime not null
         public System.DateTime updated_on { get; set; } // datetime not null
         public bool? locked { get; set; } // bit NULL
         public int? sticky { get; set; } // int NULL
    }


}
