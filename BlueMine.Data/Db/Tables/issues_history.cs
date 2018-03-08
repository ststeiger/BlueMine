
namespace BlueMine.Db 
{
    
    
    public partial class T_issues_history
    {
         public string operation_dbuser { get; set; } // nvarchar(128) NULL
         public string operation_name { get; set; } // nvarchar(128) NULL
         public string operation_time { get; set; } // nvarchar(128) NULL
         public int id { get; set; } // int not null
         public int tracker_id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public string subject { get; set; } // nvarchar(4000) not null
         public string description { get; set; } // nvarchar(MAX) NULL
         public System.DateTime? due_date { get; set; } // date NULL
         public int? category_id { get; set; } // int NULL
         public int status_id { get; set; } // int not null
         public int? assigned_to_id { get; set; } // int NULL
         public int priority_id { get; set; } // int not null
         public int? fixed_version_id { get; set; } // int NULL
         public int author_id { get; set; } // int not null
         public int lock_version { get; set; } // int not null
         public System.DateTime? created_on { get; set; } // datetime NULL
         public System.DateTime? updated_on { get; set; } // datetime NULL
         public System.DateTime? start_date { get; set; } // date NULL
         public int done_ratio { get; set; } // int not null
         public double? estimated_hours { get; set; } // float NULL
         public int? parent_id { get; set; } // int NULL
         public int? root_id { get; set; } // int NULL
         public int? lft { get; set; } // int NULL
         public int? rgt { get; set; } // int NULL
         public bool is_private { get; set; } // bit not null
         public System.DateTime? closed_on { get; set; } // datetime NULL
    }


}
