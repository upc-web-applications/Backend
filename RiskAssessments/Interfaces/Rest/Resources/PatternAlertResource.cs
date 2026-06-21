namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record PatternAlertResource(
    string Id,
    string? PatternId,
    string? SectorId,
    string Sector,
    string RiskType,
    int OccurrenceCount,
    DateTime? FirstReportDate,
    string Status,
    DateTime GenerationDate,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
