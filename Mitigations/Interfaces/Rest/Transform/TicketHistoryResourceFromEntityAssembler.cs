using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class TicketHistoryResourceFromEntityAssembler
{
    public static TicketHistoryResource ToResourceFromEntity(TicketHistory e)
        => new(e.Id, e.TicketId, e.Event, e.UserId, e.UserName, e.Details, e.Date, e.CreatedAt, e.UpdatedAt);
}
