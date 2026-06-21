using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

public class CorrectiveActionTicket : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public int TicketNumber { get; set; }
    public string? ReportId { get; set; }
    public string? SectorId { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string RiskType { get; set; } = string.Empty;
    public string CriticalityLevel { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string Instructions { get; set; } = string.Empty;
    public string? AssignedTechnicianId { get; set; }
    public string TechnicianName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ClosureDate { get; set; }
    public int SlaLimitHours { get; set; } = 48;
    public bool SlaMissed { get; set; } = false;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
