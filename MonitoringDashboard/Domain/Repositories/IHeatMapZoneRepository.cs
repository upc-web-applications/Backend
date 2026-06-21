using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.MonitoringDashboard.Domain.Repositories;

public interface IHeatMapZoneRepository : IBaseRepository<HeatMapZone>
{
    Task<IEnumerable<HeatMapZone>> ListByRiskLevelAsync(string riskLevel, CancellationToken cancellationToken = default);
}
