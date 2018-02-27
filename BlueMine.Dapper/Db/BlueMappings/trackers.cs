
namespace BlueMine.Db 
{
    
    
    public partial class T_trackers
    {
         public int id { get; set; } // int not null
         public string name { get; set; } // nvarchar(30) not null
         public bool is_in_chlog { get; set; } // bit not null
         public int? position { get; set; } // int NULL
         public bool is_in_roadmap { get; set; } // bit not null
         public int? fields_bits { get; set; } // int NULL
         public int? default_status_id { get; set; } // int NULL
    }


}
