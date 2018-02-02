
namespace BlueMine.Db 
{
    
    
    public partial class T_members
    {
         public long id; // int not null
         public long user_id; // int not null
         public long project_id; // int not null
         public System.DateTime? created_on; // datetime NULL
         public bool mail_notification; // bit not null
    }


}
