using CustomerFeedback.Models;
using CustomerFeedback.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CustomerFeedback
{
    public class Seed
    {
        public static async Task SeedData(
            AppDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            var roles = new[] { "Admin", "Member" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Email = "michael@michael.com",
                        UserName = "MichaelDuren",
                        DisplayName = "Michael",
                    },
                    new AppUser
                    {
                        Email = "daniel@daniel.com",
                        UserName = "DanielDuren",
                        DisplayName = "Daniel"
                    },
                    new AppUser
                    {
                        Email = "laurie@laurie.com",
                        UserName = "LaurieIngram",
                        DisplayName = "Laurie"
                    }
                };

                foreach (var user in users)
                {
                    Console.WriteLine($"SEEDING {user.UserName}");
                    await userManager.CreateAsync(user, "Pa$$w0rd!");
                }
            }

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
                    Title = "It was alright",
                    Description = "It was an okay app, I think it could be better",
                    Rating = 3,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "God Awful",
                    Description = "This app is terrible, I hate it",
                    Rating = 1,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "AMAZING",
                    Description = "this APP ROCKS",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Barely Works",
                    Description = "This app barely works, I can't believe I paid for this",
                    Rating = 2,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "Sort of good....",
                    Description = "This app is okay, I think it could be better",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "SIMPLY AMAZING",
                    Description = "This app is the best app I've ever used, I love it so much",
                    Rating = 5,
                    DateReviewed = DateTime.Now
                },
                new Feedback
                {
                    Title = "GOD AWFUL",
                    Description = "This app is terrible, I hate it",
                    Rating = 1,
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
