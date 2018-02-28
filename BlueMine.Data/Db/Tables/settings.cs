
namespace BlueMine.Db 
{
    
    
    public partial class T_settings
    {
         public int id { get; set; } // int not null
         public string name { get; set; } // nvarchar(255) not null
         public string value { get; set; } // nvarchar(MAX) NULL
         public System.DateTime? updated_on { get; set; } // datetime NULL
    }


}
