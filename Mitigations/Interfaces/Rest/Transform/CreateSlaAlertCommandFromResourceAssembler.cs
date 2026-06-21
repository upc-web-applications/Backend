using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CreateSlaAlertCommandFromResourceAssembler
{
    public static CreateSlaAlertCommand ToCommandFromResource(CreateSlaAlertResource r)
        => new(r.TicketId, r.ElapsedHours, r.SlaLimitHours, r.AlertDate, r.NotifiedTo, r.NotifiedName);
}
