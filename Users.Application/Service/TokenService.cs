using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Service
{
    public class TokenService : ITokenService
    {
        public string BuildToken(string key, string JwtIssuer, string JwtAudience, User user)
        {
            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Admin ? "Admin" : "User"),
            };

            var securityKay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKay, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(JwtIssuer, JwtAudience,
                claims, expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
