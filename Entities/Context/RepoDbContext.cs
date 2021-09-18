using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Entity.Configuration;
namespace Entity.Context
{
    public class RepoDbContext:DbContext
    {
      public RepoDbContext(DbContextOptions options):base(options)
      {
          
      }
      
      protected override void OnModelCreating(ModelBuilder builder)
      {
          builder.ApplyConfiguration<Company>(new ComPanyConfiguration());
          builder.ApplyConfiguration<Employee>(new EmployeConfiguration());
            builder.Entity<Company>().HasMany(d => d.Employees).WithOne(d => d.Company).OnDelete(DeleteBehavior.Cascade);
      }
      public DbSet<Company>Companies{get;set;}
      public DbSet<Employee>Employees{get;set;}
    }
    
}