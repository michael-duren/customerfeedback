using CustomerFeedback.Models;
using FluentValidation;

namespace CustomerFeedback.Endpoints.Feedback
{
    public class FeedbackValidator : AbstractValidator<Models.Feedback>
    {
        public FeedbackValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Title must be at least 5 characters long");
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Description must be at least 10 characters long");
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must be provided");
        }
    }
}
