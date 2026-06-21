using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.OrganizationAssets.Domain.Repositories;

public interface IAreaRepository : IBaseRepository<Area>
{
    Task<IEnumerable<Area>> ListActiveAsync(CancellationToken cancellationToken = default);
}
