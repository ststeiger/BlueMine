
namespace BlueMine.Db 
{
    
    
    public partial class T_time_entries
    {
         public int id { get; set; } // int not null
         public int project_id { get; set; } // int not null
         public int user_id { get; set; } // int not null
         public int? issue_id { get; set; } // int NULL
         public double hours { get; set; } // float not null
         public string comments { get; set; } // nvarchar(1024) NULL
         public int activity_id { get; set; } // int not null
         public System.DateTime spent_on { get; set; } // date not null
         public int tyear { get; set; } // int not null
         public int tmonth { get; set; } // int not null
         public int tweek { get; set; } // int not null
         public System.DateTime created_on { get; set; } // datetime not null
         public System.DateTime updated_on { get; set; } // datetime not null
    }


}
