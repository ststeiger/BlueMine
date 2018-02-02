
namespace BlueMine.Db 
{
    
    
    public partial class T_issues
    {
         public long id; // int not null
         public long tracker_id; // int not null
         public long project_id; // int not null
         public string subject; // nvarchar(4000) not null
         public string description; // nvarchar(MAX) NULL
         public System.DateTime? due_date; // date NULL
         public long? category_id; // int NULL
         public long status_id; // int not null
         public long? assigned_to_id; // int NULL
         public long priority_id; // int not null
         public long? fixed_version_id; // int NULL
         public long author_id; // int not null
         public long lock_version; // int not null
         public System.DateTime? created_on; // datetime NULL
         public System.DateTime? updated_on; // datetime NULL
         public System.DateTime? start_date; // date NULL
         public long done_ratio; // int not null
         public System.Decimal? estimated_hours; // float NULL
         public long? parent_id; // int NULL
         public long? root_id; // int NULL
         public long? lft; // int NULL
         public long? rgt; // int NULL
         public bool is_private; // bit not null
         public System.DateTime? closed_on; // datetime NULL
    }


}
