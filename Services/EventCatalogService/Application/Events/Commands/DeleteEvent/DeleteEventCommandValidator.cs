using Application.Events.Commands.DeleteEvent;
using Application.Validations.Helpers;
using FluentValidation;

namespace Application.Validations.DeleteEventValidator;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator()
    {
        RuleFor(p => p.Id).Must(ValidationIdHelper.BeAValidObjectId).NotEmpty().NotNull().WithMessage("Id is required.");
    }
}