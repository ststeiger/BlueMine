
namespace BlueMine.Db 
{
    
    
    public partial class T_issue_relations
    {
         public int id { get; set; } // int not null
         public int issue_from_id { get; set; } // int not null
         public int issue_to_id { get; set; } // int not null
         public string relation_type { get; set; } // nvarchar(4000) not null
         public int? delay { get; set; } // int NULL
    }


}
