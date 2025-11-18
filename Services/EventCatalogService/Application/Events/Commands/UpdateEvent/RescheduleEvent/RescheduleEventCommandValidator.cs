using Application.Events.Commands.UpdateEvent.RescheduleEvent;
using Application.Validations.Helpers;
using FluentValidation;

namespace Application.Validations.UpdateEventValidations;

public class RescheduleEventCommandValidator : AbstractValidator<RescheduleEventCommand>
{
    public RescheduleEventCommandValidator()
    {
        RuleFor(p => p.Id).Must(ValidationIdHelper.BeAValidObjectId).NotEmpty().NotNull().WithMessage("EventId is required.");
        RuleFor(p => p.NewStartDate).NotEmpty().NotNull().WithMessage("New start date is required.");
        RuleFor(p => p.NewEndDate).NotEmpty().NotNull().WithMessage("New end date is required.");
    }
}