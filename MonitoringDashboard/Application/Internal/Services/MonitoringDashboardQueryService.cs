using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.MonitoringDashboard.Application.Internal.Services;

public class MonitoringDashboardQueryService(AppDbContext context)
{
    public async Task<IEnumerable<HeatMapZone>> GetHeatMapAsync(CancellationToken cancellationToken)
    {
        return await context.HeatMapZones.OrderByDescending(zone => zone.HeatIndex).ToListAsync(cancellationToken);
    }
}
