
using System.IO;
using dokumen.pub_ultimate_aspnet_core_3_web_api.Extension;
using dokumen.pub_ultimate_aspnet_core_3_web_api.ExtensionFilter;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.ConfigurationSqlServer(Configuration);
            services.ConfigurationRepositoryServer();
            services.AddControllers(options =>
             {
                 options.Filters.Add(typeof(HttpGlobalExceptionFilter));
             }).AddNewtonsoftJson();
            services.AddScoped<ILoggerManger, LoggerManager>();
            services.AddAutoMapper(typeof( Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dokumen.pub_ultimate_aspnet_core_3_web_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerManger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dokumen.pub_ultimate_aspnet_core_3_web_api v1"));
            }
            app.GlobelException(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions(){
                ForwardedHeaders=ForwardedHeaders.All
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
