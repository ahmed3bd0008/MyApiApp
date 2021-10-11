using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Entities.Model;
using Entity.Context;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api
{
    public static class AddIdentityService
    {
        public static void configurationIdentityService(this IServiceCollection services)
        {
              var builder= services.AddIdentity<User,Role>(Policy=>
                        {
                                    Policy.SignIn.RequireConfirmedAccount=true;
                                    Policy.Password.RequiredLength=4;
                                    Policy.Password.RequireUppercase=false;
                                    Policy.Password.RequireNonAlphanumeric=false;
                                    Policy.Password.RequiredUniqueChars=0;
                                    Policy.Password.RequireDigit=false;
                                    Policy.User.RequireUniqueEmail=true;     
                        }); 
              builder=new IdentityBuilder(builder.UserType,builder.RoleType,builder.Services);
              builder.AddEntityFrameworkStores<RepoDbContext>().AddDefaultTokenProviders();
                
        }
    }
}