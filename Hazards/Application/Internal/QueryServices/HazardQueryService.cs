using Acme.Center.Platform.Hazards.Application.QueryServices;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Domain.Model.Queries;
using Acme.Center.Platform.Hazards.Domain.Repositories;

namespace Acme.Center.Platform.Hazards.Application.Internal.QueryServices;

public class HazardQueryService(IHazardRepository hazardRepository) : IHazardQueryService
{
    public async Task<IEnumerable<Hazard>> Handle(GetAllHazardsQuery query, CancellationToken cancellationToken)
    {
        return await hazardRepository.ListAsync(cancellationToken);
    }

    public async Task<Hazard?> Handle(GetHazardByIdQuery query, CancellationToken cancellationToken)
    {
        return await hazardRepository.FindByIdAsync(query.Id, cancellationToken);
    }
}
