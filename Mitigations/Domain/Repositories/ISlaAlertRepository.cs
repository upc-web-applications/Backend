using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Domain.Repositories;

public interface ISlaAlertRepository : IBaseRepository<SlaAlert>
{
    Task<IEnumerable<SlaAlert>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default);
}
