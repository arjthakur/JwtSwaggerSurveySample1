using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Survey.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Repositories
{
    public interface ISurveyRepository
    {
        public Task<List<Entities.Tables.Survey>> Get();
        public Task<Entities.Tables.Survey> Get(long Id);
        public ValueTask<EntityEntry<Entities.Tables.Survey>> AddAsync(Entities.Tables.Survey survey);
        public EntityEntry<Entities.Tables.Survey> UpdateAsync(Entities.Tables.Survey survey);
        public Task<int> Commit();
    }
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IApplicationDbContext applicationDbContext;

        public SurveyRepository(IApplicationDbContext ApplicationDbContext)
        {
            applicationDbContext = ApplicationDbContext;
        }
        public Task<List<Entities.Tables.Survey>> Get()
        {
            return applicationDbContext.Surveys.Where(x=>!x.IsDeleted).ToListAsync();
        }
        public Task<Entities.Tables.Survey> Get(long Id)
        {
            return applicationDbContext.Surveys.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();
        }
        public ValueTask<EntityEntry<Entities.Tables.Survey>> AddAsync(Entities.Tables.Survey survey)
        {
            return applicationDbContext.Surveys.AddAsync(survey);
        }
        public EntityEntry<Entities.Tables.Survey> UpdateAsync(Entities.Tables.Survey survey)
        {
            return applicationDbContext.Surveys.Update(survey);
        }
        public Task<int> Commit()
        {
            return applicationDbContext.Commit();
        }
    }
}
