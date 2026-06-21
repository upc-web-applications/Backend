namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

public record AreaCriticalityLevelResource(
    string Id,
    string? SectorId,
    string Sector,
    string CriticalityLevel,
    string MapIntensity,
    DateTime LastUpdated,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
