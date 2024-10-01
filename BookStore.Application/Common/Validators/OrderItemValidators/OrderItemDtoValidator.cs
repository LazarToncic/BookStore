using BookStore.Application.Common.Dto.OrderItems;
using FluentValidation;

namespace BookStore.Application.Common.Validators.OrderItemValidators;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.BookId)
            .NotNull()
            .NotEmpty().WithMessage("BookId is required.");
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .LessThan(8)
            .WithMessage("Quantity must be greater than zero and less than eight.");
        
        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("BookName is required.");
    }
}