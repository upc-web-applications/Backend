using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateCumulativeStIndicatorCommandFromResourceAssembler
{
    public static CreateCumulativeStIndicatorCommand ToCommandFromResource(CreateCumulativeStIndicatorResource resource)
    {
        return new CreateCumulativeStIndicatorCommand(resource.Name, resource.Value, resource.Status);
    }
}
