using ScienceFestival.REST.Gateway.Models;

namespace ScienceFestival.REST.Gateway.Responses
{
    public class LoginResponse
    {
        public DateTime Expires { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
