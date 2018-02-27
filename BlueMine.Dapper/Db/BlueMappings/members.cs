
namespace BlueMine.Db 
{
    
    
    public partial class T_members
    {
         public int id { get; set; } // int not null
         public int user_id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public System.DateTime? created_on { get; set; } // datetime NULL
         public bool mail_notification { get; set; } // bit not null
    }


}
