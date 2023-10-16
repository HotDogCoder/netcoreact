using Microsoft.EntityFrameworkCore;
using GeoApi.Domain.Entities;
using GeoApi.Domain.Configuration;
using GeoApi.Infrastructure.Configuration;

namespace GeoApi.Infrastructure.Contexts
{
    public class GeoApiDbContext : DbContext
    {
        public GeoApiDbContext(DbContextOptions<GeoApiDbContext> options) : base(options) { }

        // DbSet properties for your entities go here, e.g.:
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<UserSurveyResponse> UserSurveyResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SurveyConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new SurveyResponseConfiguration());
            modelBuilder.ApplyConfiguration(new UserSurveyResponseConfiguration());
        }

    }
}
