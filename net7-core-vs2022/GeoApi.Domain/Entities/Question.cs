using System;
namespace GeoApi.Domain.Entities
{
    public enum QuestionType
    {
        SingleAnswer,
        MultipleAnswer,
        TextAnswer,
        NumberAnswer
    }

    public class Question
	{
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public int SurveyId { get; set; }
        public Survey? Survey { get; set; }
        public List<Option> Options { get; set; }
    }
}

