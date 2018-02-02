
namespace BlueMine.Db 
{
    
    
    public partial class T_import_items
    {
         public long id; // int not null
         public long import_id; // int not null
         public long position; // int not null
         public long? obj_id; // int NULL
         public string message; // nvarchar(MAX) NULL
    }


}
