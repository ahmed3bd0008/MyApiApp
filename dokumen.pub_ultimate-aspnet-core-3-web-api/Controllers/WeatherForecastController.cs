using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts.Interface;
using Entity.Model;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
                        private readonly IMangeRepository _mangeRepository;

                        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMangeRepository mangeRepository)
        {
            _logger = logger;
            _mangeRepository=mangeRepository;
        }

        [HttpGet]
        public IEnumerable<Company> Get()
        {
            
            return(  _mangeRepository.componyRepository.FindAll(false).ToArray());
           
        }
    }
}
