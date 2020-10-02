using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Entities;
using System;

namespace Survey.Repositories
{
    public static class ConfigureRepositories
    {
        public static IServiceCollection ConfigureRepositoryServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.ConfigureEntityServices(Configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            return services;
        }
    }
}
