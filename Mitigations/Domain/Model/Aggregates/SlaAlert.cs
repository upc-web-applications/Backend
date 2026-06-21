using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

public class SlaAlert : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TicketId { get; set; } = string.Empty;
    public int ElapsedHours { get; set; } = 0;
    public int SlaLimitHours { get; set; } = 48;
    public DateTime AlertDate { get; set; } = DateTime.UtcNow;
    public string? NotifiedTo { get; set; }
    public string NotifiedName { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
