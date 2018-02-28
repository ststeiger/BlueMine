
namespace BlueMine.Db 
{
    
    
    public partial class T_member_roles
    {
         public int id { get; set; } // int not null
         public int member_id { get; set; } // int not null
         public int role_id { get; set; } // int not null
         public int? inherited_from { get; set; } // int NULL
    }


}
