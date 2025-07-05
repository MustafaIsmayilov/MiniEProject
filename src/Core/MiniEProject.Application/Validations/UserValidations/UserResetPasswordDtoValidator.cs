using FluentValidation;
using MiniEProject.Application.DTOs.UserAuthenticationDto;

namespace MiniEProject.Application.Validations.UserValidations;

public class UserResetPasswordDtoValidator : AbstractValidator<UserResetPasswordDto>
{
    public UserResetPasswordDtoValidator()
    {
        RuleFor(rp => rp.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be null");

        RuleFor(rp => rp.Token)
            .NotEmpty()
            .WithMessage("Reset token cannot be null");

        RuleFor(rp => rp.NewPassword)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
    }
}

