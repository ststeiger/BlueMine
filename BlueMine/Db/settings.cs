
namespace BlueMine.Db 
{
    
    
    public partial class T_settings
    {
         public long id; // int not null
         public string name; // nvarchar(255) not null
         public string value; // nvarchar(MAX) NULL
         public System.DateTime? updated_on; // datetime NULL
    }


}
