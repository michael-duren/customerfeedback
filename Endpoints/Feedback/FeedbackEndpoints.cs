using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
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
                    async (AppDbContext context, IMapper mapper) =>
                    {
                        return await context.Feedbacks
                            .ProjectTo<FeedbackDto>(mapper.ConfigurationProvider)
                            .ToListAsync();
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
                        Feedback newFeedback,
                        IMapper mapper
                    ) =>
                    {
                        IEnumerable<string> validatorResult = Validate(validator, newFeedback);

                        if (validatorResult.Any())
                            return Results.BadRequest(validatorResult);
                        var feedback = await context.FindAsync<Feedback>(id);
                        if (feedback == null)
                            return Results.NotFound();

                        mapper.Map(newFeedback, feedback);
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
