
namespace BlueMine.Db 
{
    
    
    public partial class T_custom_field_enumerations
    {
         public int id { get; set; } // int not null
         public int custom_field_id { get; set; } // int not null
         public string name { get; set; } // nvarchar(4000) not null
         public bool active { get; set; } // bit not null
         public int position { get; set; } // int not null
    }


}
