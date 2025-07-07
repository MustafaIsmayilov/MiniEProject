namespace MiniEProject.Application.Validations.OrderProductValidations;

using FluentValidation;
using MiniEProject.Application.DTOs.OrderProductDtos;

public class OrderProductCreateDtoValidator : AbstractValidator<OrderProductCreateDto>
{
    public OrderProductCreateDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID must not be empty.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price must be 0 or greater.");
    }
}

