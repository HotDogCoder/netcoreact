using System;
using GeoApi.Application.RepositoriesInterfaces;
using GeoApi.Application.ServicesInterfaces;
using GeoApi.Domain.Entities;
using GeoApi.Domain.Models;

namespace GeoApi.Application.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyResponseRepository _SurveyResponseRepository;

        public SurveyService(ISurveyRepository surveyRepository, ISurveyResponseRepository SurveyResponseRepository)
        {
            _surveyRepository = surveyRepository;
            _SurveyResponseRepository = SurveyResponseRepository;
        }

        public IEnumerable<Survey> GetAllSurveys()
        {
            return _surveyRepository.GetAllSurveys();
        }

        public Survey GetSurveyById(int id)
        {
            return _surveyRepository.GetSurveyById(id);
        }

        public void AddSurvey(Survey survey)
        {
            _surveyRepository.AddSurvey(survey);
        }

        public Survey UpdateSurvey(SurveyDto surveyDto)
        {
            return _surveyRepository.UpdateSurvey(surveyDto);
        }

        public void DeleteSurvey(int id)
        {
            _surveyRepository.DeleteSurvey(id);
        }

        public SurveyResponse AddSurveyResponse(SurveyDto surveyDto)
        {
            SurveyResponse surveyResponse = _SurveyResponseRepository.AddSurveyResponse(surveyDto);

            return _SurveyResponseRepository.GetSurveyResponseById(surveyResponse.Id);
        }

        public IEnumerable<RankingModel> GetRankingModels()
        {
            return _SurveyResponseRepository.GetRankingModels();
        }
    }
}

