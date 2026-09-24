using System;
using Aurore.Domain.Enums;
using Aurore.Domain.Exceptions;

namespace Aurore.Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; }
    public Guid OrderItemId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public TicketStatus Status { get; private set; }
    public DateTime IssuedAt { get; private set; }

    private Ticket() { }

    public Ticket(Guid orderItemId, string code)
    {
        if (orderItemId == Guid.Empty)
            throw new DomainException("OrderItemId is required.");

        if (string.IsNullOrWhiteSpace(code))
            throw new DomainException("Ticket code cannot be empty.");

        Id = Guid.NewGuid();
        OrderItemId = orderItemId;
        Code = code;
        Status = TicketStatus.Valid;
        IssuedAt = DateTime.UtcNow;
    }

    public void MarkAsUsed()
    {
        if (Status is not TicketStatus.Valid)
            throw new DomainException($"Only valid tickets can be used. Current status: '{Status}'.");

        Status = TicketStatus.Used;
    }

    public void Cancel()
    {
        if (Status == TicketStatus.Used)
            throw new DomainException("Cannot cancel a ticket that was already used.");

        Status = TicketStatus.Cancelled;
    }
}
