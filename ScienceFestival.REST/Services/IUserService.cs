using Microsoft.AspNetCore.Identity;
using ScienceFestival.REST.DTOs;
using ScienceFestival.REST.Models;

namespace ScienceFestival.REST.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDTO userRegisterDTO);

        //login user but return user and jwt token
        Task<object> Login(UserLoginDTO userLoginDTO);
    }
}
