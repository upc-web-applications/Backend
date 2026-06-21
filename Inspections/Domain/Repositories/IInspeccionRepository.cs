using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Inspections.Domain.Repositories;

public interface IInspectionRepository : IBaseRepository<Inspection>
{
    Task<IEnumerable<Inspection>> ListByOperatorAsync(string operatorId, CancellationToken cancellationToken = default);
}
