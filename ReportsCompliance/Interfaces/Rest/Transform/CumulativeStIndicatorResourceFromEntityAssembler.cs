using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CumulativeStIndicatorResourceFromEntityAssembler
{
    public static CumulativeStIndicatorResource ToResourceFromEntity(CumulativeStIndicator entity)
    {
        return new CumulativeStIndicatorResource(entity.Id, entity.Name, entity.Value, entity.Status);
    }
}
