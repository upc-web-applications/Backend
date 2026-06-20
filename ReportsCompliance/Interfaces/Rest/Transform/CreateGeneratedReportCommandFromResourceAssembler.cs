using RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateGeneratedReportCommandFromResourceAssembler
{
    public static CreateGeneratedReportCommand ToCommandFromResource(CreateGeneratedReportResource resource)
    {
        return new CreateGeneratedReportCommand(resource.Type, resource.Month, resource.Year, resource.Format, resource.FileName);
    }
}
