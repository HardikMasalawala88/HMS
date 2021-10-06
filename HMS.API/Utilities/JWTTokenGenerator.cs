using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMS.API.Utilities
{
    public class JWTTokenGenerator
    {
        private readonly IConfiguration _configurationSettings;

        public JWTTokenGenerator(IConfiguration configuration)
        {
            _configurationSettings = configuration;
        }

        public string GenerateLoginToken(string userName, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings["JWT:JwtKey"]));
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configurationSettings["JWT:JwtExpireDays"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configurationSettings["JWT:JwtIssuer"],
                _configurationSettings["JWT:JwtIssuer"],
                Claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
