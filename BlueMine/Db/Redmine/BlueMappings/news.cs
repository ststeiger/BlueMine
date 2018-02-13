
namespace BlueMine.Db 
{
    
    
    public partial class T_news
    {
         public long id; // int not null
         public long? project_id; // int NULL
         public string title; // nvarchar(60) not null
         public string summary; // nvarchar(255) NULL
         public string description; // nvarchar(MAX) NULL
         public long author_id; // int not null
         public System.DateTime? created_on; // datetime NULL
         public long comments_count; // int not null
    }


}
