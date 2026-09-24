using FluentValidation;

namespace Aurore.Application.Features.Orders.GetMyOrders
{
    public class GetMyOrdersValidator : AbstractValidator<GetMyOrdersRequest>
    {
        public GetMyOrdersValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}