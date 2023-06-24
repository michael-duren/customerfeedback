using AutoMapper;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Repository.IRepository;
using FluentValidation;
using static CustomerFeedback.Endpoints.ValidateResult;

namespace CustomerFeedback.Endpoints.Feedback
{
    public static class FeedbackEndpoint
    {
        public static void MapFeedbackEndpoints(this WebApplication app)
        {
            app.MapGet("/api/feedback", GetAllFeedback)
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
                .Produces(StatusCodes.Status204NoContent).Produces(400);

            app.MapDelete("/api/feedback/{id}", DeleteFeedback)
                .WithName("DeleteFeedback")
                .Produces(StatusCodes.Status204NoContent).Produces(400);
        }

        private static async Task<IResult> GetAllFeedback(IFeedbackRepository context, IMapper mapper)
        {
            var feedbackList = await context.GetAllAsync();
            var feedbackDtoList = mapper.Map<List<FeedbackDto>>(feedbackList);
                
            return Results.Ok(feedbackDtoList);
        }

        private static async Task<IResult> CreateFeedback(IFeedbackRepository context, IValidator<Models.Feedback> validator,
            Models.Feedback feedback,
            HttpContext httpContext)
        {
            var validatorResult = Validate(validator, feedback);

            var validationArr = validatorResult as string[] ?? validatorResult.ToArray();
            if (validationArr.Any())
                return Results.BadRequest(validationArr);

            await context.CreateAsync(feedback);
            await context.SaveAsync();
            return Results.Created(httpContext.Request.Path, feedback);
        }

        private static async Task<IResult> UpdateFeedback(IFeedbackRepository context,
            IValidator<Models.Feedback> validator,
            int id,
            Models.Feedback newFeedback,
            IMapper mapper)
        {
            var validatorResult = Validate(validator, newFeedback);

            var validationArr = validatorResult as string[] ?? validatorResult.ToArray();
            if (validationArr.Any())
                return Results.BadRequest(validationArr);
            
            var feedback = await context.GetSingleAsync(id);
            if (feedback is null)
                return Results.NotFound();

            mapper.Map(newFeedback, feedback);
            await context.SaveAsync();

            return Results.Ok();
        }

        private static async Task<IResult> DeleteFeedback(IFeedbackRepository context, int id)
        {
            Models.Feedback feedback = await context.GetSingleAsync(id);
            if (feedback is null)
                return Results.NotFound();
            
            await context.RemoveAsync(feedback);
            await context.SaveAsync();
            return Results.Ok(204);
        }
    }
}