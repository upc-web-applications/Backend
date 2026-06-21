using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Domain.Repositories;

public interface ICriticalNotificationRepository : IBaseRepository<CriticalNotification>
{
    Task<IEnumerable<CriticalNotification>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default);
}
