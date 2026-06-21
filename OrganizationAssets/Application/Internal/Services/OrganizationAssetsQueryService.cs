using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace Acme.Center.Platform.OrganizationAssets.Application.Internal.Services;

public class OrganizationAssetsQueryService(AppDbContext context)
{
    public async Task<IEnumerable<Area>> GetActiveAreasAsync(CancellationToken cancellationToken)
    {
        return await context.Areas.Where(area => area.Status == "Active").ToListAsync(cancellationToken);
    }
}
