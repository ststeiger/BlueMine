
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_redirects
    {
         public long id; // int not null
         public long wiki_id; // int not null
         public string title; // nvarchar(4000) NULL
         public string redirects_to; // nvarchar(4000) NULL
         public System.DateTime created_on; // datetime not null
         public long redirects_to_wiki_id; // int not null
    }


}
