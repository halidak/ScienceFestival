using Microsoft.IdentityModel.Tokens;
using ScienceFestival.REST.Gateway.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScienceFestival.REST.Gateway.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTq"));
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("your-claim-type-for-jury-id", user.Id)
            };

            var token = new JwtSecurityToken(
              expires: DateTime.Now.AddHours(1),
              claims: authClaims,
              signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
              );

            var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
            
            return toReturn;
        }
    }
}
