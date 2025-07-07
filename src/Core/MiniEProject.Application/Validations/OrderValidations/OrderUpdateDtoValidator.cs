using FluentValidation;
using MiniEProject.Application.DTOs.OrderDtos;
using MiniEProject.Application.Validations.OrderProductValidations;

namespace MiniEProject.Application.Validations.OrderValidations;

public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.OrderProducts)
            .NotNull().WithMessage("OrderProducts must not be null.")
            .Must(x => x.Count > 0).WithMessage("Order must contain at least one product.");

        RuleForEach(x => x.OrderProducts)
            .SetValidator(new OrderProductUpdateDtoValidator());
    }
}
