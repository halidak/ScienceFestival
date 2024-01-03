using Microsoft.IdentityModel.Tokens;
using ScienceFestival.REST.Gateway.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScienceFestival.REST.Gateway.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Token:Secret").Value));

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
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

        public string ExtractJuryIdFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var juryIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "your-claim-type-for-jury-id");

                if (juryIdClaim != null)
                {
                    return juryIdClaim.Value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
