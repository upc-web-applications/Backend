using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateCriticalAlertCommandFromResourceAssembler
{
    public static CreateCriticalAlertCommand ToCommandFromResource(CreateCriticalAlertResource resource)
    {
        return new CreateCriticalAlertCommand(resource.Type, resource.Sector, resource.RiskType, resource.Message, resource.ElapsedHours);
    }
}
