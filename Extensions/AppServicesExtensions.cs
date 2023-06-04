using CustomerFeedback.Context;
using CustomerFeedback.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CustomerFeedback.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(
            this IServiceCollection services,
            IConfiguration config,
            string connection
        )
        {
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();

            // add swagger
            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    }
                );
                x.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    }
                );
            });

            // add fluent validation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Feedback>();

            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(connection, options => options.EnableRetryOnFailure())
            );

            return services;
        }
    }
}
