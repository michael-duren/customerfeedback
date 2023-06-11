using CustomerFeedback.Context;
using CustomerFeedback.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static CustomerFeedback.Endpoints.ValidateResult;

namespace CustomerFeedback.EndpointDefinitions
{
    public static class FeedbackEndpoint
    {
        public static void MapFeedbackEndpoints(this WebApplication app)
        {
            app.MapGet(
                    "/api/feedback",
                    async (AppDbContext context) =>
                    {
                        return await context.Feedbacks.ToListAsync();
                    }
                )
                .Produces<List<Feedback>>(statusCode: 200, contentType: "application/json")
                .AllowAnonymous();

            app.MapPost(
                "/api/feedback/",
                async (AppDbContext context, IValidator<Feedback> validator, Feedback feedback) =>
                {
                    IEnumerable<string> validatorResult = Validate(validator, feedback);

                    if (validatorResult.Any())
                        return Results.BadRequest(validatorResult);

                    await context.Feedbacks.AddAsync(feedback);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
            );

            // .Produces(StatusCodes.Status201Created);

            app.MapPut(
                    "/api/feedback/{id}",
                    async (
                        AppDbContext context,
                        IValidator<Feedback> validator,
                        int id,
                        Feedback newFeedback
                    ) =>
                    {
                        IEnumerable<string> validatorResult = Validate(validator, newFeedback);

                        if (validatorResult.Any())
                            return Results.BadRequest(validatorResult);
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
                .Produces(StatusCodes.Status204NoContent);
        }
    }
}
