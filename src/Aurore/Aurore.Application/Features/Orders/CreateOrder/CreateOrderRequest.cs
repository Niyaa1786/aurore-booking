using System.Text.Json.Serialization;

namespace Aurore.Application.Features.Orders.CreateOrder
{
    public class CreateOrderRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        public Guid TicketTypeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}