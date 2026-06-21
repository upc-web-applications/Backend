using Acme.Center.Platform.Hazards.Domain.Model.Commands;
using Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Transform;

public static class CreateHazardCommandFromResourceAssembler
{
    public static CreateHazardCommand ToCommandFromResource(CreateHazardResource resource)
    {
        return new CreateHazardCommand(
            resource.Code,
            resource.Name,
            resource.Description,
            resource.Category,
            resource.BaseRiskLevel,
            resource.Status);
    }
}
