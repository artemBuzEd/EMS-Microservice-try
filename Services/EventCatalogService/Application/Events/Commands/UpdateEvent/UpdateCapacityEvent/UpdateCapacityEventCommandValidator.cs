using Application.Events.Commands.UpdateEvent.UpdateCapacityEvent;
using Application.Validations.Helpers;
using FluentValidation;

namespace Application.Validations.UpdateEventValidations;

public class UpdateCapacityEventCommandValidator : AbstractValidator<UpdateCapacityEventCommand>
{
    public UpdateCapacityEventCommandValidator()
    {
        RuleFor(p => p.Id).Must(ValidationIdHelper.BeAValidObjectId).NotEmpty().NotNull().WithMessage("Id is required.");
        RuleFor(p => p.Capacity).GreaterThan(0).WithMessage("Capacity is required.");
    }
}