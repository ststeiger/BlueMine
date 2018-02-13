
namespace BlueMine.Db 
{
    
    
    public partial class T_email_addresses
    {
         public long id; // int not null
         public long user_id; // int not null
         public string address; // nvarchar(4000) not null
         public bool is_default; // bit not null
         public bool notify; // bit not null
         public System.DateTime created_on; // datetime not null
         public System.DateTime updated_on; // datetime not null
    }


}
