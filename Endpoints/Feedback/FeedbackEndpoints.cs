using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerFeedback.Context;
using CustomerFeedback.Models.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static CustomerFeedback.Endpoints.ValidateResult;

namespace CustomerFeedback.Endpoints.Feedback
{
    public static class FeedbackEndpoint
    {
        public static void MapFeedbackEndpoints(this WebApplication app)
        {
            app.MapGet("/api/feedback", GetFeedback)
                .WithName("GetCoupons")
                .Produces<List<Models.Feedback>>(statusCode: 200, contentType: "application/json")
                .AllowAnonymous();

            app.MapPost("/api/feedback/", CreateFeedback)
                .WithName("CreateFeedback")
                .Accepts<FeedbackDto>("application/json")
                .Produces(statusCode: 201, contentType: "application/json").Produces(400);


            app.MapPut("/api/feedback/{id}", UpdateFeedback)
                .WithName("UpdateFeedback")
                .Accepts<FeedbackDto>("application/json")
                .Produces(StatusCodes.Status201Created).Produces(400);

            app.MapDelete("/api/feedback/{id}", DeleteFeedback)
                .WithName("DeleteFeedback")
                .Produces(StatusCodes.Status204NoContent).Produces(400);
        }

        private static async Task<IResult> GetFeedback(AppDbContext context, IMapper mapper)
        {
            var feedback = await context.Feedbacks
                .ProjectTo<FeedbackDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return Results.Ok(feedback);
        }

        private static async Task<IResult> CreateFeedback(AppDbContext context, IValidator<Models.Feedback> validator,
            Models.Feedback feedback,
            HttpContext httpContext)
        {
            var validatorResult = Validate(validator, feedback);

            if (validatorResult.Any())
                return Results.BadRequest(validatorResult);

            await context.Feedbacks.AddAsync(feedback);
            await context.SaveChangesAsync();
            return Results.Created((string)httpContext.Request.Path, feedback);
        }

        private static async Task<IResult> UpdateFeedback(AppDbContext context,
            IValidator<Models.Feedback> validator,
            int id,
            Models.Feedback newFeedback,
            IMapper mapper)
        {
            var validatorResult = Validate(validator, newFeedback);

            if (validatorResult.Any())
                return Results.BadRequest(validatorResult);
            var feedback = await context.FindAsync<Models.Feedback>(id);
            if (feedback == null)
                return Results.NotFound();

            mapper.Map(newFeedback, feedback);
            await context.SaveChangesAsync();

            return Results.Ok();
        }

        private static async Task<IResult> DeleteFeedback(AppDbContext context, int id)
        {
            var feedback = await context.FindAsync<Models.Feedback>(id);
            if (feedback == null)
                return Results.NotFound();
            context.Remove(feedback);
            await context.SaveChangesAsync();
            return Results.Ok(204);
        }
    }
}