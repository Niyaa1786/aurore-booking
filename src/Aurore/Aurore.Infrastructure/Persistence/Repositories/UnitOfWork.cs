using Aurore.Application.Common.Interfaces;
using Aurore.Domain.Interfaces;
using Aurore.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository? _userRepository;
        public UnitOfWork(AppDbContext context) => _context = context;

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
    }
}
