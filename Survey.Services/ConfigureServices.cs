using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Repositories;

namespace Survey.Services
{
    public class SurveyServices
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureRepositories.ConfigureServices(services, Configuration);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddAutoMapper(typeof(SurveyServices));
        }
    }
}
