using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoApi.Domain.Entities;

namespace GeoApi.Domain.Configuration
{

    public class UserSurveyResponseConfiguration : IEntityTypeConfiguration<UserSurveyResponse>
    {
        public void Configure(EntityTypeBuilder<UserSurveyResponse> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.Option);
            builder.HasOne(s => s.Question);
        }
    }

}

