using Acme.Center.Platform.Hazards.Application.QueryServices;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Domain.Model.Queries;
using Acme.Center.Platform.Hazards.Interfaces.Acl;

namespace Acme.Center.Platform.Hazards.Application.Acl;

public class HazardsContextFacade(IHazardQueryService queryService) : IHazardsContextFacade
{
    public async Task<Hazard?> FetchHazardById(string hazardId, CancellationToken cancellationToken = default)
    {
        var query = new GetHazardByIdQuery(hazardId);
        return await queryService.Handle(query, cancellationToken);
    }

    public async Task<IEnumerable<Hazard>> FetchAllHazards(CancellationToken cancellationToken = default)
    {
        var query = new GetAllHazardsQuery();
        return await queryService.Handle(query, cancellationToken);
    }
}
