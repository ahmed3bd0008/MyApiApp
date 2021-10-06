
using System.IO;
using dokumen.pub_ultimate_aspnet_core_3_web_api.Extension;
using dokumen.pub_ultimate_aspnet_core_3_web_api.ExtensionFilter;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dokumen.pub_ultimate_aspnet_core_3_web_api.ActionFilter;
using Microsoft.OpenApi.Models;
using NLog;
using System.Collections.Generic;

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
            //AddXmlDataContractSerializerFormatters by default Respnse Be With Json To accepot Respnse Wit Text/Xml
            //ReturnHttpNotAcceptable to accept header With unsprort to Server  Return Respons Error 406
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                //globel scope Iaction Result
                //config.Filters.Add(new ValidationFilterAttribute(_logger));
                config.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
            // services.AddControllers();
            services.ConfigurationSqlServer(Configuration);
            services.ConfigurationRepositoryServer();
            //make Ivalue Model status Error From 400 That Represent BadRequest To 422 that represent Model Status Invaild
            services.Configure<ApiBehaviorOptions>
            (
                Option => Option.SuppressModelStateInvalidFilter = true
            );

            services.AddScoped<ILoggerManger, LoggerManager>();
            services.AddAutoMapper(typeof(Startup));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "dokumen.pub_ultimate_aspnet_core_3_web_api", Version = "v1" });
            //});
            // add Action Filter Scope Of Cotroller Or Action
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<UpdateCompanyFilterAttribute>();

            //add Authentication
            services.AddAuthentication();
            services.configurationIdentityService();
            services.JWTConfiguration(Configuration);
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Code Maze API", Version = "v1" });
                s.SwaggerDoc("v2", new OpenApiInfo { Title = "Code Maze API", Version = "v2" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                    {
                        new OpenApiSecurityScheme
                    {
                            Reference = new OpenApiReference
                    {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                                     Name = "Bearer", }, new List<string>()
                     }
                     });
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManger logger)
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
            // app.UseForwardedHeaders() will forward proxy headers to the current request. This will help us during application deployment
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
