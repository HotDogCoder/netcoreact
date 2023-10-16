using System;
namespace GeoApi.Domain.Entities
{
	public class OptionDto
	{
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Boolean Selected { get; set; }
    }
}

