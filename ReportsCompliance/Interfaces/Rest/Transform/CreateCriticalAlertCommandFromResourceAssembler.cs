using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateCriticalAlertCommandFromResourceAssembler
{
    public static CreateCriticalAlertCommand ToCommandFromResource(CreateCriticalAlertResource resource)
    {
        return new CreateCriticalAlertCommand(resource.Type, resource.Sector, resource.RiskType, resource.Message, resource.ElapsedHours, resource.ResponsibleSupervisor);
    }
}
