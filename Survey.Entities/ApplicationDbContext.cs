using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Survey.Common;
using Survey.Entities.Tables;
using System.Threading;
using System.Threading.Tasks;

namespace Survey.Entities
{

    public interface IApplicationDbContext
    {
        DbSet<Tables.Survey> Surveys { get; set; }
        DbSet<Page> Pages { get; set; }
        public Task<int> CommitAsync(CancellationToken token);

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRoles, long>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Tables.Survey> Surveys { get; set; }
        public DbSet<Page> Pages { get; set; }
        public async Task<int> CommitAsync(CancellationToken token)
        {
            return await this.SaveChangesAsync(token);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Email = SeedConstants.SuperAdminEmail,
                    NormalizedEmail = SeedConstants.SuperAdminEmail.ToUpper(),
                    Id = SeedConstants.SeedId,
                    EmailConfirmed = true,
                    UserName = SeedConstants.UserName,
                    NormalizedUserName = SeedConstants.UserName.ToUpper(),
                    PasswordHash = SeedConstants.PasswordHash,
                    FirstName = SeedConstants.FirstName,
                    LastName = SeedConstants.LaststName,
                    PhoneNumber = SeedConstants.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = System.Guid.NewGuid().ToString("D"),
                    CreatedById = 10001,
                    CreatedOn = SeedConstants.SeedDate,
                    ModifiedById = 10001,
                    ModifiedOn = SeedConstants.SeedDate,
                    IsDeleted = false,
                });

            builder.Entity<ApplicationUser>().HasOne(x => x.CreatedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<ApplicationUser>().HasOne(x => x.ModifiedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);


            int RoleId = 1;
            foreach (var role in SeedConstants.Roles.Split(','))
            {
                builder.Entity<ApplicationRoles>().HasData(
                    new ApplicationRoles
                    {
                        Id = (RoleId++),
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        CreatedById = 10001,
                        CreatedOn = SeedConstants.SeedDate,
                        ModifiedById = 10001,
                        ModifiedOn = SeedConstants.SeedDate,
                        IsDeleted = false,
                    });
            }

            builder.Entity<ApplicationRoles>().HasOne(x => x.CreatedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<ApplicationRoles>().HasOne(x => x.ModifiedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<IdentityUserRole<long>>().HasData(
                new IdentityUserRole<long>
                {
                    UserId = 10001,
                    RoleId = 1
                });


            builder.Entity<Tables.Survey>().HasData(
                new Tables.Survey
                {
                    Id = 1,
                    CreatedById = 10001,
                    CreatedOn = SeedConstants.SeedDate,
                    Description = "Sample Survey Seed",
                    ModifiedById = 10001,
                    ModifiedOn = SeedConstants.SeedDate,
                    IsDeleted = false,
                    Title = "Sample Survey Seed"
                });

            builder.Entity<Tables.Survey>().HasOne(x => x.CreatedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<Tables.Survey>().HasOne(x => x.ModifiedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Page>().HasData(
                new Page
                {
                    Id = 1,
                    SurveyId = 1,
                    Order = 1,
                    CreatedById = 10001,
                    CreatedOn = SeedConstants.SeedDate,
                    Description = "Sample Page Seed",
                    ModifiedById = 10001,
                    ModifiedOn = SeedConstants.SeedDate,
                    IsDeleted = false,
                    Title = "Sample Page Seed",
                });
            builder.Entity<Page>().HasOne(x => x.Survey).WithMany(x => x.Pages).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Page>().HasOne(x => x.CreatedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<Page>().HasOne(x => x.ModifiedBy).WithMany().OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(builder);
        }
    }
}
