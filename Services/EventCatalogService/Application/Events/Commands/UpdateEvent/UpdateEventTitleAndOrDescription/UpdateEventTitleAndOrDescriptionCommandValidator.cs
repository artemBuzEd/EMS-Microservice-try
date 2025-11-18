using Application.Events.Commands.UpdateEvent.UpdateEventTitleAndOrDescription;
using Application.Validations.Helpers;
using FluentValidation;

namespace Application.Validations.UpdateEventValidations;

public class UpdateEventTitleAndOrDescriptionCommandValidator : AbstractValidator<UpdateEventTitleAndOrDescriptionCommand>
{
    public UpdateEventTitleAndOrDescriptionCommandValidator()
    {
        RuleFor(p => p.Id).Must(ValidationIdHelper.BeAValidObjectId).NotEmpty().NotNull().WithMessage("Id is required.");
        RuleFor(p => p.Title).MinimumLength(4).NotEmpty().WithMessage("Title is required. Minimum length is 4.");
        RuleFor(p => p.Description).MinimumLength(10).NotEmpty().WithMessage("Description is required. Minimum length is 10.");
    }
}