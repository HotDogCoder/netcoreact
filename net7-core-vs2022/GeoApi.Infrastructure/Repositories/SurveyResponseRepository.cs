using System;
using GeoApi.Application.RepositoriesInterfaces;
using GeoApi.Domain.Entities;
using GeoApi.Domain.Models;
using GeoApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Infrastructure.Repositories
{
    public class SurveyResponseRepository : ISurveyResponseRepository
    {
        private readonly GeoApiDbContext _context;

        public SurveyResponseRepository(GeoApiDbContext context)
        {
            _context = context;
        }

        public SurveyResponse AddSurveyResponse(SurveyDto surveyDto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                List<SurveyResponse> list_survey_response = _context.SurveyResponses.Where(
                    sr => surveyDto.Id == sr.SurveyId
                    ).ToList();

                SurveyResponse surveyResponse = new SurveyResponse();
                surveyResponse.UserId = surveyDto.CreatedByUserId;
                surveyResponse.SurveyId = surveyDto.Id;
                surveyResponse.Intent = list_survey_response.Count + 1;
                surveyResponse.UserSurveyResponses = new List<UserSurveyResponse>();

                List<SurveyResponse> response = new List<SurveyResponse>();
                for (int i = 0; i < surveyDto.Questions.Count; i++)
                {
                    for (int ii = 0; ii < surveyDto.Questions[i].Options.Count; ii++)
                    {
                        if (surveyDto.Questions[i].Options[ii].Selected)
                        {
                            UserSurveyResponse usr = new UserSurveyResponse();
                            usr.QuestionId = surveyDto.Questions[i].Id;
                            usr.OptionId = surveyDto.Questions[i].Options[ii].Id;
                            surveyResponse.UserSurveyResponses.Add(usr);
                        }
                    }
                }

                _context.SurveyResponses.Add(surveyResponse);

                _context.SaveChanges();

                transaction.Commit();

                return surveyResponse;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex; // or handle the exception as needed
            }
        }

        public void DeleteSurveyResponse(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SurveyResponse> GetAllUserSurveyReponses(SurveyDto surveyDto)
        {
            return _context.SurveyResponses.Where(
                sr => (surveyDto.user_id == 0 || sr.UserId == surveyDto.user_id)
            )
            .Include(sr => sr.UserSurveyResponses)
                .ThenInclude(usr => usr.Question)
            .Include(sr => sr.UserSurveyResponses)
                .ThenInclude(usr => usr.Option)
            .ToList();
        }

        public IEnumerable<RankingModel> GetRankingModels()
        {
            var rankings = new List<RankingModel>();

            // Assuming _context is your DbContext instance
            var topSurvey = _context.Surveys
                                    .Include(s => s.SurveyResponses)
                                    .OrderByDescending(s => s.SurveyResponses.Count)
                                    .FirstOrDefault();

            if (topSurvey != null)
            {
                rankings.Add(new RankingModel
                {
                    Value = topSurvey.SurveyResponses.Count,
                    Title = "Más Intentos",
                    Type = RankingType.top_ranking,
                    Survey = topSurvey
                });
            }


            var topSurveyRanking = _context.Surveys
            .Include(s => s.SurveyResponses)
            .Select(s => new
            {
                SurveyId = s.Id,
                SurveyTitle = s.Title,
                UniqueUserResponseCount = s.SurveyResponses.Select(sr => sr.UserId).Distinct().Count()
            })
            .OrderByDescending(item => item.UniqueUserResponseCount)
            .FirstOrDefault();

            if (topSurveyRanking != null)
            {
                Survey survey = _context.Surveys.Find(topSurveyRanking.SurveyId);
                rankings.Add(new RankingModel
                {
                    Value = topSurveyRanking.UniqueUserResponseCount,
                    Title = "Mas respondido por usuarios",
                    Type = RankingType.user_ranking,
                    Survey = survey
                });
            }
            /* var userSurveys = _context.SurveyResponses
                                      .GroupBy(sr => sr.UserId)
                                      .Select(g => g.OrderByDescending(sr => sr.Intent).FirstOrDefault())
                                      .ToList();

            foreach (var userSurvey in userSurveys)
            {
                Survey survey = _context.Surveys.Find(userSurvey.SurveyId);
                if(survey != null)
                {
                    rankings.Add(new RankingModel
                    {
                        Value = 1,  // Since it's one response per user
                        Title = $"Mas respondido",
                        Type = RankingType.user_ranking,
                        Survey = survey
                    });
                }
                
            }*/

            return rankings;
        }


        public SurveyResponse GetSurveyResponseById(int id)
        {
            return _context.SurveyResponses.Where(sr => sr.Id == id)
                .Include(sr => sr.UserSurveyResponses)
                    .ThenInclude(usr => usr.Question)
                .Include(sr => sr.UserSurveyResponses)
                    .ThenInclude(usr => usr.Option)
                .FirstOrDefault();
        }

        public void UpdateSurveyResponse(Survey survey)
        {
            throw new NotImplementedException();
        }
    }
}

