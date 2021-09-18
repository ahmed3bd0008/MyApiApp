using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace dokumen.pub_ultimate_aspnet_core_3_web_api.ExtensionFilter
{
    
public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}