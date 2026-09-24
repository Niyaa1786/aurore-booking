using Aurore.Application.Common.Exceptions;
using Aurore.Application.Common.Interfaces;
using Aurore.Domain.Entities;
using FluentValidation;

namespace Aurore.Application.Features.Orders.GetOrderById
{
    public class GetOrderByIdUseCase : IUseCase<GetOrderByIdRequest, GetOrderByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetOrderByIdRequest> _validator;

        public GetOrderByIdUseCase(IUnitOfWork unitOfWork, IValidator<GetOrderByIdRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<GetOrderByIdResponse> ExecuteAsync(GetOrderByIdRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var order = await _unitOfWork.Orders.GetByIdWithDetailsAsync(request.OrderId, ct);
            if (order is null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            return new GetOrderByIdResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                ExpiresAt = order.ExpiresAt,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    TicketTypeId = i.TicketTypeId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }
    }
}