using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateCumulativeStIndicatorCommandFromResourceAssembler
{
    public static CreateCumulativeStIndicatorCommand ToCommandFromResource(CreateCumulativeStIndicatorResource resource)
    {
        return new CreateCumulativeStIndicatorCommand(resource.Name, resource.Value, resource.Status);
    }
}
