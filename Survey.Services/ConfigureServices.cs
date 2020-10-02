using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Repositories;

namespace Survey.Services
{
    public static class SurveyServices
    {
        public static IServiceCollection ConfigureSurveyServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.ConfigureRepositoryServices(Configuration);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddAutoMapper(typeof(SurveyServices));
            return services;
        }
    }
}
