using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Validation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using static CustomerFeedback.Endpoints.ValidateResult;

namespace CustomerFeedback.EndpointDefinitions
{
    public static class FeedbackEndpoint
    {
        public static void MapFeedbackEndpoints(WebApplication app)
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
                        // IValidator<Feedback> validator,
                        Feedback feedback
                    ) =>
                    {
                        FeedbackValidator validator = new();
                        IEnumerable<string> validatorResult = Validate(validator, feedback);

                        if (validatorResult.Any())
                            return Results.BadRequest(validatorResult);

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

                        FeedbackValidator validator = new();
                        IEnumerable<string> validatorResult = Validate(validator, feedback);

                        if (validatorResult.Any())
                            return Results.BadRequest(validatorResult);

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
