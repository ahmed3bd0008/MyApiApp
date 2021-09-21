using System.Linq;
using System.Threading.Tasks;
using LoggerService;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.ActionFilter
{
            public class ValidationFilterAttribute : IAsyncActionFilter
            {
                        private readonly ILoggerManger _logger;

                        public ValidationFilterAttribute(ILoggerManger logger)
                        {
                            _logger=logger;
                        }
                        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
                        {
                            //Befor Action
                            var action =context.RouteData.Values["action"].ToString();
                            var controller =context.RouteData.Values["controller"].ToString();
                            var prams=context.ActionArguments.SingleOrDefault(x=>x.Value.ToString().Contains("Dto")).Value;
                            if(prams==null)
                            {
                                _logger.LogError($"Object is null In Action{action} Controller {controller}");
                                Console.WriteLine($"Object is null In Action{action} Controller {controller}");
                                context.Result=new BadRequestObjectResult($"Object is null In Action{action} Controller {controller}");
                                return;
                            }
                            if(!context.ModelState.IsValid)
                            {
                                  _logger.LogError($"Object is null In Action{action} Controller {controller}");
                                Console.WriteLine($"Object is null In Action{action} Controller {controller}");
                                context.Result=new UnprocessableEntityObjectResult(context.ModelState);
                return;
                            }
                                    var resultContext = await next();
                            //after Action
                            
                        }
            }
}