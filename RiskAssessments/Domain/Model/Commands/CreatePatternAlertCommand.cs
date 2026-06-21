namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;

public record CreatePatternAlertCommand(
    string? PatternId,
    string? SectorId,
    string Sector,
    string RiskType,
    int OccurrenceCount,
    DateTime? FirstReportDate,
    string Status,
    DateTime GenerationDate);
