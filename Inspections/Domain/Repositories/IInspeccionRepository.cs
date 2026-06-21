using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;

namespace RiskGuard.Platform.Inspections.Domain.Repositories;

public interface IInspeccionRepository : IBaseRepository<Inspeccion>
{
    Task<IEnumerable<Inspeccion>> ListByOperatorAsync(string operatorId, CancellationToken cancellationToken = default);
}
