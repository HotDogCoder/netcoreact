using System;
using GeoApi.Domain.Entities;

namespace GeoApi.Application.RepositoriesInterfaces
{
	public interface ISurveyRepository
	{
        IEnumerable<Survey> GetAllSurveys();
        Survey GetSurveyById(int id);
        void AddSurvey(Survey survey);
        Survey UpdateSurvey(SurveyDto surveyDto);
        void DeleteSurvey(int id);
    }
}

