using CustomerFeedback.Context;
using CustomerFeedback.Models;
using Microsoft.AspNetCore.Identity;

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
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication();

            return services;
        }
    }
}
