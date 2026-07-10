using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class UpdateHistoricalIncidentRecordCommandFromResourceAssembler
{
    public static HistoricalIncidentRecord ToEntityFromResource(string id, UpdateHistoricalIncidentRecordResource resource)
    {
        return new HistoricalIncidentRecord
        {
            Id = id,
            Sector = resource.Sector,
            IncidentType = resource.IncidentType,
            Criticality = resource.Criticality,
            IncidentDate = resource.IncidentDate,
            Description = resource.Description,
            Resolved = resource.Resolved,
            ClosingDate = resource.ClosingDate,
            ResolutionTimeHours = resource.ResolutionTimeHours,
            OperatorId = resource.OperatorId
        };
    }
}
