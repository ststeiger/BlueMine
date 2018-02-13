
namespace BlueMine.Db 
{
    
    
    public partial class T_time_entries
    {
         public long id; // int not null
         public long project_id; // int not null
         public long user_id; // int not null
         public long? issue_id; // int NULL
         public System.Decimal hours; // float not null
         public string comments; // nvarchar(1024) NULL
         public long activity_id; // int not null
         public System.DateTime spent_on; // date not null
         public long tyear; // int not null
         public long tmonth; // int not null
         public long tweek; // int not null
         public System.DateTime created_on; // datetime not null
         public System.DateTime updated_on; // datetime not null
    }


}
