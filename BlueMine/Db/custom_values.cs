
namespace BlueMine.Db 
{
    
    
    public partial class T_custom_values
    {
         public long id; // int not null
         public string customized_type; // nvarchar(30) not null
         public long customized_id; // int not null
         public long custom_field_id; // int not null
         public string value; // nvarchar(MAX) NULL
    }


}
