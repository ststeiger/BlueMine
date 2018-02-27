
namespace BlueMine.Db 
{
    
    
    public partial class T_documents
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public int category_id { get; set; } // int not null
         public string title { get; set; } // nvarchar(4000) not null
         public string description { get; set; } // nvarchar(MAX) NULL
         public System.DateTime? created_on { get; set; } // datetime NULL
    }


}
