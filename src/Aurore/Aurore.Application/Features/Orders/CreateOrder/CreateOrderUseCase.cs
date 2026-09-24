using Aurore.Application.Common.Interfaces;
using Aurore.Domain.Entities;
using FluentValidation;

namespace Aurore.Application.Features.Orders.CreateOrder
{
    public class CreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateOrderRequest> _validator;

        public CreateOrderUseCase(IUnitOfWork unitOfWork, IValidator<CreateOrderRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var order = new Order(request.UserId, request.ExpiresAt);
            foreach (var item in request.Items)
                order.AddItem(item.TicketTypeId, item.Quantity, item.UnitPrice);

            _unitOfWork.Orders.Add(order);
            await _unitOfWork.SaveChangesAsync(ct);

            return new CreateOrderResponse
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                ExpiresAt = order.ExpiresAt
            };
        }
    }
}