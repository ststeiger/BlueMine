
namespace BlueMine.Db 
{
    
    
    public partial class T_roles
    {
         public long id; // int not null
         public string name; // nvarchar(30) not null
         public long? position; // int NULL
         public bool? assignable; // bit NULL
         public long builtin; // int not null
         public string permissions; // nvarchar(MAX) NULL
         public string issues_visibility; // nvarchar(30) not null
         public string users_visibility; // nvarchar(30) not null
         public string time_entries_visibility; // nvarchar(30) not null
         public bool all_roles_managed; // bit not null
    }


}
