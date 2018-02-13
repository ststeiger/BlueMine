
namespace BlueMine.Db 
{
    
    
    public partial class T_journals
    {
         public long id; // int not null
         public long journalized_id; // int not null
         public string journalized_type; // nvarchar(30) not null
         public long user_id; // int not null
         public string notes; // nvarchar(MAX) NULL
         public System.DateTime created_on; // datetime not null
         public bool private_notes; // bit not null
    }


}
