namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;

public record UpdateHazardResource(
    string Code,
    string Name,
    string Description,
    string Category,
    string BaseRiskLevel,
    string Status);
