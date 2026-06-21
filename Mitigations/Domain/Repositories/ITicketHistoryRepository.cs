using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Domain.Repositories;

public interface ITicketHistoryRepository : IBaseRepository<TicketHistory>
{
    Task<IEnumerable<TicketHistory>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default);
}
