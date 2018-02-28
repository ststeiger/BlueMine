
namespace BlueMine.Db 
{
    
    
    public partial class T_journal_details
    {
         public int id { get; set; } // int not null
         public int journal_id { get; set; } // int not null
         public string property { get; set; } // nvarchar(30) not null
         public string prop_key { get; set; } // nvarchar(30) not null
         public string old_value { get; set; } // nvarchar(MAX) NULL
         public string value { get; set; } // nvarchar(MAX) NULL
    }


}
