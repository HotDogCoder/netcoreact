using System.IO;
using GeoApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GeoApi.Infrastructure.Factories
{
    public class GeoApiDbContextFactory : IDesignTimeDbContextFactory<GeoApiDbContext>
    {
        public GeoApiDbContext CreateDbContext(string[] args)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "../GeoApi.Presentation");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("megane");

            var optionsBuilder = new DbContextOptionsBuilder<GeoApiDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new GeoApiDbContext(optionsBuilder.Options);
        }

    }
}
