using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class MitigationResourceFromEntityAssembler
{
    public static MitigationResource ToResourceFromEntity(Mitigation e)
        => new(e.Id, e.RiskAssessmentId, e.TicketId, e.Code, e.Description, e.Responsible, e.AssignedDate, e.ExecutionDate, e.Status, e.Result, e.Observations, e.CreatedAt, e.UpdatedAt);
}
