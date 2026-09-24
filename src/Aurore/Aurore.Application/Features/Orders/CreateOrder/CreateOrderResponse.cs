namespace Aurore.Application.Features.Orders.CreateOrder
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}