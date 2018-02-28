
namespace BlueMine.Db 
{
    
    
    public partial class T_open_id_authentication_nonces
    {
         public int id { get; set; } // int not null
         public int timestamp { get; set; } // int not null
         public string server_url { get; set; } // nvarchar(4000) NULL
         public string salt { get; set; } // nvarchar(4000) not null
    }


}
