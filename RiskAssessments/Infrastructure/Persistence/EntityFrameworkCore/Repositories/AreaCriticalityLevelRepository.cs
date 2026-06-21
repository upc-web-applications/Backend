using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AreaCriticalityLevelRepository(AppDbContext context)
    : BaseRepository<AreaCriticalityLevel>(context), IAreaCriticalityLevelRepository
{
    public async Task<IEnumerable<AreaCriticalityLevel>> FindBySectorAsync(string sector, CancellationToken cancellationToken = default)
        => await Context.Set<AreaCriticalityLevel>().Where(r => r.Sector == sector).ToListAsync(cancellationToken);
}
