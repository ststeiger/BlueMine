
namespace BlueMine.Db 
{
    
    
    public partial class T_journal_details
    {
         public long id; // int not null
         public long journal_id; // int not null
         public string property; // nvarchar(30) not null
         public string prop_key; // nvarchar(30) not null
         public string old_value; // nvarchar(MAX) NULL
         public string value; // nvarchar(MAX) NULL
    }


}
