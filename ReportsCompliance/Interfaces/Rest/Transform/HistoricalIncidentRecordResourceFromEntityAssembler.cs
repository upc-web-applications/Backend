using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class HistoricalIncidentRecordResourceFromEntityAssembler
{
    public static HistoricalIncidentRecordResource ToResourceFromEntity(HistoricalIncidentRecord entity)
    {
        return new HistoricalIncidentRecordResource(entity.Id, entity.Sector, entity.IncidentType, entity.Criticality, entity.IncidentDate);
    }
}
