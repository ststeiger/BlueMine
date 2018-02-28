
using Microsoft.EntityFrameworkCore;


namespace BlueMine.Db
{


    public partial class BlueMineContext : DbContext
    {
        
        
        public BlueMineContext(DbContextOptions<BlueMineContext> options)
            : base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Database.AutoTransactionsEnabled = true;
            this.Database.SetCommandTimeout(30);
            // this.Database.
            // this.Model.SqlServer().
        }
        
        
    }
    
    
}
