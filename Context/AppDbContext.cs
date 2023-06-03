using CustomerFeedback.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerFeedback.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

		public DbSet<Feedback> Feedbacks { get; set; } = null!;
	}
}