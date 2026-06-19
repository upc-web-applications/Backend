using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.ReportsCompliance.Application.Internal.Services;

public class ReportsComplianceQueryService(AppDbContext context)
{
    public async Task<IEnumerable<KpiDashboard>> GetExecutiveKpisAsync(CancellationToken cancellationToken)
    {
        return await context.KpiDashboard.AsNoTracking().ToListAsync(cancellationToken);
    }
}
