using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

public class DailySummary : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime Date { get; set; } = DateTime.UtcNow.Date;
    public string? SectorId { get; set; }
    public string Sector { get; set; } = string.Empty;
    public int TotalNew { get; set; }
    public int TotalInProgress { get; set; }
    public int TotalResolved { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
