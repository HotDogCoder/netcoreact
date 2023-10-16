using System;
using GeoApi.Domain.Entities;
using GeoApi.Domain.Models;

namespace GeoApi.Application.ServicesInterfaces
{
	public interface ISurveyService
	{
        IEnumerable<Survey> GetAllSurveys();
        Survey GetSurveyById(int id);
        void AddSurvey(Survey survey);
        public SurveyResponse AddSurveyResponse(SurveyDto surveyDto);
        Survey UpdateSurvey(SurveyDto surveyDto);
        void DeleteSurvey(int id);
        IEnumerable<RankingModel> GetRankingModels();
    }
}

