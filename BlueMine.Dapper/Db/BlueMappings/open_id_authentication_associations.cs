
namespace BlueMine.Db 
{
    
    
    public partial class T_open_id_authentication_associations
    {
         public int id { get; set; } // int not null
         public int? issued { get; set; } // int NULL
         public int? lifetime { get; set; } // int NULL
         public string handle { get; set; } // nvarchar(4000) NULL
         public string assoc_type { get; set; } // nvarchar(4000) NULL
         public byte[] server_url { get; set; } // varbinary(MAX) NULL
         public byte[] secret { get; set; } // varbinary(MAX) NULL
    }


}
