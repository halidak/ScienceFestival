using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScienceFestival.REST.DTOs;
using ScienceFestival.REST.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScienceFestival.REST.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
      
        //register user
        public async Task<User> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = new User
            {
                UserName = userRegisterDTO.UserName,
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Age = userRegisterDTO.Age,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                Role = userRegisterDTO.Role
            };

            var result = await userManager.CreateAsync(user, userRegisterDTO.Password);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new Exception("Registration failed. Reason: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<object> Login(UserLoginDTO userLoginDTO)
        {
            var user = await userManager.FindByNameAsync(userLoginDTO.UserName);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if(await userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTq"));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                var token = new JwtSecurityToken(
                  expires: DateTime.Now.AddHours(1),
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                  );

                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var obj = new
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn,
                    user = user
                };
                return obj;
            }
            else
            {
                throw new Exception("Username and password not match");
            }
        }

        //TODO: get all performers and get performer by id

        public async Task<List<User>> GetAllPerformers()
        {
            var performers = await userManager.Users.Where(u => u.Role == Role.Performer).ToListAsync();
            return performers;
        }

        //get performer by id
        public async Task<User> GetUserById(string id)
        {
            var performer = await userManager.FindByIdAsync(id);
            if (performer == null)
            {
                throw new Exception("Performer not found.");
            }
            return performer;
        }

        public async Task<List<User>> GetAllJuries()
        {
            var juries = await userManager.Users.Where(u => u.Role == Role.Jury).ToListAsync();
            return juries;
        }
    }
}
