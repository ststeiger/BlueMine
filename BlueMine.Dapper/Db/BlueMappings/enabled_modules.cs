
namespace BlueMine.Db 
{
    
    
    public partial class T_enabled_modules
    {
         public int id { get; set; } // int not null
         public int? project_id { get; set; } // int NULL
         public string name { get; set; } // nvarchar(4000) not null
    }


}
