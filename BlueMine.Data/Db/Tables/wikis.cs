
namespace BlueMine.Db 
{
    
    
    public partial class T_wikis
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public string start_page { get; set; } // nvarchar(255) not null
         public int status { get; set; } // int not null
    }


}
