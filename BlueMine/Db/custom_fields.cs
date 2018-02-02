
namespace BlueMine.Db 
{
    
    
    public partial class T_custom_fields
    {
         public long id; // int not null
         public string type; // nvarchar(30) not null
         public string name; // nvarchar(30) not null
         public string field_format; // nvarchar(30) not null
         public string possible_values; // nvarchar(MAX) NULL
         public string regexp; // nvarchar(4000) NULL
         public long? min_length; // int NULL
         public long? max_length; // int NULL
         public bool is_required; // bit not null
         public bool is_for_all; // bit not null
         public bool is_filter; // bit not null
         public long? position; // int NULL
         public bool? searchable; // bit NULL
         public string default_value; // nvarchar(MAX) NULL
         public bool? editable; // bit NULL
         public bool visible; // bit not null
         public bool? multiple; // bit NULL
         public string format_store; // nvarchar(MAX) NULL
         public string description; // nvarchar(MAX) NULL
    }


}
