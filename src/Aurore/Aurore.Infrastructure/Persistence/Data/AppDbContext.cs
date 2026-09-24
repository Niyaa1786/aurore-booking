using Aurore.Domain.Entities;
using Aurore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Infrastructure.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


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

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");

                entity.HasMany(e => e.Items)
                    .WithOne(i => i.Order)
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => new { e.Status, e.ExpiresAt });
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");

                entity.HasMany(e => e.Tickets)
                      .WithOne(t => t.OrderItem)
                      .HasForeignKey(t => t.OrderItemId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.OrderId);
                entity.HasIndex(e => e.TicketTypeId);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);

                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasIndex(e => e.OrderItemId);
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Role = UserRole.Admin,
                FullName = "Admin",
                Email = "admin@aurore.com",
                PasswordHash = "$2a$11$E3RGRhjfkGzTz5J42JIOXe3dpiCEGaiZZxLIYfm0qdwnc/xFU/w.u",
                CreatedAt = new DateTime(2026, 9, 24),
                UpdatedAt = new DateTime(2026, 9, 24),
            });
        }
    }
}
