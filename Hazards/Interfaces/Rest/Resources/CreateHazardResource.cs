namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;

public record CreateHazardResource(
    string Code,
    string Name,
    string Description,
    string Category,
    string BaseRiskLevel,
    string Status);
