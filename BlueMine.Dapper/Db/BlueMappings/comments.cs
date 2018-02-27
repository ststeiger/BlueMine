
namespace BlueMine.Db 
{
    
    
    public partial class T_comments
    {
         public int id { get; set; } // int not null
         public string commented_type { get; set; } // nvarchar(30) not null
         public int commented_id { get; set; } // int not null
         public int author_id { get; set; } // int not null
         public string comments { get; set; } // nvarchar(MAX) NULL
         public System.DateTime created_on { get; set; } // datetime not null
         public System.DateTime updated_on { get; set; } // datetime not null
    }


}
