
using Microsoft.EntityFrameworkCore;


namespace BlueMine.Redmine
{
    
    
    public partial class RedmineContext : DbContext
    {
        
        
        public RedmineContext(DbContextOptions<RedmineContext> options)
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
