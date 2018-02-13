
namespace BlueMine.Db 
{
    
    
    public partial class T_custom_field_enumerations
    {
         public long id; // int not null
         public long custom_field_id; // int not null
         public string name; // nvarchar(4000) not null
         public bool active; // bit not null
         public long position; // int not null
    }


}
