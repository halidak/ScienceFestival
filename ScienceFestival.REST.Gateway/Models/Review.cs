namespace ScienceFestival.REST.Gateway.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ShowId { get; set; }
        public string JuryId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
