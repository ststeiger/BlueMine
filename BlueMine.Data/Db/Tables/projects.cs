
namespace BlueMine.Db 
{
    
    
    public partial class T_projects
    {
         public int id { get; set; } // int not null
         public string name { get; set; } // nvarchar(4000) not null
         public string description { get; set; } // nvarchar(MAX) NULL
         public string homepage { get; set; } // nvarchar(4000) NULL
         public bool is_public { get; set; } // bit not null
         public int? parent_id { get; set; } // int NULL
         public System.DateTime? created_on { get; set; } // datetime NULL
         public System.DateTime? updated_on { get; set; } // datetime NULL
         public string identifier { get; set; } // nvarchar(4000) NULL
         public int status { get; set; } // int not null
         public int? lft { get; set; } // int NULL
         public int? rgt { get; set; } // int NULL
         public bool inherit_members { get; set; } // bit not null
         public int? default_version_id { get; set; } // int NULL
    }


}
