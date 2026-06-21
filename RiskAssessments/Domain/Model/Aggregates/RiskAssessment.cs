using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

public class RiskAssessment : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Code { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string HazardType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Probability { get; set; } = 1;
    public int Severity { get; set; } = 1;
    public string RiskLevel { get; set; } = "Low";
    public string ControlMeasures { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime EvaluationDate { get; set; } = DateTime.UtcNow;
    public string? UserId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
