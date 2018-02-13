
namespace BlueMine.Db 
{
    
    
    public partial class T_changesets
    {
         public long id; // int not null
         public long repository_id; // int not null
         public string revision; // nvarchar(4000) not null
         public string committer; // nvarchar(4000) NULL
         public System.DateTime committed_on; // datetime not null
         public string comments; // nvarchar(MAX) NULL
         public System.DateTime? commit_date; // date NULL
         public string scmid; // nvarchar(4000) NULL
         public long? user_id; // int NULL
    }


}
