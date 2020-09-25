using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Survey.Entities;
using Survey.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Repositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetLogin(string UserName, string Password);
        Task<IList<string>> GetRoles(ApplicationUser user);
        List<Page> GetPages();
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationDbContext applicationDbContext;

        public UserRepository(UserManager<ApplicationUser> UserManager,IApplicationDbContext ApplicationDbContext)
        {
            userManager = UserManager;
            applicationDbContext = ApplicationDbContext;
        }

        public List<Page> GetPages()
        {
            return applicationDbContext.Pages.Include(x=>x.Survey)
                .Select(x => new Page
                {
                    Id = x.Id,
                    SurveyId = x.SurveyId,
                    CreatedBy = x.CreatedBy,
                    CreatedById = x.CreatedById,
                    CreatedOn = x.CreatedOn,
                    Description = x.Description,
                    IsDeleted = x.IsDeleted,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedById = x.ModifiedById,
                    ModifiedOn = x.ModifiedOn,
                    Order = x.Order,
                    Title = x.Title,
                    Survey = new Entities.Tables.Survey
                    {
                        IsDeleted = x.Survey.IsDeleted,
                        ModifiedBy = x.Survey.ModifiedBy,
                        ModifiedById = x.Survey.ModifiedById,
                        ModifiedOn = x.Survey.ModifiedOn,
                        CreatedById = x.Survey.CreatedById,
                        CreatedOn = x.Survey.CreatedOn,
                        CreatedBy = x.Survey.CreatedBy,
                        Title = x.Survey.Title,
                        Description = x.Survey.Description,
                        Id = x.Survey.Id
                    }
                })
                .ToList();
        }
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await userManager.Users.ToListAsync();
        }
        public async Task<ApplicationUser> GetLogin(string Username, string Password)
        {
            var user = await userManager.FindByNameAsync(Username);
            if (user != null && await userManager.CheckPasswordAsync(user, Password))
            {
                return user;
            }
            return null;
        }
        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }
    }
}
