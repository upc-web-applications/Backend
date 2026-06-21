using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RiskPatternRepository(AppDbContext context)
    : BaseRepository<RiskPattern>(context), IRiskPatternRepository
{
    public async Task<IEnumerable<RiskPattern>> FindBySectorAsync(string sector, CancellationToken cancellationToken = default)
        => await Context.Set<RiskPattern>().Where(r => r.Sector == sector).ToListAsync(cancellationToken);
}
