using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Extension
{
    public static class AddJWTConfiguration
    {
        public  static void JWTConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            IConfigurationSection JwtSetting=configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt => 
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options=>{
                Options.TokenValidationParameters=new TokenValidationParameters()
                {
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer=JwtSetting.GetSection("validIssuer").Value,
                    ValidAudience=JwtSetting.GetSection("validAudience").Value,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting.GetSection("Key").Value))
                };
            });
        }

    }
}
