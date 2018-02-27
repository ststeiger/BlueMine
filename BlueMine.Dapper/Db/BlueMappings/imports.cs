
namespace BlueMine.Db 
{
    
    
    public partial class T_imports
    {
         public int id { get; set; } // int not null
         public string type { get; set; } // nvarchar(4000) NULL
         public int user_id { get; set; } // int not null
         public string filename { get; set; } // nvarchar(4000) NULL
         public string settings { get; set; } // nvarchar(MAX) NULL
         public int? total_items { get; set; } // int NULL
         public bool finished { get; set; } // bit not null
         public System.DateTime created_at { get; set; } // datetime not null
         public System.DateTime updated_at { get; set; } // datetime not null
    }


}
