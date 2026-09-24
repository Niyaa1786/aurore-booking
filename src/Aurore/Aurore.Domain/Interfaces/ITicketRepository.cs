using System;
using Aurore.Domain.Entities;

namespace Aurore.Domain.Interfaces
{
    public interface ITicketRepository : IBaseRepository<Ticket, Guid>
    {
        Task<IEnumerable<Ticket>> GetByOrderItemIdsAsync(IEnumerable<Guid> orderItemIds, CancellationToken ct = default);
    }
}
