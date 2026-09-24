using System;
using Aurore.Domain.Entities;
using Aurore.Domain.Interfaces;
using Aurore.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Aurore.Infrastructure.Persistence.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context) => _context = context;

        public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _context.Tickets.FindAsync(id, ct);

        public async Task<IEnumerable<Ticket>> GetByOrderItemIdsAsync(IEnumerable<Guid> orderItemIds, CancellationToken ct = default)
        {
            var ids = orderItemIds.ToList();
            return await _context.Tickets
                .AsNoTracking()
                .Where(t => ids.Contains(t.OrderItemId))
                .ToListAsync(ct);
        }

        public void Add(Ticket entity) => _context.Tickets.Add(entity);
        public void Update(Ticket entity) => _context.Tickets.Update(entity);
        public void Remove(Ticket entity) => _context.Tickets.Remove(entity);
    }
}
