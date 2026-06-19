using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class PredictiveIndicatorResourceFromEntityAssembler
{
    public static PredictiveIndicatorResource ToResourceFromEntity(PredictiveIndicator entity)
    {
        return new PredictiveIndicatorResource(entity.Id, entity.Name, entity.Description, entity.Value, entity.Trend);
    }
}
