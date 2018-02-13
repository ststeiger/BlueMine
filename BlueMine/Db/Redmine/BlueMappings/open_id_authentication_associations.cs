
namespace BlueMine.Db 
{
    
    
    public partial class T_open_id_authentication_associations
    {
         public long id; // int not null
         public long? issued; // int NULL
         public long? lifetime; // int NULL
         public string handle; // nvarchar(4000) NULL
         public string assoc_type; // nvarchar(4000) NULL
         public byte[] server_url; // varbinary(MAX) NULL
         public byte[] secret; // varbinary(MAX) NULL
    }


}
