using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CreateMeasureVerificationCommandFromResourceAssembler
{
    public static CreateMeasureVerificationCommand ToCommandFromResource(CreateMeasureVerificationResource r)
        => new(r.TicketId, r.SupervisorId, r.SupervisorName, r.Verdict, r.JustificationComment, r.VerificationDate);
}
