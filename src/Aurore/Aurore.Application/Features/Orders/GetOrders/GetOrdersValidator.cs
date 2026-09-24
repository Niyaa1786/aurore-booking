using FluentValidation;

namespace Aurore.Application.Features.Orders.GetOrders
{
    public class GetOrdersValidator : AbstractValidator<GetOrdersRequest>
    {
        public GetOrdersValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}