using BLL.DTOs.Request.UserProfile;
using DAL.Entities;
using FluentValidation;

namespace BLL.DTOs.Validation.CreateValidation;

public class UserProfileCreateValidation : AbstractValidator<UserProfileCreateRequestDTO>
{
    public UserProfileCreateValidation()
    {
        RuleFor(u => u.user_id).NotNull().NotEmpty().WithMessage("User id cannot be empty or null");
        RuleFor(u => u.first_name).NotNull().NotEmpty().WithMessage("First name cannot be empty or null");
        RuleFor(u => u.last_name).NotNull().NotEmpty().WithMessage("Last name cannot be empty or null");
        RuleFor(u => u.birth_date).NotNull().NotEmpty().LessThan(DateTime.Now).WithMessage("Birth date cannot be empty or null");
    }
}