using Aurore.Application.Common.Interfaces;
using FluentValidation;

namespace Aurore.Application.Features.Orders.GetOrders
{
    public class GetOrdersUseCase : IUseCase<GetOrdersRequest, GetOrdersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetOrdersRequest> _validator;

        public GetOrdersUseCase(IUnitOfWork unitOfWork, IValidator<GetOrdersRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<GetOrdersResponse> ExecuteAsync(GetOrdersRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var orders = await _unitOfWork.Orders.GetOrdersAsync(request.Status, request.Page, request.PageSize, ct);
            var totalCount = await _unitOfWork.Orders.CountOrdersAsync(request.Status, ct);

            return new GetOrdersResponse
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Items = orders.Select(o => new OrderSummaryDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
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