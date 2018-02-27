
namespace BlueMine.Db
{


    public partial class T_issues_history
    {
        public System.Guid isshist_uid { get; set; } // uniqueidentifier NOT NULL

        // public partial class BlueMineContext : DbContext
        // modelBuilder.Entity<T_issues_history>(entity =>
        // entity.HasKey(e => e.isshist_uid).HasName("PK_issues_history");
    }


}
