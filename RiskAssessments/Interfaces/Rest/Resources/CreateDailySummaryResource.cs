namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record CreateDailySummaryResource(
    DateTime Date,
    string? SectorId,
    string Sector,
    int TotalNew,
    int TotalInProgress,
    int TotalResolved);
