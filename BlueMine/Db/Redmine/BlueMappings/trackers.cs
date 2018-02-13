
namespace BlueMine.Db 
{
    
    
    public partial class T_trackers
    {
         public long id; // int not null
         public string name; // nvarchar(30) not null
         public bool is_in_chlog; // bit not null
         public long? position; // int NULL
         public bool is_in_roadmap; // bit not null
         public long? fields_bits; // int NULL
         public long? default_status_id; // int NULL
    }


}
