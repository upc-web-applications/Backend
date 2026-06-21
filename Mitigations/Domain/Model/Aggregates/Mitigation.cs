using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

public class Mitigation : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? RiskAssessmentId { get; set; }
    public string? TicketId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Responsible { get; set; } = string.Empty;
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExecutionDate { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Result { get; set; }
    public string? Observations { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
