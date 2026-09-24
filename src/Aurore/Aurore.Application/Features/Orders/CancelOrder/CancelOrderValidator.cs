using FluentValidation;

namespace Aurore.Application.Features.Orders.CancelOrder
{
    public class CancelOrderValidator : AbstractValidator<CancelOrderRequest>
    {
        public CancelOrderValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
        }
    }
}