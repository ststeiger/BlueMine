
namespace BlueMine.Db 
{
    
    
    public partial class T_versions
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public string name { get; set; } // nvarchar(4000) NULL
         public string description { get; set; } // nvarchar(4000) NULL
         public System.DateTime? effective_date { get; set; } // date NULL
         public System.DateTime? created_on { get; set; } // datetime NULL
         public System.DateTime? updated_on { get; set; } // datetime NULL
         public string wiki_page_title { get; set; } // nvarchar(4000) NULL
         public string status { get; set; } // nvarchar(4000) NULL
         public string sharing { get; set; } // nvarchar(4000) not null
    }


}
