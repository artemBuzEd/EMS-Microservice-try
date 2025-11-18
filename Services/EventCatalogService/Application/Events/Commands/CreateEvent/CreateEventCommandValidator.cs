using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.UpdateEvent;
using FluentValidation;

namespace Application.Validations.CreateEventValidations;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(p => p.Title).MinimumLength(4).NotEmpty().WithMessage("Title is required. Minimum length is 4.");
        RuleFor(p => p.Description).MinimumLength(10).NotEmpty().WithMessage("Description is required. Minimum length is 10.");
        RuleFor(p => p.DateRange.Start).NotEmpty().WithMessage("Date is required.");
        RuleFor(p => p.DateRange.End).NotEmpty().WithMessage("Date is required.");
        RuleFor(p => p.Location.Address).MinimumLength(4).NotEmpty().WithMessage("Location is required. Min length is 4.");
        RuleFor(p => p.Location.City).NotEmpty().WithMessage("City is required.");
        RuleFor(p => p.Location.Country).NotEmpty().WithMessage("Country is required.");
        RuleFor(p => p.OrganizerId).NotEmpty().NotNull().WithMessage("Organizer is required.");
        RuleFor(p => p.VenueId).NotEmpty().NotNull().WithMessage("Venue is required.");
        RuleFor(p => p.Capacity).GreaterThan(0).WithMessage("Capacity is required.");
    }
}