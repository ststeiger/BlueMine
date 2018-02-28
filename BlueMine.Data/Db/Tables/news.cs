
namespace BlueMine.Db 
{
    
    
    public partial class T_news
    {
         public int id { get; set; } // int not null
         public int? project_id { get; set; } // int NULL
         public string title { get; set; } // nvarchar(60) not null
         public string summary { get; set; } // nvarchar(255) NULL
         public string description { get; set; } // nvarchar(MAX) NULL
         public int author_id { get; set; } // int not null
         public System.DateTime? created_on { get; set; } // datetime NULL
         public int comments_count { get; set; } // int not null
    }


}
