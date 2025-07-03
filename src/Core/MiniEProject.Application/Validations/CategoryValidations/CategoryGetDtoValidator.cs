using FluentValidation;
using MiniEProject.Application.DTOs.CategoryDtos;

namespace MiniEProject.Application.Validations.CategoryValidations;

public class CategoryGetDtoValidator: AbstractValidator<CategoryMainGetDto>
{
    public CategoryGetDtoValidator()
    {
        RuleFor(c => c.Id)
              .NotEmpty().WithMessage("Id can not be null.");

        RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Name can not be null.")
              .MinimumLength(3).WithMessage("Name should be minimum 3 characters.");
    }
}
