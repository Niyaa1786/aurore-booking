using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
