
namespace BlueMine.Db 
{
    
    
    public partial class T_custom_fields
    {
         public int id { get; set; } // int not null
         public string type { get; set; } // nvarchar(30) not null
         public string name { get; set; } // nvarchar(30) not null
         public string field_format { get; set; } // nvarchar(30) not null
         public string possible_values { get; set; } // nvarchar(MAX) NULL
         public string regexp { get; set; } // nvarchar(4000) NULL
         public int? min_length { get; set; } // int NULL
         public int? max_length { get; set; } // int NULL
         public bool is_required { get; set; } // bit not null
         public bool is_for_all { get; set; } // bit not null
         public bool is_filter { get; set; } // bit not null
         public int? position { get; set; } // int NULL
         public bool? searchable { get; set; } // bit NULL
         public string default_value { get; set; } // nvarchar(MAX) NULL
         public bool? editable { get; set; } // bit NULL
         public bool visible { get; set; } // bit not null
         public bool? multiple { get; set; } // bit NULL
         public string format_store { get; set; } // nvarchar(MAX) NULL
         public string description { get; set; } // nvarchar(MAX) NULL
    }


}
