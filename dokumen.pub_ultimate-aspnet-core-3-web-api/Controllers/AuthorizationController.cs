using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts.Interface;
using Entities.Model;
using Entities.DataTransferObject;
using AutoMapper;
using dokumen.pub_ultimate_aspnet_core_3_web_api.ActionFilter;
using System.Threading.Tasks;
using System.Transactions;
namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IMangeRepository _mangeRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManger _authenticationManger;

        public AuthorizationController(ILogger<AuthorizationController> logger,
                                            IMangeRepository mangeRepository,
                                            UserManager<User>userManager,
                                            RoleManager<Role>roleManager,
                                            IMapper mapper,
                                            IAuthenticationManger authenticationManger )
        {
            _logger = logger;
            _mangeRepository=mangeRepository;
            _userManager=userManager;
            _roleManager=roleManager;
            _mapper=mapper;
            _authenticationManger=authenticationManger;
        }
        [HttpPost("AddUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddUser(RegistUserDto registUserDto)
        {
            var user=_mapper.Map<User>(registUserDto);
            try
             {
                    using(TransactionScope scope = new TransactionScope())
                    {
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
                                    /* Perform transactional work here */
                                scope.Complete();
                    }
                     return StatusCode(201);        
             }
             catch (TransactionAbortedException ex)
             {
                 ModelState.AddModelError(string.Empty,$"add again {ex.Message}");
                 return BadRequest(ModelState);   
             }
        }
        [HttpPost("LogIn")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> LogInUser(LoginUserDto loginUser)
        {
            if(! await _authenticationManger.VaildUser(loginUser))
                {
                    _logger.LogError($"this user {loginUser.UserName} with this password {loginUser.Password}");
                    return Unauthorized();
                }

            return Ok(new {Token=await _authenticationManger.CreateToken()});   
        }
    }
}
