namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;

public record CreateDailySummaryCommand(
    DateTime Date,
    string? SectorId,
    string Sector,
    int TotalNew,
    int TotalInProgress,
    int TotalResolved);
