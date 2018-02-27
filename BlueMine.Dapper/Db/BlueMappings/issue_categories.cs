
namespace BlueMine.Db 
{
    
    
    public partial class T_issue_categories
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public string name { get; set; } // nvarchar(60) not null
         public int? assigned_to_id { get; set; } // int NULL
    }


}
