namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;

public record CreateRiskPatternCommand(
    string? SectorId,
    string Sector,
    string IncidentType,
    string HazardType,
    string Description,
    int Frequency,
    DateTime? FirstOccurrenceDate,
    int AnalysisPeriodDays,
    bool IsReviewed,
    DateTime? ReviewDate,
    string? ReviewedBy);
