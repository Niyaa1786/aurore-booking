using System.Text.Json.Serialization;

namespace Aurore.Application.Features.Orders.CancelOrder
{
    public class CancelOrderRequest
    {
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}