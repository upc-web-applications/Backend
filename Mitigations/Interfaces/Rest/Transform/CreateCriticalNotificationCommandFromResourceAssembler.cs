using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CreateCriticalNotificationCommandFromResourceAssembler
{
    public static CreateCriticalNotificationCommand ToCommandFromResource(CreateCriticalNotificationResource r)
        => new(r.TicketId, r.SupervisorId, r.SupervisorName, r.Message, r.Sent, r.SentDate);
}
