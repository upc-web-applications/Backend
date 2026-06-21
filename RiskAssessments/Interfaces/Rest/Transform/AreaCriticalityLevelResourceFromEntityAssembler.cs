using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class AreaCriticalityLevelResourceFromEntityAssembler
{
    public static AreaCriticalityLevelResource ToResourceFromEntity(AreaCriticalityLevel entity)
        => new(entity.Id, entity.SectorId, entity.Sector, entity.CriticalityLevel, entity.MapIntensity,
               entity.LastUpdated, entity.CreatedAt, entity.UpdatedAt);
}
