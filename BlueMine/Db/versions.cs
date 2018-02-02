
namespace BlueMine.Db 
{
    
    
    public partial class T_versions
    {
         public long id; // int not null
         public long project_id; // int not null
         public string name; // nvarchar(4000) NULL
         public string description; // nvarchar(4000) NULL
         public System.DateTime? effective_date; // date NULL
         public System.DateTime? created_on; // datetime NULL
         public System.DateTime? updated_on; // datetime NULL
         public string wiki_page_title; // nvarchar(4000) NULL
         public string status; // nvarchar(4000) NULL
         public string sharing; // nvarchar(4000) not null
    }


}
