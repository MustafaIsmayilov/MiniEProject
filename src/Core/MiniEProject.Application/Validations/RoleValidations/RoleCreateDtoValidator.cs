using FluentValidation;
using MiniEProject.Application.DTOs.RoleDtos;

namespace MiniEProject.Application.Validations.RoleValidations;

public class RoleCreateDtoValidator : AbstractValidator<RoleCreateDto>
{
    public RoleCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.");

        RuleFor(x => x.PermissionList)
            .NotNull().WithMessage("Permission list cannot be null.")
            .Must(pl => pl.Count > 0).WithMessage("At least one permission must be specified.")
            .ForEach(permissionRule =>
                permissionRule.NotEmpty().WithMessage("Permission cannot be empty."));
    }
}
