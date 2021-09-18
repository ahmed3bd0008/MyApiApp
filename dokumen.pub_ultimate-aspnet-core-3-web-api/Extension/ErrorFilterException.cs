using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Entity.ErrorDetails;
using LoggerService;

namespace  dokumen.pub_ultimate_aspnet_core_3_web_api.Extension
{
            public static class ErrorFilterException
            {
                  public static void GlobelException(this IApplicationBuilder app,ILoggerManger logger) 
                  {
                              app.UseExceptionHandler(appErro=>appErro.Run(async context=>{
                              context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                              context.Response.ContentType="application/json";
                              var contextFeature=context.Features.Get<IExceptionHandlerFeature>();
                              if(contextFeature!=null)
                              {
                                  logger.LogError($"Something went wrong: {contextFeature.Error}");
                                  await  context.Response.WriteAsync(new ErrorDetails()
                                  {
                                     StatusCod=context.Response.StatusCode,
                                     Message="errormessage"         
                                  }.ToString());
                              }
                              }));
                  }     
            }
}