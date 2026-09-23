using System;
using Aurore.Domain.Entities;

namespace Aurore.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
        Task<bool> IsEmailExist(string email, CancellationToken ct = default);
    }
}
