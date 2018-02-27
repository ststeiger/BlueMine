
namespace BlueMine.Db 
{
    
    
    public partial class T_journals
    {
         public int id { get; set; } // int not null
         public int journalized_id { get; set; } // int not null
         public string journalized_type { get; set; } // nvarchar(30) not null
         public int user_id { get; set; } // int not null
         public string notes { get; set; } // nvarchar(MAX) NULL
         public System.DateTime created_on { get; set; } // datetime not null
         public bool private_notes { get; set; } // bit not null
    }


}
