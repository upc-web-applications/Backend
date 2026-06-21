using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;

namespace Acme.Center.Platform.Mitigations.Application.QueryServices;

public interface ICriticalNotificationQueryService
{
    Task<IEnumerable<CriticalNotification>> Handle(GetAllCriticalNotificationsQuery query, CancellationToken cancellationToken);
    Task<CriticalNotification?> Handle(GetCriticalNotificationByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CriticalNotification>> Handle(GetNotificationsByTicketQuery query, CancellationToken cancellationToken);
}
