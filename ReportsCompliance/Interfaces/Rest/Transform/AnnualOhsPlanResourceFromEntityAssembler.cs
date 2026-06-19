using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class AnnualOhsPlanResourceFromEntityAssembler
{
    public static AnnualOhsPlanResource ToResourceFromEntity(AnnualOhsPlan entity)
    {
        return new AnnualOhsPlanResource(entity.Id, entity.Year, entity.GlobalCompliance, entity.Goal, entity.CompletedActivities, entity.TotalActivities);
    }
}
