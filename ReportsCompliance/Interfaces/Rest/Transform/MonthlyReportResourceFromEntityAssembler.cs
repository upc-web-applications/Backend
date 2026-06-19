using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class MonthlyReportResourceFromEntityAssembler
{
    public static MonthlyReportResource ToResourceFromEntity(MonthlyReport entity)
    {
        return new MonthlyReportResource(entity.Id, entity.Month, entity.Year, entity.Status, entity.CreationDate);
    }
}
