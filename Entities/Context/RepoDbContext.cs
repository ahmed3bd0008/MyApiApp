using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Entity.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Entities.Model;
using Entities.Configuration;

namespace Entity.Context
{
    public class RepoDbContext:IdentityDbContext<User,Role,string>
    {
      public RepoDbContext(DbContextOptions options):base(options)
      {
          
      }
      
      protected override void OnModelCreating(ModelBuilder builder)
      {
          base.OnModelCreating(builder);
          builder.ApplyConfiguration<Company>(new ComPanyConfiguration());
          builder.ApplyConfiguration<Employee>(new EmployeConfiguration());
          builder.ApplyConfiguration<Role>(new RoleConfiguration());
          builder.Entity<Company>().HasMany(d => d.Employees).WithOne(d => d.Company).OnDelete(DeleteBehavior.Cascade);
      }
      public DbSet<Company>Companies{get;set;}
      public DbSet<Employee>Employees{get;set;}
    }
    
}