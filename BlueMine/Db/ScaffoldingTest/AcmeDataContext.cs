using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlueMine
{
    
    public partial class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    
    public partial class AcmeDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Integrated Security=true;");
            }
        }
        
        
        
        public DbSet<Person> People { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
		    	
		}
        
        
    }
    
    
}
