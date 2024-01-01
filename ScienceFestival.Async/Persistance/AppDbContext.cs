using Microsoft.EntityFrameworkCore;
using ScienceFestival.Async.Models;

namespace ScienceFestival.Async.Persistance
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; } = default!;
    }
}
