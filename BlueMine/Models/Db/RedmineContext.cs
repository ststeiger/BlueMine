
using Microsoft.EntityFrameworkCore;


namespace BlueMine.Redmine
{
    
    
    public partial class RedmineContext : DbContext
    {
        
        public RedmineContext(DbContextOptions<RedmineContext> options)
            : base(options)
        {    }
        
        
    }
    
    
}
