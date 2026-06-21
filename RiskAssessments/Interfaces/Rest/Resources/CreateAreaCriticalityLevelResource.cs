namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record CreateAreaCriticalityLevelResource(
    string? SectorId,
    string Sector,
    string CriticalityLevel,
    string MapIntensity,
    DateTime LastUpdated);
