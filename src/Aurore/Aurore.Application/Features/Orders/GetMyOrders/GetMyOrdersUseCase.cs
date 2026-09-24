using Aurore.Application.Common.Interfaces;
using FluentValidation;

namespace Aurore.Application.Features.Orders.GetMyOrders
{
    public class GetMyOrdersUseCase : IUseCase<GetMyOrdersRequest, GetMyOrdersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetMyOrdersRequest> _validator;

        public GetMyOrdersUseCase(IUnitOfWork unitOfWork, IValidator<GetMyOrdersRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<GetMyOrdersResponse> ExecuteAsync(GetMyOrdersRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(request.UserId, request.Page, request.PageSize, request.Status, ct);

            var totalCount = await _unitOfWork.Orders.CountOrdersByUserIdAsync(request.UserId, request.Status, ct);

            return new GetMyOrdersResponse
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Items = orders.Select(o => new OrderSummaryDto
                {
                    Id = o.Id,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    ExpiresAt = o.ExpiresAt,
                    CreatedAt = o.CreatedAt,
                    ItemCount = o.Items.Count
                }).ToList()
            };
        }
    }
}