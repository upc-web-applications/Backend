namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record CreatePatternAlertResource(
    string? PatternId,
    string? SectorId,
    string Sector,
    string RiskType,
    int OccurrenceCount,
    DateTime? FirstReportDate,
    string Status,
    DateTime GenerationDate);
