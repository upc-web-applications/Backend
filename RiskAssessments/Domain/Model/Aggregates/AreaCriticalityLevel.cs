using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

public class AreaCriticalityLevel : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? SectorId { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string CriticalityLevel { get; set; } = "Low";
    public string MapIntensity { get; set; } = "Low";
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
