using System;
namespace GeoApi.Domain.Entities
{
    public class QuestionDto
	{
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public int SurveyId { get; set; }
        public List<OptionDto> Options { get; set; }
        public string? ReponseText { get; set; }
    }
}

