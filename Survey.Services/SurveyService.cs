using AutoMapper;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Services
{
    public interface ISurveyService
    {
        Task<List<SurveyDto>> GetAsync();
        Task<SurveyDto> GetAsync(long Id);
        Task<SurveyDto> AddAsync(NewSurvey model, long UserId);
        Task<SurveyDto> UpdateAsync(SurveyDto model, long UserId);
        Task<SurveyDto> DeleteAsync(long Id, long UserId);
    }
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository surveyRepository;
        private readonly IMapper mapper;

        public SurveyService(ISurveyRepository SurveyRepository, IMapper mapper)
        {
            surveyRepository = SurveyRepository;
            this.mapper = mapper;
        }
        public async Task<List<SurveyDto>> GetAsync()
        {
            var users = await surveyRepository.Get();
            return mapper.Map<List<SurveyDto>>(users);
        }
        public async Task<SurveyDto> GetAsync(long Id)
        {
            var users = await surveyRepository.Get(Id);
            return mapper.Map<SurveyDto>(users);
        }
        public async Task<SurveyDto> AddAsync(NewSurvey model,long UserId)
        {
            var survey = mapper.Map<Entities.Tables.Survey>(model);
            survey.CreatedById = UserId;
            survey.ModifiedById = UserId;
            survey.ModifiedOn = DateTime.UtcNow;
            survey.CreatedOn = DateTime.UtcNow;
            await surveyRepository.AddAsync(survey);
            await surveyRepository.Commit();
            return mapper.Map<SurveyDto>(survey);
        }
        public async Task<SurveyDto> UpdateAsync(SurveyDto model, long UserId)
        {
            var survey = await surveyRepository.Get(model.Id);
            survey.Title = model.Title;
            survey.Description = model.Description;
            survey.ModifiedById = UserId;
            survey.ModifiedOn = DateTime.UtcNow;
            surveyRepository.UpdateAsync(survey);
            await surveyRepository.Commit();
            return mapper.Map<SurveyDto>(survey);
        }
        public async Task<SurveyDto> DeleteAsync(long Id, long UserId)
        {
            var survey = await surveyRepository.Get(Id);
            survey.ModifiedById = UserId;
            survey.ModifiedOn = DateTime.UtcNow;
            survey.IsDeleted = true;
            surveyRepository.UpdateAsync(survey);
            await surveyRepository.Commit();
            return mapper.Map<SurveyDto>(survey);
        }
    }
}
