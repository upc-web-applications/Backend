using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateMonthlyReportCommandFromResourceAssembler
{
    public static CreateMonthlyReportCommand ToCommandFromResource(CreateMonthlyReportResource resource)
    {
        return new CreateMonthlyReportCommand(resource.Month, resource.Year);
    }
}
