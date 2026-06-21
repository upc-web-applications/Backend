using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

public class RiskPattern : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? SectorId { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string IncidentType { get; set; } = string.Empty;
    public string HazardType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Frequency { get; set; }
    public DateTime? FirstOccurrenceDate { get; set; }
    public int AnalysisPeriodDays { get; set; } = 30;
    public bool IsReviewed { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? ReviewedBy { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
