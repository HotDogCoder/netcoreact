using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoApi.Domain.Entities;

namespace GeoApi.Domain.Configuration
{

    public class SurveyResponseConfiguration : IEntityTypeConfiguration<SurveyResponse>
    {
        public void Configure(EntityTypeBuilder<SurveyResponse> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasMany(s => s.UserSurveyResponses);
        }
    }

}

