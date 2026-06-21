using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class HistoricalTrendResourceFromEntityAssembler
{
    public static HistoricalTrendResource ToResourceFromEntity(HistoricalTrend entity)
    {
        return new HistoricalTrendResource(entity.Id, entity.Month, entity.Year, entity.TotalIncidents, entity.Sector, entity.Type);
    }
}
