using Application.Events.Commands.UpdateEvent.ChangeLocationEvent;
using Application.Validations.Helpers;
using FluentValidation;

namespace Application.Validations.UpdateEventValidations;

public class ChangeLocationEventCommandValidator : AbstractValidator<ChangeLocationEventCommand>
{
    public ChangeLocationEventCommandValidator()
    {
        RuleFor(p => p.Id).Must(ValidationIdHelper.BeAValidObjectId).NotEmpty().NotNull().WithMessage("Id is required.");
        RuleFor(p => p.Location.Address).NotEmpty().NotNull().WithMessage("Location is required.");
        RuleFor(p => p.Location.City).NotEmpty().NotNull().WithMessage("Location is required.");
        RuleFor(p => p.Location.Country).NotEmpty().NotNull().WithMessage("Location is required.");
    }
}