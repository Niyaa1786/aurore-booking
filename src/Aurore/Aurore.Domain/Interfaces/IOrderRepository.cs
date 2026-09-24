using System;
using Aurore.Domain.Entities;
using Aurore.Domain.Enums;

namespace Aurore.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order, Guid>

    {
        Task<Order?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, int page, int pageSize, OrderStatus? orderStatus = null, CancellationToken ct = default);
        Task<IEnumerable<Order>> GetOrdersAsync(OrderStatus? status, int page, int pageSize, CancellationToken ct = default);
        Task<IEnumerable<Order>> GetExpiredPendingOrdersAsync(DateTime now, int batchSize, CancellationToken ct = default);
        Task<int> CountOrdersAsync(OrderStatus? status, CancellationToken ct = default);
        Task<int> CountOrdersByUserIdAsync(Guid userId, OrderStatus? status = null, CancellationToken ct = default);
    }
}
