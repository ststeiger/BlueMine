
namespace BlueMine.Db 
{
    
    
    public partial class T_issue_statuses
    {
         public int id { get; set; } // int not null
         public string name { get; set; } // nvarchar(30) not null
         public bool is_closed { get; set; } // bit not null
         public int? position { get; set; } // int NULL
         public int? default_done_ratio { get; set; } // int NULL
    }


}
