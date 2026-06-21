using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CriticalNotificationResourceFromEntityAssembler
{
    public static CriticalNotificationResource ToResourceFromEntity(CriticalNotification e)
        => new(e.Id, e.TicketId, e.SupervisorId, e.SupervisorName, e.Message,
               e.Sent, e.SentDate, e.CreatedAt, e.UpdatedAt);
}
