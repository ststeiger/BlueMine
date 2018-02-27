
namespace BlueMine.Db 
{
    
    
    public partial class T_custom_values
    {
         public int id { get; set; } // int not null
         public string customized_type { get; set; } // nvarchar(30) not null
         public int customized_id { get; set; } // int not null
         public int custom_field_id { get; set; } // int not null
         public string value { get; set; } // nvarchar(MAX) NULL
    }


}
