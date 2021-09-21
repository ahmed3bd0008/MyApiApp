using Contracts.Interface;
using LoggerService;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.ActionFilter
{
            public class UpdateCompanyFilterAttribute : IActionFilter
            {
                        private readonly IMangeRepository _mangeRepository;
                        private readonly ILoggerManger _loggerManger;

                        public UpdateCompanyFilterAttribute(IMangeRepository mangeRepository,ILoggerManger loggerManger)
                {
                    _mangeRepository=mangeRepository;
                    _loggerManger=loggerManger;
                }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context) 
        {
            var 
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param == null) 
            { _logger.LogError($"Object sent from client is null. Controller: {controller}, action: {action}"); context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}"); return; } if (!context.ModelState.IsValid) { _logger.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}"); context.Result = new UnprocessableEntityObjectResult(context.ModelState); } }

       
            }
}