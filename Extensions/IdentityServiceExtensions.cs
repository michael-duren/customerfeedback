using CustomerFeedback.Context;
using CustomerFeedback.Models;

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
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication();

            return services;
        }
    }
}
