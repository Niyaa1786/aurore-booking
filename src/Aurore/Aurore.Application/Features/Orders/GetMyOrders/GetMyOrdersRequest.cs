using System.Text.Json.Serialization;
using Aurore.Domain.Enums;

namespace Aurore.Application.Features.Orders.GetMyOrders
{
    public class GetMyOrdersRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public OrderStatus? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}