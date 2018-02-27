
namespace BlueMine.Db 
{
    
    
    public partial class T_changes
    {
         public int id { get; set; } // int not null
         public int changeset_id { get; set; } // int not null
         public string action { get; set; } // nvarchar(1) not null
         public string path { get; set; } // nvarchar(MAX) not null
         public string from_path { get; set; } // nvarchar(MAX) NULL
         public string from_revision { get; set; } // nvarchar(4000) NULL
         public string revision { get; set; } // nvarchar(4000) NULL
         public string branch { get; set; } // nvarchar(4000) NULL
    }


}
