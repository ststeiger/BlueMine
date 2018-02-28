
namespace BlueMine.Db 
{
    
    
    public partial class T_roles
    {
         public int id { get; set; } // int not null
         public string name { get; set; } // nvarchar(30) not null
         public int? position { get; set; } // int NULL
         public bool? assignable { get; set; } // bit NULL
         public int builtin { get; set; } // int not null
         public string permissions { get; set; } // nvarchar(MAX) NULL
         public string issues_visibility { get; set; } // nvarchar(30) not null
         public string users_visibility { get; set; } // nvarchar(30) not null
         public string time_entries_visibility { get; set; } // nvarchar(30) not null
         public bool all_roles_managed { get; set; } // bit not null
    }


}
