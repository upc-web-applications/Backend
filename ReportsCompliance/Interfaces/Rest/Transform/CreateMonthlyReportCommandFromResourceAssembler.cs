using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateMonthlyReportCommandFromResourceAssembler
{
    public static CreateMonthlyReportCommand ToCommandFromResource(CreateMonthlyReportResource resource)
    {
        return new CreateMonthlyReportCommand(resource.Month, resource.Year);
    }
}
