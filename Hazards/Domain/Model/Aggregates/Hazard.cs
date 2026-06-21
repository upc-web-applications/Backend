using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Hazards.Domain.Model.Aggregates;

public class Hazard : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string BaseRiskLevel { get; set; } = "Medium";
    public string Status { get; set; } = "Active";
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
