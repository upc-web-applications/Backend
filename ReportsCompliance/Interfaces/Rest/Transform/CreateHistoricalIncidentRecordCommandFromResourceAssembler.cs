using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateHistoricalIncidentRecordCommandFromResourceAssembler
{
    public static CreateHistoricalIncidentRecordCommand ToCommandFromResource(CreateHistoricalIncidentRecordResource resource)
    {
        return new CreateHistoricalIncidentRecordCommand(resource.Sector, resource.IncidentType, resource.Criticality);
    }
}
