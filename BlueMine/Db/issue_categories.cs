
namespace BlueMine.Db 
{
    
    
    public partial class T_issue_categories
    {
         public long id; // int not null
         public long project_id; // int not null
         public string name; // nvarchar(60) not null
         public long? assigned_to_id; // int NULL
    }


}
