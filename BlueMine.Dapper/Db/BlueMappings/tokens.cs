
namespace BlueMine.Db 
{
    
    
    public partial class T_tokens
    {
         public int id { get; set; } // int not null
         public int user_id { get; set; } // int not null
         public string action { get; set; } // nvarchar(30) not null
         public string value { get; set; } // nvarchar(40) not null
         public System.DateTime created_on { get; set; } // datetime not null
         public System.DateTime? updated_on { get; set; } // datetime NULL
    }


}
