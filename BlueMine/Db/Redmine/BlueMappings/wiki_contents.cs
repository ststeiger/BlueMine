
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_contents
    {
         public long id; // int not null
         public long page_id; // int not null
         public long? author_id; // int NULL
         public string text; // nvarchar(MAX) NULL
         public string comments; // nvarchar(1024) NULL
         public System.DateTime updated_on; // datetime not null
         public long version; // int not null
    }


}
