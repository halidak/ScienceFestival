using Microsoft.AspNetCore.Identity;
using ScienceFestival.REST.DTOs;
using ScienceFestival.REST.Models;

namespace ScienceFestival.REST.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDTO userRegisterDTO);

        Task<object> Login(UserLoginDTO userLoginDTO);

        Task<List<User>> GetAllPerformers();

        Task<User> GetUserById(string id);

        Task<List<User>> GetAllJuries();
    }
}
