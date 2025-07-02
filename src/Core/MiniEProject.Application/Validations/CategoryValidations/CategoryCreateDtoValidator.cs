using FluentValidation;
using MiniEProject.Application.DTOs.CategoryDtos;

namespace MiniEProject.Application.Validations.CategoryValidations;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name important.")
            .Length(2, 100).WithMessage("Name can be 2-100.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description max can be 500 ");
    }
}
