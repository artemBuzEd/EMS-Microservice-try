using BLL.DTOs.Request.UserProfile;
using FluentValidation;

namespace BLL.DTOs.Validation.UpdateValidation;

public class UserProfileUpdateValidation : AbstractValidator<UserProfileUpdateRequestDTO>
{
    public UserProfileUpdateValidation()
    {
        RuleFor(u => u.first_name).NotEmpty().NotNull().WithMessage("First name cannot be empty or null");
        RuleFor(u => u.last_name).NotEmpty().NotNull().WithMessage("Last name cannot be empty or null");
        RuleFor(u => u.birth_date).NotEmpty().NotNull().LessThan(DateTime.Now).WithMessage("Birth date cannot be empty or null");
    }
}