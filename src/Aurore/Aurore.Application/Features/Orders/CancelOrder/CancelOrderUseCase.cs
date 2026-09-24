using Aurore.Application.Common.Exceptions;
using Aurore.Application.Common.Interfaces;
using Aurore.Domain.Entities;
using FluentValidation;

namespace Aurore.Application.Features.Orders.CancelOrder
{
    public class CancelOrderUseCase : IUseCase<CancelOrderRequest, CancelOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CancelOrderRequest> _validator;

        public CancelOrderUseCase(IUnitOfWork unitOfWork, IValidator<CancelOrderRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CancelOrderResponse> ExecuteAsync(CancelOrderRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, ct);
            if (order is null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.Cancel();
            await _unitOfWork.SaveChangesAsync(ct);

            return new CancelOrderResponse { OrderId = order.Id };
        }
    }
}