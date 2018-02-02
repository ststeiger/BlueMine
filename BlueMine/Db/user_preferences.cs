
namespace BlueMine.Db 
{
    
    
    public partial class T_user_preferences
    {
         public long id; // int not null
         public long user_id; // int not null
         public string others; // nvarchar(MAX) NULL
         public bool? hide_mail; // bit NULL
         public string time_zone; // nvarchar(4000) NULL
    }


}
