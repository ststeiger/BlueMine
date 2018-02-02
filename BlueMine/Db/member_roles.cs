
namespace BlueMine.Db 
{
    
    
    public partial class T_member_roles
    {
         public long id; // int not null
         public long member_id; // int not null
         public long role_id; // int not null
         public long? inherited_from; // int NULL
    }


}
