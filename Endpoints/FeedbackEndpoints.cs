using CustomerFeedback.Context;
using CustomerFeedback.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CustomerFeedback.EndpointDefinitions
{
    public static class FeedbackEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet(
                    "/api/feedback",
                    async (AppDbContext context) =>
                    {
                        return await context.Feedbacks.ToListAsync();
                    }
                )
                .AllowAnonymous()
                .Produces<List<Feedback>>(StatusCodes.Status200OK);

            app.MapPost(
                    "/api/feedback/",
                    async (
                        AppDbContext context,
                        IValidator<Feedback> validator,
                        Feedback feedback
                    ) =>
                    {
                        var validationResult = await validator.ValidateAsync(feedback);
                        if (!validationResult.IsValid)
                        {
                            var errors = new
                            {
                                errors = validationResult.Errors.Select(x => x.ErrorMessage)
                            };
                            return Results.BadRequest(errors);
                        }

                        await context.Feedbacks.AddAsync(feedback);
                        await context.SaveChangesAsync();
                        return Results.Ok();
                    }
                )
                .AllowAnonymous();

            // .Produces(StatusCodes.Status201Created);

            app.MapPut(
                    "/api/feedback/{id}",
                    async (AppDbContext context, int id, Feedback newFeedback) =>
                    {
                        var feedback = await context.FindAsync<Feedback>(id);
                        if (feedback == null)
                            return Results.NotFound();
                        feedback.Title = newFeedback.Title;
                        feedback.Description = newFeedback.Description;
                        feedback.Rating = newFeedback.Rating;
                        feedback.DateReviewed = newFeedback.DateReviewed;
                        await context.SaveChangesAsync();

                        return Results.Ok();
                    }
                )
                .AllowAnonymous()
                .Produces(StatusCodes.Status201Created);

            app.MapDelete(
                    "/api/feedback/{id}",
                    async (AppDbContext context, int id) =>
                    {
                        var feedback = await context.FindAsync<Feedback>(id);
                        if (feedback == null)
                            return Results.NotFound();
                        context.Remove(feedback);
                        await context.SaveChangesAsync();
                        return Results.Ok(204);
                    }
                )
                .Produces(StatusCodes.Status204NoContent)
                .AllowAnonymous();
        }
    }
}
