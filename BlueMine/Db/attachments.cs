
namespace BlueMine.Db 
{
    
    
    public partial class T_attachments
    {
         public long id; // int not null
         public long? container_id; // int NULL
         public string container_type; // nvarchar(30) NULL
         public string filename; // nvarchar(4000) not null
         public string disk_filename; // nvarchar(4000) not null
         public long filesize; // bigint not null
         public string content_type; // nvarchar(4000) NULL
         public string digest; // nvarchar(40) not null
         public long downloads; // int not null
         public long author_id; // int not null
         public System.DateTime? created_on; // datetime NULL
         public string description; // nvarchar(4000) NULL
         public string disk_directory; // nvarchar(4000) NULL
    }


}
