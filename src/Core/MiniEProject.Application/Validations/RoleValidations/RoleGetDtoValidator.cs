using FluentValidation;
using MiniEProject.Application.DTOs.RoleDtos;

namespace MiniEProject.Application.Validations.RoleValidations;

public class RoleGetDtoValidator : AbstractValidator<RoleGetDto>
{
    public RoleGetDtoValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");


        RuleFor(x => x.Permissions)
            .NotNull().WithMessage("Permissions list cannot be null.")
            .ForEach(permissionRule =>
                permissionRule.NotEmpty().WithMessage("Permission cannot be empty."));
    }
}


