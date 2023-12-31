using Microsoft.AspNetCore.Mvc;
using ScienceFestival.REST.DTOs;
using ScienceFestival.REST.Services;

namespace ScienceFestival.REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("performer-register")]
        public async Task<IActionResult> PerformerRegister(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await userService.Register(userRegisterDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("performer-login")]
        public async Task<IActionResult> PerformerLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await userService.Login(userLoginDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
