
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_redirects
    {
         public int id { get; set; } // int not null
         public int wiki_id { get; set; } // int not null
         public string title { get; set; } // nvarchar(4000) NULL
         public string redirects_to { get; set; } // nvarchar(4000) NULL
         public System.DateTime created_on { get; set; } // datetime not null
         public int redirects_to_wiki_id { get; set; } // int not null
    }


}
