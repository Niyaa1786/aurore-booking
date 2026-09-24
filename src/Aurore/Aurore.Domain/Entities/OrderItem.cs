using System;
using Aurore.Domain.Exceptions;

namespace Aurore.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid TicketTypeId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Order? Order { get; private set; }

        private readonly List<Ticket> _tickets = new();
        public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();

        private OrderItem() { }

        internal OrderItem(Guid orderId, Guid ticketTypeId, int quantity, decimal unitPrice)
        {
            if (orderId == Guid.Empty)
                throw new DomainException("OrderId is required.");

            if (ticketTypeId == Guid.Empty)
                throw new DomainException("TicketTypeId is required.");

            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than 0.");

            if (unitPrice < 0)
                throw new DomainException("UnitPrice cannot be negative.");

            Id = Guid.NewGuid();
            OrderId = orderId;
            TicketTypeId = ticketTypeId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal Subtotal => UnitPrice * Quantity;
    }
}
