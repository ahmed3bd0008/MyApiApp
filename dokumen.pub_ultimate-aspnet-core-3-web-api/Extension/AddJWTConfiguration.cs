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
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options=>{
                Options.SaveToken = true;
                var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"].ToString());
                var secret = new SymmetricSecurityKey(key);
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSetting.GetSection("validIssuer").Value,
                    ValidAudience = JwtSetting.GetSection("validAudience").Value,
                    IssuerSigningKey = secret,
                    RequireExpirationTime=false
                };

            });
        }

    }
}
