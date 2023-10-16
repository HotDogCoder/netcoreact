using System;
using GeoApi.Domain.Entities;
using GeoApi.Domain.Models;

namespace GeoApi.Application.RepositoriesInterfaces
{
	public interface ISurveyResponseRepository
	{
        IEnumerable<SurveyResponse> GetAllUserSurveyReponses(SurveyDto surveyDto);
        SurveyResponse GetSurveyResponseById(int id);
        SurveyResponse AddSurveyResponse(SurveyDto surveyDto);
        void UpdateSurveyResponse(Survey survey);
        void DeleteSurveyResponse(int id);
        IEnumerable<RankingModel> GetRankingModels();
    }
}

