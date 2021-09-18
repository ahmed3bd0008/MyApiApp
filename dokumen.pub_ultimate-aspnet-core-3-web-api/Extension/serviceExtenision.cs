
using Contracts.Interface;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Implementation; 
namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Extension
{
    public static class serviceExtension
    {
            public static void ConfigurationSqlServer(this IServiceCollection services,IConfiguration configuration)
             {
                services.AddDbContext<RepoDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),b=>b.MigrationsAssembly("dokumen.pub_ultimate-aspnet-core-3-web-api")));
             }
             public static void ConfigurationRepositoryServer(this IServiceCollection services)
             {
                services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
                services.AddScoped(typeof(IEmployeeRepository),typeof(EmployeeRepository));
                services.AddScoped(typeof(IComponyRepository),typeof(CompanyRepository));
                services.AddScoped(typeof(IMangeRepository),typeof(MangeRepository));
             }
    }
}