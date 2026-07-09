using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CreateGeneratedReportCommandFromResourceAssembler
{
    public static CreateGeneratedReportCommand ToCommandFromResource(CreateGeneratedReportResource resource)
    {
        return new CreateGeneratedReportCommand(
            resource.Type, resource.Month, resource.Year, resource.Format, resource.FileName,
            resource.StartDate, resource.EndDate, resource.SectorFilter, resource.SizeKb);
    }
}
