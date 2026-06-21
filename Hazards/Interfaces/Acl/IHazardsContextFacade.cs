using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Hazards.Interfaces.Acl;

public interface IHazardsContextFacade
{
    Task<Hazard?> FetchHazardById(string hazardId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Hazard>> FetchAllHazards(CancellationToken cancellationToken = default);
}
