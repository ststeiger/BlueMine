
namespace BlueMine.Db 
{
    
    
    public partial class T_documents
    {
         public long id; // int not null
         public long project_id; // int not null
         public long category_id; // int not null
         public string title; // nvarchar(4000) not null
         public string description; // nvarchar(MAX) NULL
         public System.DateTime? created_on; // datetime NULL
    }


}
