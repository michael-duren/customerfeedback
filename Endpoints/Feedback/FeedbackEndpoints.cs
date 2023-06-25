using AutoMapper;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Repository.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
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
                .Produces(StatusCodes.Status204NoContent).Produces(400)
                .RequireAuthorization("admin_access");
        }

        private static async Task<IResult> GetAllFeedback(IFeedbackRepository context, IMapper mapper, UserManager<AppUser> userManager)
        {
            var feedbackList = await context.GetAllAsync();
            List<FeedbackDto> feedbackDtoList = new();
            foreach (var feedback in feedbackList)
            {
                if (feedback.UserId == null) continue;
                var user = await userManager.FindByIdAsync(feedback.UserId);
                var feedbackDto = mapper.Map<FeedbackDto>(feedback);
                if (user is not null) feedbackDto.UserName = user.UserName!;
                feedbackDtoList.Add(feedbackDto);
            }
                
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
            Models.Feedback newFeedbackDto,
            IMapper mapper)
        {
            var validatorResult = Validate(validator, newFeedbackDto);

            var validationArr = validatorResult as string[] ?? validatorResult.ToArray();
            if (validationArr.Any())
                return Results.BadRequest(validationArr);
            
            var feedback = await context.GetSingleAsync(id);
            if (feedback is null)
                return Results.NotFound();

            var newFeedback =  mapper.Map(newFeedbackDto, feedback);
            await context.UpdateAsync(newFeedback);
            await context.SaveAsync();

            return Results.Ok();
        }

        private static async Task<IResult> DeleteFeedback(IFeedbackRepository context, int id)
        {
            var feedback = await context.GetSingleAsync(id);
            if (feedback is null)
                return Results.NotFound();
            
            await context.RemoveAsync(feedback);
            await context.SaveAsync();
            return Results.Ok(204);
        }
    }
}