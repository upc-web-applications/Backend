using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateKpiDashboardCommandFromResourceAssembler
{
    public static CreateKpiDashboardCommand ToCommandFromResource(CreateKpiDashboardResource resource)
    {
        return new CreateKpiDashboardCommand(resource.Name, resource.Value, resource.Goal, resource.Status);
    }
}
