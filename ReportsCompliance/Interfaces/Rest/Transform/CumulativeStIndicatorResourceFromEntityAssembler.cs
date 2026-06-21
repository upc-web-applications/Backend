using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CumulativeStIndicatorResourceFromEntityAssembler
{
    public static CumulativeStIndicatorResource ToResourceFromEntity(CumulativeStIndicator entity)
    {
        return new CumulativeStIndicatorResource(entity.Id, entity.Name, entity.Value, entity.Status);
    }
}
