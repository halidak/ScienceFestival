using System.ComponentModel.DataAnnotations;

namespace ScienceFestival.REST.Gateway.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
