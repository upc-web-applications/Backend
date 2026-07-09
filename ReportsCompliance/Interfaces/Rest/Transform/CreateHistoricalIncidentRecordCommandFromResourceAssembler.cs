using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateHistoricalIncidentRecordCommandFromResourceAssembler
{
    public static CreateHistoricalIncidentRecordCommand ToCommandFromResource(CreateHistoricalIncidentRecordResource resource)
    {
        return new CreateHistoricalIncidentRecordCommand(
            resource.Sector, resource.IncidentType, resource.Criticality,
            resource.Description, resource.Resolved, resource.ClosingDate, resource.ResolutionTimeHours, resource.OperatorId);
    }
}
