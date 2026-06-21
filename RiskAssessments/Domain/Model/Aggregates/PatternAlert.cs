using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

public class PatternAlert : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? PatternId { get; set; }
    public string? SectorId { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string RiskType { get; set; } = string.Empty;
    public int OccurrenceCount { get; set; }
    public DateTime? FirstReportDate { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime GenerationDate { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
