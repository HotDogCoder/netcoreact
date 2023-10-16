using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoApi.Domain.Entities;

namespace GeoApi.Domain.Configuration
{

    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasMany(s => s.Questions)
                   .WithOne(q => q.Survey)
                   .HasForeignKey(q => q.SurveyId);
        }
    }

    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);
        }
    }
}

