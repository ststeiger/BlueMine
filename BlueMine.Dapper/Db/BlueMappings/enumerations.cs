
namespace BlueMine.Db 
{
    
    
    public partial class T_enumerations
    {
         public int id { get; set; } // int not null
         public string name { get; set; } // nvarchar(30) not null
         public int? position { get; set; } // int NULL
         public bool is_default { get; set; } // bit not null
         public string type { get; set; } // nvarchar(4000) NULL
         public bool active { get; set; } // bit not null
         public int? project_id { get; set; } // int NULL
         public int? parent_id { get; set; } // int NULL
         public string position_name { get; set; } // nvarchar(30) NULL
    }


}
