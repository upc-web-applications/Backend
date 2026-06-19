using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateKpiDashboardCommandFromResourceAssembler
{
    public static CreateKpiDashboardCommand ToCommandFromResource(CreateKpiDashboardResource resource)
    {
        return new CreateKpiDashboardCommand(resource.Name, resource.Value, resource.Goal, resource.Status);
    }
}
