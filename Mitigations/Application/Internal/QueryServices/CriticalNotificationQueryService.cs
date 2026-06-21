using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;

public class CriticalNotificationQueryService(ICriticalNotificationRepository repository) : ICriticalNotificationQueryService
{
    public async Task<IEnumerable<CriticalNotification>> Handle(GetAllCriticalNotificationsQuery q, CancellationToken ct) => await repository.ListAsync(ct);
    public async Task<CriticalNotification?> Handle(GetCriticalNotificationByIdQuery q, CancellationToken ct) => await repository.FindByIdAsync(q.Id, ct);
    public async Task<IEnumerable<CriticalNotification>> Handle(GetNotificationsByTicketQuery q, CancellationToken ct) => await repository.FindByTicketIdAsync(q.TicketId, ct);
}
