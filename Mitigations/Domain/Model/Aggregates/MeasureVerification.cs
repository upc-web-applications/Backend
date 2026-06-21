using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

public class MeasureVerification : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TicketId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public string SupervisorName { get; set; } = string.Empty;
    public string Verdict { get; set; } = string.Empty;
    public string JustificationComment { get; set; } = string.Empty;
    public DateTime VerificationDate { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
