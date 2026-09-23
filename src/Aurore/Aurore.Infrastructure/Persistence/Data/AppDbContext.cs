using Aurore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Infrastructure.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.RefreshToken).HasMaxLength(500);

                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
