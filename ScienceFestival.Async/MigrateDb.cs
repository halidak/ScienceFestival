using Microsoft.EntityFrameworkCore;
using ScienceFestival.Async.Persistance;

namespace ScienceFestival.Async
{
    public class MigrateDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext? appDbContext)
        {
            Console.WriteLine("Applying Migrations...");
            appDbContext?.Database.Migrate();
        }
    }
}
