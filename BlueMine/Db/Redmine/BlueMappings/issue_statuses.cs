
namespace BlueMine.Db 
{
    
    
    public partial class T_issue_statuses
    {
         public long id; // int not null
         public string name; // nvarchar(30) not null
         public bool is_closed; // bit not null
         public long? position; // int NULL
         public long? default_done_ratio; // int NULL
    }


}
