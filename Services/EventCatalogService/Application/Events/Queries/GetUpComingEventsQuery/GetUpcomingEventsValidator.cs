using FluentValidation;

namespace Application.Events.Queries.GetUpComingEventsQuery;

public class GetUpcomingEventsValidator : AbstractValidator<GetUpcomingEventsQuery>
{
    public GetUpcomingEventsValidator()
    {
        RuleFor(e => e.CategoryNameFilter).NotEmpty().NotNull().WithMessage("Please specify a category name.");
    }
}