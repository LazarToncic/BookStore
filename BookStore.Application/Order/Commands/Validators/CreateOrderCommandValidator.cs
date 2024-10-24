using BookStore.Application.Common.Validators.OrderItemValidators;
using FluentValidation;

namespace BookStore.Application.Order.Commands.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(dto => dto.OrderDto.Items)
            .NotNull().WithMessage("Items cannot be null.")
            .NotEmpty().WithMessage("At least one item is required.")
            .Must(items => items.Any()).WithMessage("At least one item must be present.");
        
        RuleForEach(x => x.OrderDto.Items)
            .SetValidator(new OrderItemDtoValidator());
    }
}