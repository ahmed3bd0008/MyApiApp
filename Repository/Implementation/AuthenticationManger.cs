using System.Threading.Tasks;
using Contracts.Interface;
using Entities.DataTransferObject;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace Repository.Implementation
{
            public class AuthenticationManger : IAuthenticationManger
            {
                        private readonly IConfiguration _config;
                        private readonly UserManager<User> _userManager;
                        private readonly IConfigurationSection _serviceKey;

                        public AuthenticationManger(IConfiguration config,UserManager<User>userManager)
                        {
                            _config=config;
                            _userManager=userManager;
                            _serviceKey=_config.GetSection("JwtSettings");
                        }
                        public User user { get ; set ; }

                        public async Task<string> CreateToken()
                        {
                                   SigningCredentials signingCredentials=getSigningCredentials();
                                   List<Claim> claims=await getClaims();
                                   JwtSecurityToken jwtSecurityToken =generatToken(signingCredentials,claims);
                                   return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


                        }
                        public async Task<bool> VaildUser(LoginUserDto loginUserDto)
                        {
                              user=await _userManager.FindByNameAsync(loginUserDto.UserName);
                              return (user!=null && await _userManager.CheckPasswordAsync(user,loginUserDto.Password));        
                        }
                        private SigningCredentials getSigningCredentials()
                        {
                                string Key=_serviceKey.GetSection("Key").Value;
                                var secret=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
                                return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
                        }
                        private async Task< List<Claim>> getClaims()
                        {
                            List<Claim>claims=new();
                            claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                            IList<string> roles=await _userManager.GetRolesAsync(user);
                            foreach (var role in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role,role));
                            }
                            return claims;
                        }
                        private JwtSecurityToken generatToken(SigningCredentials signingCredentials,List<Claim>claims)
                        {
                             JwtSecurityToken jwtSecurityToken= new
                             (
                                 issuer:_serviceKey.GetSection("CodeMazeAPI").Value,
                                 audience:_serviceKey.GetSection("https://localhost:5001").Value,
                                 claims:claims,
                                 signingCredentials:signingCredentials,
                                 expires:DateTime.Now.AddMinutes( Convert.ToDouble( _serviceKey.GetSection("expires").Value))

                             );
                             return jwtSecurityToken;
                        }

                       
            }
}