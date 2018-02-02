
namespace BlueMine.Db 
{
    
    
    public partial class T_workflows
    {
         public long id; // int not null
         public long tracker_id; // int not null
         public long old_status_id; // int not null
         public long new_status_id; // int not null
         public long role_id; // int not null
         public bool assignee; // bit not null
         public bool author; // bit not null
         public string type; // nvarchar(30) NULL
         public string field_name; // nvarchar(30) NULL
         public string rule; // nvarchar(30) NULL
    }


}
