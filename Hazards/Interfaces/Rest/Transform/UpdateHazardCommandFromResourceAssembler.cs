using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Transform;

public static class UpdateHazardCommandFromResourceAssembler
{
    public static Hazard ToEntityFromResource(string id, UpdateHazardResource resource)
    {
        return new Hazard
        {
            Id = id,
            Code = resource.Code,
            Name = resource.Name,
            Description = resource.Description,
            Category = resource.Category,
            BaseRiskLevel = resource.BaseRiskLevel,
            Status = resource.Status
        };
    }
}
