using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;

namespace RiskGuard.Platform.OrganizationAssets.Domain.Repositories;

public interface IAreaRepository : IBaseRepository<Area>
{
    Task<IEnumerable<Area>> ListActiveAsync(CancellationToken cancellationToken = default);
}
