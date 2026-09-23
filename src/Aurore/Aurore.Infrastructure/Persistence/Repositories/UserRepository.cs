using System;
using Aurore.Domain.Entities;
using Aurore.Domain.Interfaces;
using Aurore.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Aurore.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _context.Users.FindAsync(id, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email.Trim() == email, ct);

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, ct);

        public async Task<bool> IsEmailExist(string email, CancellationToken ct = default)
            => await _context.Users.AnyAsync(u => u.Email == email.Trim(), ct);

        public void Add(User entity) => _context.Add(entity);
        public void Update(User entity) => _context.Update(entity);
        public void Remove(User entity) => _context.Remove(entity);
    }
}
