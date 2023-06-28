using CustomerFeedback.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerFeedback.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;

    }
}
