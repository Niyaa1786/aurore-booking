using System.Text.Json.Serialization;

namespace Aurore.Application.Features.Orders.GetOrderById
{
    public class GetOrderByIdRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}