using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MiniEProject.Application.DTOs.UserAuthenticationDto;

namespace MiniEProject.Application.Validations.UserValidations;

public class UserForgotPasswordDtoValidator : AbstractValidator<UserForgotPasswordDto>
{
    public UserForgotPasswordDtoValidator()
    {
        RuleFor(fp => fp.Email)
             .NotEmpty().WithMessage("Email is required")
             .EmailAddress().WithMessage("Invalid email format");
    }
}
