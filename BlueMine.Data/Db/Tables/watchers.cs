
namespace BlueMine.Db 
{
    
    
    public partial class T_watchers
    {
         public int id { get; set; } // int not null
         public string watchable_type { get; set; } // nvarchar(4000) not null
         public int watchable_id { get; set; } // int not null
         public int? user_id { get; set; } // int NULL
    }


}
