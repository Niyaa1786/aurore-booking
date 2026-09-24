using FluentValidation;

namespace Aurore.Application.Features.Orders.GetOrderById
{
    public class GetOrderByIdValidator : AbstractValidator<GetOrderByIdRequest>
    {
        public GetOrderByIdValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
        }
    }
}