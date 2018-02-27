
namespace BlueMine.Db 
{
    
    
    public partial class T_user_preferences
    {
         public int id { get; set; } // int not null
         public int user_id { get; set; } // int not null
         public string others { get; set; } // nvarchar(MAX) NULL
         public bool? hide_mail { get; set; } // bit NULL
         public string time_zone { get; set; } // nvarchar(4000) NULL
    }


}
