using FluentValidation;

namespace Check.Request;

public class DateRangeRequestValidator : AbstractValidator<DateRangeRequest>
{
    DateRangeRequestValidator()
    {
        RuleFor(r => r.StartDate).NotEmpty().NotEmpty()
            .LessThanOrEqualTo(r => r.EndDate)
            .WithMessage("Start date should be less than end date. Not be null, or empty");
        RuleFor(r => r.EndDate).NotEmpty().NotEmpty()
            .GreaterThanOrEqualTo(r => r.StartDate)
            .WithMessage("End date should be greater than start date. Not be null, or empty");
    }
}