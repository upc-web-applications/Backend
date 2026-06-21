using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Domain.Repositories;

public interface IDailySummaryRepository : IBaseRepository<DailySummary>
{
    Task<IEnumerable<DailySummary>> FindBySectorAsync(string sector, CancellationToken cancellationToken = default);
    Task<IEnumerable<DailySummary>> FindByDateAsync(DateTime date, CancellationToken cancellationToken = default);
}
