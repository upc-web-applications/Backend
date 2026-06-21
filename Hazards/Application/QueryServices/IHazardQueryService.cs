using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Domain.Model.Queries;

namespace Acme.Center.Platform.Hazards.Application.QueryServices;

public interface IHazardQueryService
{
    Task<IEnumerable<Hazard>> Handle(GetAllHazardsQuery query, CancellationToken cancellationToken);
    Task<Hazard?> Handle(GetHazardByIdQuery query, CancellationToken cancellationToken);
}
