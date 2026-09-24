using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aurore.Domain.Entities;
using Aurore.Domain.Enums;
using Aurore.Domain.Interfaces;
using Aurore.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Aurore.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) => _context = context;

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);

        public async Task<Order?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
            => await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, int page, int pageSize, OrderStatus? orderStatus = null, CancellationToken ct = default)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.UserId == userId);

            if (orderStatus.HasValue)
                query = query.Where(o => o.Status == orderStatus.Value);

            return await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderStatus? status, int page, int pageSize, CancellationToken ct = default)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            return await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<int> CountOrdersAsync(OrderStatus? status, CancellationToken ct = default)
        {
            var query = _context.Orders
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            return await query.CountAsync(ct);
        }

        public async Task<int> CountOrdersByUserIdAsync(Guid userId, OrderStatus? status = null, CancellationToken ct = default)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            return await query.CountAsync(ct);
        }

        public async Task<IEnumerable<Order>> GetExpiredPendingOrdersAsync(DateTime now, int batchSize, CancellationToken ct = default)
            => await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.Status == OrderStatus.Pending && o.ExpiresAt < now)
                .OrderBy(o => o.ExpiresAt)
                .Take(batchSize)
                .ToListAsync(ct);

        public void Add(Order entity) => _context.Orders.Add(entity);
        public void Update(Order entity) => _context.Orders.Update(entity);
        public void Remove(Order entity) => _context.Orders.Remove(entity);
    }
}