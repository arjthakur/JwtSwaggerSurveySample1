using AutoMapper;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survey.Services
{
    public interface ISurveyService
    {
        Task<List<SurveyDto>> GetAsync(CancellationToken token);
        Task<SurveyDto> GetAsync(long Id, CancellationToken token);
        Task<SurveyDto> AddAsync(NewSurvey model, long UserId, CancellationToken token);
        Task<SurveyDto> UpdateAsync(SurveyDto model, long UserId, CancellationToken token);
        Task<SurveyDto> DeleteAsync(long Id, long UserId, CancellationToken token);
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
        public async Task<List<SurveyDto>> GetAsync(CancellationToken token)
        {
            var users = await surveyRepository.Get(token);
            return mapper.Map<List<SurveyDto>>(users);
        }
        public async Task<SurveyDto> GetAsync(long Id, CancellationToken token)
        {
            var users = await surveyRepository.Get(Id, token);
            return mapper.Map<SurveyDto>(users);
        }
        public async Task<SurveyDto> AddAsync(NewSurvey model, long UserId, CancellationToken token)
        {
            var survey = mapper.Map<Entities.Tables.Survey>(model);
            survey.CreatedById = UserId;
            survey.ModifiedById = UserId;
            survey.ModifiedOn = DateTime.UtcNow;
            survey.CreatedOn = DateTime.UtcNow;
            await surveyRepository.AddAsync(survey, token);
            await surveyRepository.Commit(token);
            return mapper.Map<SurveyDto>(survey);
        }
        public async Task<SurveyDto> UpdateAsync(SurveyDto model, long UserId, CancellationToken token)
        {
            var survey = await surveyRepository.Get(model.Id, token);
            survey.Title = model.Title;
            survey.Description = model.Description;
            survey.ModifiedById = UserId;
            survey.ModifiedOn = DateTime.UtcNow;
            surveyRepository.UpdateAsync(survey);
            await surveyRepository.Commit(token);
            return mapper.Map<SurveyDto>(survey);
        }
        public async Task<SurveyDto> DeleteAsync(long Id, long UserId, CancellationToken token)
        {
            var survey = await surveyRepository.Get(Id, token);
            survey.ModifiedById = UserId;
            survey.ModifiedOn = DateTime.UtcNow;
            survey.IsDeleted = true;
            surveyRepository.UpdateAsync(survey);
            await surveyRepository.Commit(token);
            return mapper.Map<SurveyDto>(survey);
        }
    }
}
