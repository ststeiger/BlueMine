
namespace BlueMine.Db 
{
    
    
    public partial class T_queries
    {
         public int id { get; set; } // int not null
         public int? project_id { get; set; } // int NULL
         public string name { get; set; } // nvarchar(4000) not null
         public string filters { get; set; } // nvarchar(MAX) NULL
         public int user_id { get; set; } // int not null
         public string column_names { get; set; } // nvarchar(MAX) NULL
         public string sort_criteria { get; set; } // nvarchar(MAX) NULL
         public string group_by { get; set; } // nvarchar(4000) NULL
         public string type { get; set; } // nvarchar(4000) NULL
         public int? visibility { get; set; } // int NULL
         public string options { get; set; } // nvarchar(MAX) NULL
    }


}
