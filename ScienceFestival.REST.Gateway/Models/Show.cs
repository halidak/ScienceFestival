namespace ScienceFestival.REST.Gateway.Models
{
    public class Show
    {
        public string _Id { get; set; }
        public string Name { get; set; }
        public string[] Performers { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }
        public double AvgRating { get; set; }
    }
}
