using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class KpiDashboardResourceFromEntityAssembler
{
    public static KpiDashboardResource ToResourceFromEntity(KpiDashboard entity)
    {
        return new KpiDashboardResource(entity.Id, entity.Name, entity.Value, entity.Goal, entity.Status, entity.UpdateDate);
    }
}
