using FluentValidation;
using FluentValidation.Results;

namespace CustomerFeedback.Endpoints
{
    public static class ValidateResult
    {
        public static IEnumerable<string> Validate<T>(IValidator<T> validator, T verifyAgainst)
        {
            ValidationResult results = validator.Validate(verifyAgainst);

            return results.Errors.Select(x => x.ErrorMessage);
        }
    }
}
