
namespace BlueMine.Db 
{
    
    
    public partial class T_boards
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public string name { get; set; } // nvarchar(4000) not null
         public string description { get; set; } // nvarchar(4000) NULL
         public int? position { get; set; } // int NULL
         public int topics_count { get; set; } // int not null
         public int messages_count { get; set; } // int not null
         public int? last_message_id { get; set; } // int NULL
         public int? parent_id { get; set; } // int NULL
    }


}
