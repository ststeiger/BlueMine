
namespace BlueMine.Db 
{
    
    
    public partial class T_open_id_authentication_nonces
    {
         public long id; // int not null
         public long timestamp; // int not null
         public string server_url; // nvarchar(4000) NULL
         public string salt; // nvarchar(4000) not null
    }


}
