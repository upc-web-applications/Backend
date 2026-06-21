using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using Acme.Center.Platform.OrganizationAssets.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AreaRepository(AppDbContext context) : BaseRepository<Area>(context), IAreaRepository
{
    public async Task<IEnumerable<Area>> ListActiveAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Areas.Where(area => area.Status == "Active").ToListAsync(cancellationToken);
    }
}
