using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.MonitoringDashboard.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class HeatMapZoneRepository(AppDbContext context) : BaseRepository<HeatMapZone>(context), IHeatMapZoneRepository
{
    public async Task<IEnumerable<HeatMapZone>> ListByRiskLevelAsync(string riskLevel,
        CancellationToken cancellationToken = default)
    {
        return await Context.HeatMapZones.Where(zone => zone.RiskLevel == riskLevel).ToListAsync(cancellationToken);
    }
}
