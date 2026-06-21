using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;
using RiskGuard.Platform.OrganizationAssets.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace RiskGuard.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AreaRepository(AppDbContext context) : BaseRepository<Area>(context), IAreaRepository
{
    public async Task<IEnumerable<Area>> ListActiveAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Areas.Where(area => area.Estado == "Activo").ToListAsync(cancellationToken);
    }
}
