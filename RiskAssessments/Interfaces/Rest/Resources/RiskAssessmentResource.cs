namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record RiskAssessmentResource(
    string Id,
    string Code,
    string Sector,
    string HazardType,
    string Description,
    int Probability,
    int Severity,
    string RiskLevel,
    string ControlMeasures,
    string Status,
    DateTime EvaluationDate,
    string? UserId,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
