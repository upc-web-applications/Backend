namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;

public record HazardResource(
    string Id,
    string Code,
    string Name,
    string Description,
    string Category,
    string BaseRiskLevel,
    string Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
