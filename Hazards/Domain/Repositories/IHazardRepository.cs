using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Hazards.Domain.Repositories;

public interface IHazardRepository : IBaseRepository<Hazard>
{
}
