using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class PredictiveIndicatorResourceFromEntityAssembler
{
    public static PredictiveIndicatorResource ToResourceFromEntity(PredictiveIndicator entity)
    {
        return new PredictiveIndicatorResource(
            entity.Id, entity.Name, entity.Description, entity.Value, entity.Trend,
            entity.CreatedAt?.UtcDateTime ?? DateTime.UtcNow, 30, 0, 0, 0, 24,
            [], [], []);
    }
}
