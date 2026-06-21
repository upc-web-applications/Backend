using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Domain.Repositories;

public interface IRiskPatternRepository : IBaseRepository<RiskPattern>
{
    Task<IEnumerable<RiskPattern>> FindBySectorAsync(string sector, CancellationToken cancellationToken = default);
}
