
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_pages
    {
         public int id { get; set; } // int not null
         public int wiki_id { get; set; } // int not null
         public string title { get; set; } // nvarchar(255) not null
         public System.DateTime created_on { get; set; } // datetime not null
         public bool @protected { get; set; } // bit not null
         public int? parent_id { get; set; } // int NULL
    }


}
