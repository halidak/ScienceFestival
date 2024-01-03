namespace ScienceFestival.REST.Gateway.Models
{
    public class Show
    {
        public string _Id { get; set; }
        public string Name { get; set; }
        public string Performer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }
    }
}
