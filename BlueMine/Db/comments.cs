
namespace BlueMine.Db 
{
    
    
    public partial class T_comments
    {
         public long id; // int not null
         public string commented_type; // nvarchar(30) not null
         public long commented_id; // int not null
         public long author_id; // int not null
         public string comments; // nvarchar(MAX) NULL
         public System.DateTime created_on; // datetime not null
         public System.DateTime updated_on; // datetime not null
    }


}
