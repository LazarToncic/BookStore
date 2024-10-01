using BookStore.Application.Common.Validators.OrderItemValidators;
using FluentValidation;

namespace BookStore.Application.Order.Commands.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(dto => dto.OrderDto.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(dto => dto.OrderDto.Items)
            .NotNull().WithMessage("Items cannot be null.")
            .NotEmpty().WithMessage("At least one item is required.")
            .Must(items => items.Any()).WithMessage("At least one item must be present.");

        /*RuleForEach(dto => dto.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.BookId)
                .NotEmpty().WithMessage("BookId is required.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0)
                .LessThan(8)
                .WithMessage("Quantity must be greater than zero and lesser than eight.");

            item.RuleFor(i => i.BookName)
                .NotEmpty().WithMessage("BookName is required.");
        });*/
        
        RuleForEach(x => x.OrderDto.Items)
            .SetValidator(new OrderItemDtoValidator());
    }
}