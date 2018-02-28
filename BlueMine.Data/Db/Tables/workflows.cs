
namespace BlueMine.Db 
{
    
    
    public partial class T_workflows
    {
         public int id { get; set; } // int not null
         public int tracker_id { get; set; } // int not null
         public int old_status_id { get; set; } // int not null
         public int new_status_id { get; set; } // int not null
         public int role_id { get; set; } // int not null
         public bool assignee { get; set; } // bit not null
         public bool author { get; set; } // bit not null
         public string type { get; set; } // nvarchar(30) NULL
         public string field_name { get; set; } // nvarchar(30) NULL
         public string rule { get; set; } // nvarchar(30) NULL
    }


}
