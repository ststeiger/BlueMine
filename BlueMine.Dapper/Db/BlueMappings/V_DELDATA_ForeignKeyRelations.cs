
namespace BlueMine.Db 
{
    
    
    public partial class T_V_DELDATA_ForeignKeyRelations
    {
         public string FK_CONSTRAINT_NAME { get; set; } // nvarchar(128) NULL
         public string FK_TABLE_NAME { get; set; } // nvarchar(128) NULL
         public string FK_COLUMN_NAME { get; set; } // nvarchar(128) NULL
         public long? FK_ORDINAL_POSITION { get; set; } // int NULL
         public string REFERENCED_CONSTRAINT_NAME { get; set; } // nvarchar(128) NULL
         public string REFERENCED_TABLE_NAME { get; set; } // nvarchar(128) NULL
         public string REFERENCED_COLUMN_NAME { get; set; } // nvarchar(128) NULL
         public long? REFERENCED_ORDINAL_POSITION { get; set; } // int NULL
    }


}
