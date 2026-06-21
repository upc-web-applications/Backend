using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Transform;

public static class HazardResourceFromEntityAssembler
{
    public static HazardResource ToResourceFromEntity(Hazard entity)
    {
        return new HazardResource(
            entity.Id,
            entity.Code,
            entity.Name,
            entity.Description,
            entity.Category,
            entity.BaseRiskLevel,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
