using System.Text;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomerFeedback.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    opt.Password.RequiredLength = 8;
                    opt.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // this key needs to match what we have in our token service
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        // this will validate that the token we get is valid
                        ValidateIssuerSigningKey = true,
                        // key created above
                        IssuerSigningKey = key,
                        // we are not currently validating these, but will in future
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization(opt =>
            {
                opt.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                opt.AddPolicy("admin_access", policy =>
                {
                    policy
                        .RequireRole("Admin")
                        .RequireClaim("Roles", "Admin");
                });
            });

            services.AddScoped<TokenService>();

            return services;
        }
    }
}
