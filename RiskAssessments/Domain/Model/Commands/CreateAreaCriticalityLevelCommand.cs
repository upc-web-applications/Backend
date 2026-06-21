namespace Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;

public record CreateAreaCriticalityLevelCommand(
    string? SectorId,
    string Sector,
    string CriticalityLevel,
    string MapIntensity,
    DateTime LastUpdated);
