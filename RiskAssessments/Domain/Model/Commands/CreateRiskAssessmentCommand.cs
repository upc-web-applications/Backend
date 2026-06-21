namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;

public record CreateRiskAssessmentCommand(
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
    string? UserId);
