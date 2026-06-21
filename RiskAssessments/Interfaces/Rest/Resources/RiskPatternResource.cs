namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record RiskPatternResource(
    string Id,
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
    string? ReviewedBy,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
