
namespace BlueMine.Db 
{
    
    
    public partial class T_import_items
    {
         public int id { get; set; } // int not null
         public int import_id { get; set; } // int not null
         public int position { get; set; } // int not null
         public int? obj_id { get; set; } // int NULL
         public string message { get; set; } // nvarchar(MAX) NULL
    }


}
