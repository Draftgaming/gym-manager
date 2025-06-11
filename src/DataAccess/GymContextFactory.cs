using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
    public class GymContextFactory : IDesignTimeDbContextFactory<GymContext>
    {
        public GymContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GymContext>();

            builder.UseSqlite("Data Source=./Data/gym.db");

            return new GymContext(builder.Options);
        }
    }
}
