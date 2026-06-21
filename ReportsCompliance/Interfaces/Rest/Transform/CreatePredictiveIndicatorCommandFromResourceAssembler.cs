using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreatePredictiveIndicatorCommandFromResourceAssembler
{
    public static CreatePredictiveIndicatorCommand ToCommandFromResource(CreatePredictiveIndicatorResource resource)
    {
        return new CreatePredictiveIndicatorCommand(resource.Name, resource.Description, resource.Value, resource.Trend);
    }
}
