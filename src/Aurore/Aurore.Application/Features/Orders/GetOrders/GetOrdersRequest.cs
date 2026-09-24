using Aurore.Domain.Enums;

namespace Aurore.Application.Features.Orders.GetOrders
{
    public class GetOrdersRequest
    {
        public OrderStatus? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}