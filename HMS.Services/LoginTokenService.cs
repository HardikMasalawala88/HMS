using HMS.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services.Services
{
    public class LoginTokenService : ILoginTokenService
    {
        private readonly IConfiguration _config;

        public LoginTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateLoginToken()
        {
            var Claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:JwtKey"]));
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["JWT:JwtExpireDays"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["JWT:JwtIssuer"],
                _config["JWT:JwtIssuer"],
                Claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
