using Microsoft.AspNetCore.Identity;
using ScienceFestival.REST.DTOs;
using ScienceFestival.REST.Models;

namespace ScienceFestival.REST.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDTO userRegisterDTO);

        Task<object> Login(UserLoginDTO userLoginDTO);

        //get all performers
        Task<List<User>> GetAllPerformers();

        //get performer by id
        Task<User> GetPerformerById(string id);
    }
}
