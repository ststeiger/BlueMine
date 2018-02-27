
namespace BlueMine.Db 
{
    
    
    public partial class T_attachments
    {
         public int id { get; set; } // int not null
         public int? container_id { get; set; } // int NULL
         public string container_type { get; set; } // nvarchar(30) NULL
         public string filename { get; set; } // nvarchar(4000) not null
         public string disk_filename { get; set; } // nvarchar(4000) not null
         public long filesize { get; set; } // bigint not null
         public string content_type { get; set; } // nvarchar(4000) NULL
         public string digest { get; set; } // nvarchar(40) not null
         public int downloads { get; set; } // int not null
         public int author_id { get; set; } // int not null
         public System.DateTime? created_on { get; set; } // datetime NULL
         public string description { get; set; } // nvarchar(4000) NULL
         public string disk_directory { get; set; } // nvarchar(4000) NULL
    }


}
