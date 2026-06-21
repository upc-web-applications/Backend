using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using RiskGuard.Platform.MonitoringDashboard.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace RiskGuard.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class HeatMapZoneRepository(AppDbContext context) : BaseRepository<HeatMapZone>(context), IHeatMapZoneRepository
{
    public async Task<IEnumerable<HeatMapZone>> ListByRiskLevelAsync(string riskLevel,
        CancellationToken cancellationToken = default)
    {
        return await Context.HeatMapZones.Where(zone => zone.RiskLevel == riskLevel).ToListAsync(cancellationToken);
    }
}
