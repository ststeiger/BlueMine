
namespace BlueMine.Db 
{
    
    
    public partial class T_issue_relations
    {
         public long id; // int not null
         public long issue_from_id; // int not null
         public long issue_to_id; // int not null
         public string relation_type; // nvarchar(4000) not null
         public long? delay; // int NULL
    }


}
