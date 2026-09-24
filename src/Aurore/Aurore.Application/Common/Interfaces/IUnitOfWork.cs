using System;
using System.Collections.Generic;
using System.Text;
using Aurore.Domain.Interfaces;

namespace Aurore.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        ITicketRepository Tickets { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
