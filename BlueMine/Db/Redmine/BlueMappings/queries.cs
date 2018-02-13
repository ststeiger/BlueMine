
namespace BlueMine.Db 
{
    
    
    public partial class T_queries
    {
         public long id; // int not null
         public long? project_id; // int NULL
         public string name; // nvarchar(4000) not null
         public string filters; // nvarchar(MAX) NULL
         public long user_id; // int not null
         public string column_names; // nvarchar(MAX) NULL
         public string sort_criteria; // nvarchar(MAX) NULL
         public string group_by; // nvarchar(4000) NULL
         public string type; // nvarchar(4000) NULL
         public long? visibility; // int NULL
         public string options; // nvarchar(MAX) NULL
    }


}
