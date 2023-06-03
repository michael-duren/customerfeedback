using CustomerFeedback.Models;
using CustomerFeedback.Context;
using Microsoft.EntityFrameworkCore;

namespace CustomerFeedback
{
    public class Seed
    {
        public static async Task SeedData(AppDbContext context)
        {
            if (await context.Feedbacks.AnyAsync())
                return;

            var feedbacks = new List<Feedback>
            {
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Great App",
                    Description = "I love this app, it's so easy to use and it's really helpful",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
            };

            foreach (var feedback in feedbacks)
            {
                context.Feedbacks.Add(feedback);
            }

            await context.SaveChangesAsync();
        }
    }
}
