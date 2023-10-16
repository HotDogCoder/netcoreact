using System;
namespace GeoApi.Domain.Entities
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CreatedByUserId { get; set; }
        public List<QuestionDto> Questions { get; set; }
        public int user_id { get; set; }
    }
}

