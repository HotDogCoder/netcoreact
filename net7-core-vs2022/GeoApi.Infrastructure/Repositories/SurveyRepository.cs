using System;
using GeoApi.Application.RepositoriesInterfaces;
using GeoApi.Domain.Entities;
using GeoApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Infrastructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly GeoApiDbContext _context;

        public SurveyRepository(GeoApiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Survey> GetAllSurveys()
        {
            return _context.Surveys.Include(s => s.Questions).ThenInclude(q => q.Options).ToList();
        }

        public Survey GetSurveyById(int id)
        {
            return _context.Surveys.FirstOrDefault(s => s.Id == id);
        }

        public void AddSurvey(Survey survey)
        {
            _context.Surveys.Add(survey);
            _context.SaveChanges();
        }

        public Survey UpdateSurvey(SurveyDto surveyDto)
        {
            var existingSurvey = _context.Surveys
                .Where(s => s.Id == surveyDto.Id)
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Options).FirstOrDefault();

            if (existingSurvey == null)
            {
                throw new Exception("Survey not found");
            }

            // Update main properties
            existingSurvey.Title = surveyDto.Title;
            //... other properties ...

            var updatedQuestionIds = surveyDto.Questions.Select(q => q.Id).ToList();

            // Remove questions not found in the DTO
            existingSurvey.Questions.RemoveAll(q => !updatedQuestionIds.Contains(q.Id));

            foreach (var questionDto in surveyDto.Questions)
            {
                if (questionDto.Id == 0)
                {
                    var newQuestion = new Question
                    {
                        Text = questionDto.Text,
                        Options = questionDto.Options.Select(o => new Option
                        {
                            Text = o.Text,
                        }).ToList()
                    };
                    existingSurvey.Questions.Add(newQuestion);
                }
                else
                {
                    var existingQuestion = existingSurvey.Questions.FirstOrDefault(q => q.Id == questionDto.Id);
                    existingQuestion.Text = questionDto.Text;
                    var updatedOptionIds = questionDto.Options.Select(o => o.Id).ToList();

                    existingQuestion.Options.RemoveAll(o => !updatedOptionIds.Contains(o.Id));

                    foreach (var optionDto in questionDto.Options)
                    {
                        if (optionDto.Id == 0)
                        {
                            var newOption = new Option
                            {
                                Text = optionDto.Text,
                            };
                            existingQuestion.Options.Add(newOption);
                        }
                        else
                        {
                            var existingOption = existingQuestion.Options.FirstOrDefault(o => o.Id == optionDto.Id);
                            existingOption.Text = optionDto.Text;
                        }
                    }
                }
            }

            _context.SaveChanges();

            return existingSurvey;
        }


        public void DeleteSurvey(int id)
        {
            Survey survey = _context.Surveys.Find(id);
            if (survey != null)
            {
                _context.Surveys.Remove(survey);
                _context.SaveChanges();
            }
        }
    }
}

