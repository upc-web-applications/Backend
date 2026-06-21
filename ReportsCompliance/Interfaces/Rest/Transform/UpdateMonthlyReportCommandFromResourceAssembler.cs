using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class UpdateMonthlyReportCommandFromResourceAssembler
{
    public static MonthlyReport ToEntityFromResource(string id, UpdateMonthlyReportResource resource)
    {
        return new MonthlyReport
        {
            Id = id,
            Month = resource.Month,
            Year = resource.Year,
            Status = resource.Status,
            CreationDate = DateTime.UtcNow
        };
    }
}
