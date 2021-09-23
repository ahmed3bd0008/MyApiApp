using Contracts.Interface;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
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
            bool IsUpdate = false;
            IsUpdate = context.HttpContext.Request.Method.Equals("PUT");
            var action=context.RouteData.Values["action"].ToString();
            var controller=context.RouteData.Values["controller"].ToString();
            var CompanyId=(Guid) context.ActionArguments["CompanyId"];
            var Company=_mangeRepository.componyRepository.GetCompany(CompanyId, IsUpdate);
            if(Company==null)
            {
                _loggerManger.LogError($"thier is No Company With Id {CompanyId}");
                context.ModelState.AddModelError(string.Empty,$"thier is No Company With Id {CompanyId}");
                context.Result=new BadRequestObjectResult($"Object is null In Action{action} Controller {controller}");
                                return;
            }
            context.HttpContext.Items.Add("Company",Company);
            }
}
}