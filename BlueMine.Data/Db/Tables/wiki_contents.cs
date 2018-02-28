
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_contents
    {
         public int id { get; set; } // int not null
         public int page_id { get; set; } // int not null
         public int? author_id { get; set; } // int NULL
         public string text { get; set; } // nvarchar(MAX) NULL
         public string comments { get; set; } // nvarchar(1024) NULL
         public System.DateTime updated_on { get; set; } // datetime not null
         public int version { get; set; } // int not null
    }


}
