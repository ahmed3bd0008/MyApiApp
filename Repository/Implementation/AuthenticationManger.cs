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
using Microsoft.Extensions.Options;
using Entities.Configuration;

namespace Repository.Implementation
{
    public class AuthenticationManger : IAuthenticationManger
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationSection _serviceKey;
     private readonly JwtSettings _jwtSettings;

     public AuthenticationManger(IConfiguration config,
         UserManager<User> userManager,
         IOptionsMonitor<JwtSettings>jwtSettings)
        {
            _config = config;
            _userManager = userManager;
            _serviceKey = _config.GetSection("JwtSettings");
            _jwtSettings=jwtSettings.CurrentValue;
        }
        public User user { get; set; }

        public async Task<string> CreateToken()
        {
            var signingCredentials = getSigningCredentials();
            var claims = await getClaims();
            var tokenOptions = generatToken(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        public async Task<bool> VaildUser(LoginUserDto loginUserDto)
        {
            user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            return (user != null && await _userManager.CheckPasswordAsync(user, loginUserDto.Password));
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"].ToString());
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private SigningCredentials getSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"].ToString());
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> getClaims()
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserName) };
            var roles = await _userManager.GetRolesAsync(user); foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken generatToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            JwtSecurityToken jwtSecurityToken = new
            (
                issuer: _serviceKey.GetSection("validIssuer").Value,
                audience: _serviceKey.GetSection("validAudience").Value,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_serviceKey.GetSection("expires").Value))

            );
            return jwtSecurityToken;
        }
       
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.GetSection("validIssuer").Value, 
            audience: jwtSettings.GetSection("validAudience").Value, claims: claims, expires:
            DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)), signingCredentials: signingCredentials
            ); return tokenOptions;
        }

    }
}