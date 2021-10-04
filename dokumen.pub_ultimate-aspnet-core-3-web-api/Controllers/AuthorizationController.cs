using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts.Interface;
using Entity.Model;
using Entities.Model;
using Entities.DataTransferObject;
using AutoMapper;
using dokumen.pub_ultimate_aspnet_core_3_web_api.ActionFilter;
using System.Threading.Tasks;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMangeRepository _mangeRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

                        public AuthorizationController(ILogger<WeatherForecastController> logger,
                                            IMangeRepository mangeRepository,
                                            UserManager<User>userManager,
                                            RoleManager<Role>roleManager,
                                            IMapper mapper)
        {
            _logger = logger;
            _mangeRepository=mangeRepository;
            _userManager=userManager;
            _roleManager=roleManager;
            _mapper=mapper;
        }

        [HttpGet]
        public IEnumerable<Company> Get()
        {
            
            return(  _mangeRepository.componyRepository.FindAll(false).ToArray());
           
        }
        [HttpPost("AddUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddUser(RegistUserDto registUserDto)
        {
            var user=_mapper.Map<User>(registUserDto);
            var res=await _userManager.CreateAsync(user,registUserDto.Password);
            if(! res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return BadRequest(ModelState);
             }
             var xx=_roleManager.RoleExistsAsync(registUserDto.Roles.FirstOrDefault());
              await _userManager.AddToRolesAsync(user,registUserDto.Roles);
              return StatusCode(201); 

           
           
        }
    }
}
