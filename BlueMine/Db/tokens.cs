
namespace BlueMine.Db 
{
    
    
    public partial class T_tokens
    {
         public long id; // int not null
         public long user_id; // int not null
         public string action; // nvarchar(30) not null
         public string value; // nvarchar(40) not null
         public System.DateTime created_on; // datetime not null
         public System.DateTime? updated_on; // datetime NULL
    }


}
