
namespace BlueMine.Db 
{
    
    
    public partial class T_changesets
    {
         public int id { get; set; } // int not null
         public int repository_id { get; set; } // int not null
         public string revision { get; set; } // nvarchar(4000) not null
         public string committer { get; set; } // nvarchar(4000) NULL
         public System.DateTime committed_on { get; set; } // datetime not null
         public string comments { get; set; } // nvarchar(MAX) NULL
         public System.DateTime? commit_date { get; set; } // date NULL
         public string scmid { get; set; } // nvarchar(4000) NULL
         public int? user_id { get; set; } // int NULL
    }


}
