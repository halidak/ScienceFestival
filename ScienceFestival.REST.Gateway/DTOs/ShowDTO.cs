namespace ScienceFestival.REST.Gateway.DTOs
{
    public class ShowDTO
    {
        public string Name { get; set; }
        public string[] Performers { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
