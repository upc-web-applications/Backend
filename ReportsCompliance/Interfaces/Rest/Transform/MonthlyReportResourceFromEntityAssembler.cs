using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class MonthlyReportResourceFromEntityAssembler
{
    public static MonthlyReportResource ToResourceFromEntity(MonthlyReport entity)
    {
        return new MonthlyReportResource(entity.Id, entity.Month, entity.Year, entity.Status, entity.CreationDate);
    }
}
