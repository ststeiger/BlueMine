
namespace BlueMine.Db 
{
    
    
    public partial class T_wiki_content_versions
    {
         public long id; // int not null
         public long wiki_content_id; // int not null
         public long page_id; // int not null
         public long? author_id; // int NULL
         public byte[] data; // varbinary(MAX) NULL
         public string compression; // nvarchar(6) NULL
         public string comments; // nvarchar(1024) NULL
         public System.DateTime updated_on; // datetime not null
         public long version; // int not null
    }


}
