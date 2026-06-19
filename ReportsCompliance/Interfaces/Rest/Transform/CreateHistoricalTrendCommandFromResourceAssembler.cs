using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateHistoricalTrendCommandFromResourceAssembler
{
    public static CreateHistoricalTrendCommand ToCommandFromResource(CreateHistoricalTrendResource resource)
    {
        return new CreateHistoricalTrendCommand(resource.Month, resource.Year, resource.TotalIncidents, resource.Sector, resource.Type);
    }
}
