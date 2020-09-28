using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Survey.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Survey.Repositories
{
    public interface ISurveyRepository
    {
        public Task<List<Entities.Tables.Survey>> Get(CancellationToken token);
        public Task<Entities.Tables.Survey> Get(long Id, CancellationToken token);
        public ValueTask<EntityEntry<Entities.Tables.Survey>> AddAsync(Entities.Tables.Survey survey, CancellationToken token);
        public EntityEntry<Entities.Tables.Survey> UpdateAsync(Entities.Tables.Survey survey);
        public Task<int> Commit(CancellationToken token);
    }
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IApplicationDbContext applicationDbContext;

        public SurveyRepository(IApplicationDbContext ApplicationDbContext)
        {
            applicationDbContext = ApplicationDbContext;
        }
        public Task<List<Entities.Tables.Survey>> Get(CancellationToken token)
        {
            return applicationDbContext.Surveys.Where(x=>!x.IsDeleted).ToListAsync(token);
        }
        public Task<Entities.Tables.Survey> Get(long Id, CancellationToken token)
        {
            return applicationDbContext.Surveys.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync(token);
        }
        public ValueTask<EntityEntry<Entities.Tables.Survey>> AddAsync(Entities.Tables.Survey survey, CancellationToken token)
        {
            return applicationDbContext.Surveys.AddAsync(survey, token);
        }
        public EntityEntry<Entities.Tables.Survey> UpdateAsync(Entities.Tables.Survey survey)
        {
            return applicationDbContext.Surveys.Update(survey);
        }
        public async Task<int> Commit(CancellationToken token)
        {
            return await applicationDbContext.CommitAsync(token);
        }
    }
}
