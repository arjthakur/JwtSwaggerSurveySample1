using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Entities;
using System;

namespace Survey.Repositories
{
    public class ConfigureRepositories
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureEntities.ConfigureServices(services, Configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

        }
    }
}
