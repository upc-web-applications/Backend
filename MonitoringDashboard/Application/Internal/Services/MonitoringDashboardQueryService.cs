using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace Acme.Center.Platform.MonitoringDashboard.Application.Internal.Services;

public class MonitoringDashboardQueryService(AppDbContext context)
{
    public async Task<IEnumerable<HeatMapZone>> GetHeatMapAsync(CancellationToken cancellationToken)
    {
        return await context.HeatMapZones.OrderByDescending(zone => zone.HeatIndex).ToListAsync(cancellationToken);
    }
}
