using Microsoft.EntityFrameworkCore;
using ScienceFestival.REST.Persistance;

namespace ScienceFestival.REST
{
    public class MigrateDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<UserDbContext>());
        }

        private static void SeedData(UserDbContext? userDbContext)
        {
            Console.WriteLine("Applying Migrations...");
            userDbContext?.Database.Migrate();
        }
    }
}
