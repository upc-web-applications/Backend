using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Domain.Repositories;

public interface IAreaCriticalityLevelRepository : IBaseRepository<AreaCriticalityLevel>
{
    Task<IEnumerable<AreaCriticalityLevel>> FindBySectorAsync(string sector, CancellationToken cancellationToken = default);
}
