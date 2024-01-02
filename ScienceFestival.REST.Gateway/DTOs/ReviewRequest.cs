namespace ScienceFestival.REST.Gateway.DTOs
{
    public class ReviewRequest
    {
        public string ShowId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
