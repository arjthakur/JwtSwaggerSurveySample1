using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Survey.DTOs.Intern;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Survey.Api.JWT
{
    /// <summary>
    /// To Enable JWT bearer and Swagger commonly
    /// </summary>
    internal static class JwtAuthSwaggerConfigure
    {
        /// <summary>
        /// Configure JWT and Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        internal static void ConfigureJwtServices(this IServiceCollection services, IConfiguration Configuration)
        {
            // JWT Configuration DI
            var token = Configuration.GetSection("TokenManagement").Get<TokenManagement>();
            services.AddSingleton(token);

            // JWT Authentication setting
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = token.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidAudience = token.Audience,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Swagger UI, Header Authentication setups

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Survey Application ",
                    Description = "Please login using Auth end point and provide token to call Authorize end points",
                    Contact = new OpenApiContact
                    {
                        Name = @"Arjun's Repository",
                        Email = "arjthalur@gmail.com",
                        Url = new Uri("https://google.com/arjthakur")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
        }

        internal static void JwtConfigure(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(configuration.GetSection("VirtualPath").Value + "/swagger/v1/swagger.json", "v1");
                c.DocumentTitle = "Survey App";
                c.DefaultModelsExpandDepth(0);
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthentication();
        }
    }
}
