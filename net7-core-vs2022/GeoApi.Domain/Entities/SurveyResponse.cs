using System;
namespace GeoApi.Domain.Entities
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Intent { get; set; }
        public int SurveyId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Survey Survey { get; set; }
        public List<UserSurveyResponse> UserSurveyResponses { get; set; }
    }
}