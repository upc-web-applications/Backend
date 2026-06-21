using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

public class CriticalNotification : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TicketId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public string SupervisorName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool Sent { get; set; } = false;
    public DateTime? SentDate { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
