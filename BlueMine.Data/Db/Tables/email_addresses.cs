
namespace BlueMine.Db 
{
    
    
    public partial class T_email_addresses
    {
         public int id { get; set; } // int not null
         public int user_id { get; set; } // int not null
         public string address { get; set; } // nvarchar(4000) not null
         public bool is_default { get; set; } // bit not null
         public bool notify { get; set; } // bit not null
         public System.DateTime created_on { get; set; } // datetime not null
         public System.DateTime updated_on { get; set; } // datetime not null
    }


}
