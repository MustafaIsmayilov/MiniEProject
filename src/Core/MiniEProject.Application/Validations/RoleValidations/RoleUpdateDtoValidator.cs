using FluentValidation;
using MiniEProject.Application.DTOs.RoleDtos;

namespace MiniEProject.Application.Validations.RoleValidations;

public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
{
    public RoleUpdateDtoValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name!)
                .NotEmpty().WithMessage("Role name cannot be empty when provided.")
                .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.");
        });

        When(x => x.PermissionList != null, () =>
        {
            RuleFor(x => x.PermissionList!)
                .Must(pl => pl.Count > 0).WithMessage("Permission list must contain at least one permission if provided.")
                .ForEach(permissionRule =>
                    permissionRule.NotEmpty().WithMessage("Permission cannot be empty."));
        });
    }
}