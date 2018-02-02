
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_pages
    {
         public long id; // int not null
         public long wiki_id; // int not null
         public string title; // nvarchar(255) not null
         public System.DateTime created_on; // datetime not null
         public bool @protected; // bit not null
         public long? parent_id; // int NULL
    }


}
