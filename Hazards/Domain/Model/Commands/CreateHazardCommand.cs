namespace Acme.Center.Platform.Hazards.Domain.Model.Commands;

public record CreateHazardCommand(
    string Code,
    string Name,
    string Description,
    string Category,
    string BaseRiskLevel,
    string Status);
