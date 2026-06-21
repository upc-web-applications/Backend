namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record DailySummaryResource(
    string Id,
    DateTime Date,
    string? SectorId,
    string Sector,
    int TotalNew,
    int TotalInProgress,
    int TotalResolved,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
