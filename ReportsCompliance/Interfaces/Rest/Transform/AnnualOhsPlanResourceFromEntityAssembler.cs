using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class AnnualOhsPlanResourceFromEntityAssembler
{
    public static AnnualOhsPlanResource ToResourceFromEntity(AnnualOhsPlan entity)
    {
        return new AnnualOhsPlanResource(
            entity.Id, entity.Year, entity.GlobalCompliance, entity.Goal, entity.CompletedActivities, entity.TotalActivities,
            0, entity.CreatedAt?.UtcDateTime ?? DateTime.UtcNow, [], [], null);
    }
}
