using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateAnnualOhsPlanCommandFromResourceAssembler
{
    public static CreateAnnualOhsPlanCommand ToCommandFromResource(CreateAnnualOhsPlanResource resource)
    {
        return new CreateAnnualOhsPlanCommand(resource.Year, resource.GlobalCompliance, resource.Goal, resource.CompletedActivities, resource.TotalActivities);
    }
}
