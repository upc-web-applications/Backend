using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;

namespace RiskGuard.Platform.MonitoringDashboard.Domain.Repositories;

public interface IHeatMapZoneRepository : IBaseRepository<HeatMapZone>
{
    Task<IEnumerable<HeatMapZone>> ListByRiskLevelAsync(string riskLevel, CancellationToken cancellationToken = default);
}
