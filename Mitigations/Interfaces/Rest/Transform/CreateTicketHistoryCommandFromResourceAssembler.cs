using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CreateTicketHistoryCommandFromResourceAssembler
{
    public static CreateTicketHistoryCommand ToCommandFromResource(CreateTicketHistoryResource r)
        => new(r.TicketId, r.Event, r.UserId, r.UserName, r.Details, r.Date);
}
