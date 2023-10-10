using System;
namespace GeoApi.Domain.Entities
{
	public class UserSurveyResponse
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string TextAnswer { get; set; }
        public List<Option> SelectedOptions { get; set; }
    }
}

