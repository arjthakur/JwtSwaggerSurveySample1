using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.Entities
{
    public static class ConfigureEntities
    {
        public static IServiceCollection ConfigureEntityServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("MyAppConnection")));
            services.AddIdentityCore<ApplicationUser>().AddRoles<ApplicationRoles>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }
    }
}
