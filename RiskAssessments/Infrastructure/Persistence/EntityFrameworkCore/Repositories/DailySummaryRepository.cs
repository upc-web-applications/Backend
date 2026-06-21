using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DailySummaryRepository(AppDbContext context)
    : BaseRepository<DailySummary>(context), IDailySummaryRepository
{
    public async Task<IEnumerable<DailySummary>> FindBySectorAsync(string sector, CancellationToken cancellationToken = default)
        => await Context.Set<DailySummary>().Where(r => r.Sector == sector).ToListAsync(cancellationToken);

    public async Task<IEnumerable<DailySummary>> FindByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        => await Context.Set<DailySummary>().Where(r => r.Date == date).ToListAsync(cancellationToken);
}
