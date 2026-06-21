using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

public class TicketHistory : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TicketId { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
