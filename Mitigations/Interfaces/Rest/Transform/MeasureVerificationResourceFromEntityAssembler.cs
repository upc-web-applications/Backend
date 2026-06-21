using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class MeasureVerificationResourceFromEntityAssembler
{
    public static MeasureVerificationResource ToResourceFromEntity(MeasureVerification e)
        => new(e.Id, e.TicketId, e.SupervisorId, e.SupervisorName, e.Verdict,
               e.JustificationComment, e.VerificationDate, e.CreatedAt, e.UpdatedAt);
}
