using System;
using Aurore.Domain.Enums;
using Aurore.Domain.Exceptions;

namespace Aurore.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        private Order() { }

        public Order(Guid userId, DateTime expiresAt)
        {
            if (userId == Guid.Empty)
                throw new DomainException("UserId is required.");

            if (expiresAt <= DateTime.UtcNow)
                throw new DomainException("ExpiresAt must be in the future.");

            Id = Guid.NewGuid();
            UserId = userId;
            Status = OrderStatus.Pending;
            ExpiresAt = expiresAt;
            TotalAmount = 0m;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddItem(Guid ticketTypeId, int quantity, decimal unitPrice)
        {
            ThrowIfNotPending();

            if (ticketTypeId == Guid.Empty)
                throw new DomainException("TicketTypeId is required.");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than 0.");
            if (unitPrice < 0)
                throw new DomainException("UnitPrice cannot be negative.");

            var item = new OrderItem(Id, ticketTypeId, quantity, unitPrice);
            _items.Add(item);
            TotalAmount += item.Subtotal;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsPaid()
        {
            if (Status is not OrderStatus.Pending)
                throw new DomainException($"Cannot pay an order with status '{Status}'.");

            if (DateTime.UtcNow > ExpiresAt)
                throw new DomainException("Order has expired and can no longer be paid.");

            Status = OrderStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsExpired()
        {
            if (Status is not OrderStatus.Pending) return;

            Status = OrderStatus.Expired;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status is not OrderStatus.Pending)
                throw new DomainException($"Only pending orders can be cancelled. Current status: '{Status}'.");

            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        private void ThrowIfNotPending()
        {
            if (Status is not OrderStatus.Pending)
                throw new DomainException($"Cannot modify an order with status '{Status}'.");
        }
    }
}
