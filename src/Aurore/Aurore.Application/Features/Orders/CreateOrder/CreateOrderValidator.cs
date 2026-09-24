using FluentValidation;

namespace Aurore.Application.Features.Orders.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.ExpiresAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("ExpiresAt must be in the future.");
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.TicketTypeId)
                    .NotEmpty().WithMessage("TicketTypeId is required.");
                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
                item.RuleFor(i => i.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("UnitPrice cannot be negative.");
            });
        }
    }
}