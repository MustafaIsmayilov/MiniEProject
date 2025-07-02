using FluentValidation;
using MiniEProject.Application.DTOs.CategoryDtos;

namespace MiniEProject.Application.Validations.CategoryValidations;

public class CategoryUpdateDtoValiator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValiator()
    {
        RuleFor(x => x.Id)
    .NotEmpty().WithMessage("Id boş ola bilməz.");

        RuleFor(x => x.Name)
    .NotEmpty().WithMessage("Name cannot be empty.")
    .MaximumLength(100).WithMessage("Name can be a maximum of 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description can be a maximum of 500 characters.");

        RuleFor(x => x.ParentCategoryId)
            .Must(x => x == null || x != Guid.Empty)
            .WithMessage("ParentCategoryId must be in a valid format.");
    }
}
