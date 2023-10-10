using Microsoft.EntityFrameworkCore;
using GeoApi.Domain.Entities;

namespace GeoApi.Infrastructure.Contexts
{
    public class GeoApiDbContext : DbContext
    {
        public GeoApiDbContext(DbContextOptions<GeoApiDbContext> options) : base(options) { }

        // DbSet properties for your entities go here, e.g.:
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
