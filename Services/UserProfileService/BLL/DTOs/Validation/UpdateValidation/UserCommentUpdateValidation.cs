using BLL.DTOs.Request.UserComment;
using FluentValidation;

namespace BLL.DTOs.Validation.UpdateValidation;

public class UserCommentUpdateValidation : AbstractValidator<UserCommentUpdateRequestDTO>
{
    public UserCommentUpdateValidation()
    {
        RuleFor(u => u.comment).NotEmpty().NotNull();
        RuleFor(u => u.rating).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("rating cannot be less than 0 and greater than 5 ");
    }
}