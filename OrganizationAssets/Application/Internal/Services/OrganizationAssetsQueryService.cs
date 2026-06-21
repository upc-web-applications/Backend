using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.OrganizationAssets.Application.Internal.Services;

public class OrganizationAssetsQueryService(AppDbContext context)
{
    public async Task<IEnumerable<Area>> GetActiveAreasAsync(CancellationToken cancellationToken)
    {
        return await context.Areas.Where(area => area.Estado == "Activo").ToListAsync(cancellationToken);
    }
}
