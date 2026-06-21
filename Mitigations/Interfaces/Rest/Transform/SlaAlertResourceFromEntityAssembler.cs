using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class SlaAlertResourceFromEntityAssembler
{
    public static SlaAlertResource ToResourceFromEntity(SlaAlert e)
        => new(e.Id, e.TicketId, e.ElapsedHours, e.SlaLimitHours, e.AlertDate,
               e.NotifiedTo, e.NotifiedName, e.CreatedAt, e.UpdatedAt);
}
